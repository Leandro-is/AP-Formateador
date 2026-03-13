using DPUruNet;
using System;
using System.Drawing;
using System.IO;

namespace IS.DocumenFormater.utilities.Biometric
{
    public static class RawConvert
    {

        public static string ToJpg(string rawBase64, int width, int height)
        {
            return ToJpg(Convert.FromBase64String(rawBase64), width, height);
        }

        public static string ToJpg(byte[] rawBytes, int width, int height)
        {
            string jpg64 = "";
            Bitmap bmp = DrawManager.CreateBitmap(rawBytes, width, height);
            using (MemoryStream stream = new MemoryStream())
            {
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = stream.ToArray();
                jpg64 = Convert.ToBase64String(imageBytes);
            }
            //bmp.Save(@"D:\Sample.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return jpg64;
        }

        public static string ToMinutia(string rawBase64, int rawWidth, int rawHeight)
        {
            byte[] rawBytes = Convert.FromBase64String(rawBase64);
            DataResult<Fmd> fmd = FeatureExtraction.CreateFmdFromRaw(rawBytes, 2, 51, rawWidth, rawHeight, 500, DPUruNet.Constants.Formats.Fmd.ANSI);
            return Convert.ToBase64String(fmd.Data.Bytes);
        }
    }
}
