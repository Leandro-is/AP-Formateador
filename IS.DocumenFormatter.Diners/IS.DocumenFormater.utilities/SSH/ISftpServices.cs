using System.Threading.Tasks;

namespace IS.DocumenFormater.utilities.SSH
{
    public interface ISftpServices
    {
        Task<bool> UploadFile(string fileBase64, string pathFile);
        Task<bool> UploadFile(byte[] file, string pathFile);
        Task<string> DownloadFile(string pathRemoteFile);
        Task ListAsync(string remoteDirectory);
    }
}
