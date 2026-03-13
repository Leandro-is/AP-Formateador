using IS.DocumenFormater.utilities.SSH.Exchange;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IS.DocumenFormater.utilities.SSH
{
    public class SftpServices : ISftpServices
    {
        private readonly ISftpConfiguration _sftpConfiguration;
        public SftpServices(ISftpConfiguration sftpConfiguration)
        {
            _sftpConfiguration = sftpConfiguration;
        }

        public async Task<bool> UploadFile(string fileBase64, string pathFile)
        {
            byte[] file = Convert.FromBase64String(fileBase64);
            return await UploadFile(file, pathFile);
        }
        public async Task<bool> UploadFile(byte[] file, string pathFile)
        {
            //SftpClient _sftp = new SftpClient(_sftpConfiguration.SftpHost, _sftpConfiguration.SftpPort, _sftpConfiguration.SftpUsername, _sftpConfiguration.SftpPassword);
            //_sftp.Connect();
            //if (_sftp.IsConnected)
            //{
            //    using (Stream uplfileStream = new MemoryStream(file))
            //    {
            //        String finalPath = $"{_sftpConfiguration.SftpPathBase}{pathFile}";
            //        String dir = Path.GetDirectoryName(finalPath).Replace("\\", "/");

            //        CreateDirectoryRecursively(_sftp, dir);

            //        if (!_sftp.Exists(dir)) _sftp.CreateDirectory(dir);

            //        _sftp.UploadFile(uplfileStream, finalPath, true);
            //    }
            //}
            //_sftp.Disconnect();
            //_sftp.Dispose();
            //return true;


            return await Task.Run<bool>(delegate ()
            {
                bool success = false;
                using (SftpClient sftp = new SftpClient(_sftpConfiguration.SftpHost, _sftpConfiguration.SftpPort, _sftpConfiguration.SftpUsername, _sftpConfiguration.SftpPassword))
                {
                    sftp.Connect();
                    if (sftp.IsConnected)
                    {
                        using (Stream uplfileStream = new MemoryStream(file))
                        {
                            String finalPath = $"{_sftpConfiguration.SftpPathBase}{pathFile}";
                            String dir = Path.GetDirectoryName(finalPath).Replace("\\", "/");
                            //if (!sftp.Exists(dir)) sftp.CreateDirectory(dir);
                            CreateDirectoryRecursively(sftp, dir);
                            sftp.UploadFile(uplfileStream, finalPath, true);
                        }
                        sftp.Disconnect();
                    }
                    success = true;
                }
                return success;
            });
        }
        public void CreateDirectoryRecursively(SftpClient client, string path)
        {
            string current = "";
            if (path[0] == '/') path = path.Substring(1);
            while (!string.IsNullOrEmpty(path))
            {
                int p = path.IndexOf('/');
                current += '/';
                if (p >= 0)
                {
                    current += path.Substring(0, p);
                    path = path.Substring(p + 1);
                }
                else
                {
                    current += path;
                    path = "";
                }

                try
                {
                    SftpFileAttributes attrs = client.GetAttributes(current);
                    if (!attrs.IsDirectory)
                    {
                        throw new Exception("not directory");
                    }
                }
                catch (SftpPathNotFoundException)
                {
                    client.CreateDirectory(current);
                }
            }
        }
        public async Task<string> DownloadFile(string pathRemoteFile)
        {
            return await Task.Run<string>(delegate ()
            {
                string base64 = "";
                using (SftpClient sftp = new SftpClient(_sftpConfiguration.SftpHost, _sftpConfiguration.SftpPort, _sftpConfiguration.SftpUsername, _sftpConfiguration.SftpPassword))
                {
                    sftp.Connect();
                    using (var stream = new MemoryStream())
                    {
                        sftp.DownloadFile($"{_sftpConfiguration.SftpPathBase}{pathRemoteFile}", stream);
                        byte[] imageBytes = stream.ToArray();
                        base64 = Convert.ToBase64String(imageBytes);
                    }
                    sftp.Disconnect();
                }
                return base64;
            });
        }
        public async Task ListAsync(string remoteDirectory)
        {
            await Task.Run(delegate ()
            {
                using (SftpClient sftp = new SftpClient(_sftpConfiguration.SftpHost, _sftpConfiguration.SftpPort, _sftpConfiguration.SftpUsername, _sftpConfiguration.SftpPassword))
                {
                    try
                    {
                        sftp.Connect();
                        var files = sftp.ListDirectory(remoteDirectory);
                        foreach (var file in files)
                        {
                            Console.WriteLine(file.Name);
                        }
                        sftp.Disconnect();
                    }
                    catch (Exception er)
                    {
                    }
                }
            });
        }
    }
}
