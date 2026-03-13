using DPUruNet;
using System;
using System.Drawing;
using System.IO;

namespace IS.DocumenFormater.utilities.Biometric
{
    public class WSQConvert
    {
        public static string ConvertToJpg(String base64)
        {
            return ConvertToJpg(Convert.FromBase64String(base64));
        }

        public static string ConvertToJpg(byte[] file)
        {
            string jpg64 = "";
            byte[] wsqFile = file;// File.ReadAllBytes(@"C:\Users\Legion\source\Workspaces\IdentitySign\Utils\WSQ\WSQ_Microsoft_CSharp_2017_64\WSQ_Microsoft_CSharp_2017_64\bin\x64\Release\sample_image.wsq");
            Compression.Start();
            RawImage rawImage = Compression.ExpandRaw(wsqFile, CompressionAlgorithm.COMPRESSION_WSQ_NIST);
            Compression.Finish();
            Bitmap bmp = DrawManager.CreateBitmap(rawImage.Data, rawImage.Width, rawImage.Height);
            using (MemoryStream stream = new MemoryStream())
            {
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = stream.ToArray();
                jpg64 = Convert.ToBase64String(imageBytes);
            }
            //bmp.Save(@"C:\Users\Legion\Desktop\Sample.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return jpg64;
        }

        public static string ConvertToMinucia(String base64)
        {
            return ConvertToMinucia(Convert.FromBase64String(base64));
        }
        public static string ConvertToMinucia(byte[] file)
        {
            string jpg64 = "";
            byte[] wsqFile = file;
            Compression.Start();
            RawImage rawImage = Compression.ExpandRaw(wsqFile, CompressionAlgorithm.COMPRESSION_WSQ_NIST);
            Compression.Finish();

            DataResult<Fmd> fmd = FeatureExtraction.CreateFmdFromRaw(rawImage.Data, 2, 51, rawImage.Width, rawImage.Height, 500, Constants.Formats.Fmd.ANSI);
            jpg64 = Convert.ToBase64String(fmd.Data.Bytes);
            return jpg64;
        }
    }
}
