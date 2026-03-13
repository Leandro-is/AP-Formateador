namespace IS.DocumenFormater.utilities.Biometric
{
    public class MinuciaConvert
    {
        //public static string ConvertToJpg(String base64)
        //{
        //    string jpg64 = "";
        //    Compression.Start();
        //    DataResult<Fmd> fmd = FeatureExtraction.CreateFmdFromFid(Fid..Fiv(file), Constants.Formats.Fmd.ANSI);

        //    RawImage rawImage = Compression.CompressFid(Fid.DeserializeXml(base64, CompressionAlgorithm.COMPRESSION_WSQ_NIST);
        //    Compression.Finish();
        //    Bitmap bmp = CreateBitmap(rawImage.Data, rawImage.Width, rawImage.Height);
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        byte[] imageBytes = stream.ToArray();
        //        jpg64 = Convert.ToBase64String(imageBytes);
        //    }
        //    //bmp.Save(@"C:\Users\Legion\Desktop\Sample.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        //    return jpg64;
        //}

        //static internal Bitmap CreateBitmap(byte[] bytes, int width, int height)
        //{
        //    byte[] rgbBytes = new byte[bytes.Length * 3];
        //    for (int i = 0; i <= bytes.Length - 1; i++)
        //    {
        //        rgbBytes[(i * 3)] = bytes[i];
        //        rgbBytes[(i * 3) + 1] = bytes[i];
        //        rgbBytes[(i * 3) + 2] = bytes[i];
        //    }
        //    Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        //    BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
        //    for (int i = 0; i <= bmp.Height - 1; i++)
        //    {
        //        IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
        //        System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
        //    }
        //    bmp.UnlockBits(data);
        //    return bmp;
        //}

        //public static string ConvertToMinucia(String base64)
        //{
        //    return ConvertToMinucia(Convert.FromBase64String(base64));
        //}
        //public static string ConvertToMinucia(byte[] file)
        //{
        //    string jpg64 = "";
        //    byte[] wsqFile = file;
        //    Compression.Start();
        //    RawImage rawImage = Compression.ExpandRaw(wsqFile, CompressionAlgorithm.COMPRESSION_WSQ_NIST);
        //    Compression.Finish();

        //    DataResult<Fmd> fmd = FeatureExtraction.CreateFmdFromRaw(rawImage.Data, 2, 51, rawImage.Width, rawImage.Height, 500, Constants.Formats.Fmd.ANSI);
        //    jpg64 = Convert.ToBase64String(fmd.Data.Bytes);
        //    return jpg64;
        //}
    }
}
