using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.PDF417;

namespace IS.DocumenFormater.utilities.BarCode
{
    public static class BarCode
    {
        public static String CreateQRCode(string content)
        {
            String base64 = "";
            //IBarcodeWriter writer = new BarcodeWriter
            //{
            //    Format = BarcodeFormat.QR_CODE,
            //    Options = new QrCodeEncodingOptions
            //    {
            //        Width = 250,
            //        Height = 250,
            //    }
            //};
            //using (var qrCodeImage = writer.Write("asdas"))
            //{
            //    using (var stream = new MemoryStream())
            //    {
            //        qrCodeImage.Save(stream, ImageFormat.Png);
            //        byte[] imageBytes = stream.ToArray();
            //        base64 = Convert.ToBase64String(imageBytes);
            //    }
            //}
            return base64;
        }
        public static String CreateBarCode417(string content)
        {
            //var options = new PDF417EncodingOptions { Margin = 10 };
            //var bar = new ZXing.BarcodeWriter();
            //bar.Options = options;
            //bar.Format = ZXing.BarcodeFormat.PDF_417;
            //var barResult = new Bitmap(bar.Write(text));
            //String barImage = "";
            //using (var bsm = new MemoryStream())
            //{
            //    barResult.Save(bsm, System.Drawing.Imaging.ImageFormat.Png);
            //    barImage = Convert.ToBase64String(bsm.ToArray());
            //}
            //return barImage;



            //String base64 = "";
            //BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, content);
            //using (var stream = new MemoryStream())
            //{
            //    generator.Save(stream, BarCodeImageFormat.Jpeg);
            //    byte[] imageBytes = stream.ToArray();
            //    base64 = Convert.ToBase64String(imageBytes);
            //}

            //return base64;

            String base64 = "";
            var hints = new PDF417EncodingOptions
            {
                Margin = 0
            };
            const int size = 64;
            var writer = new PDF417Writer();
            var bitMatrix = writer.encode(content, BarcodeFormat.PDF_417, size, size, hints.Hints);
            int width = bitMatrix.Width;
            int height = bitMatrix.Height;

            var bmp = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmp.SetPixel(x, y, bitMatrix[x, y] ? Color.Black : Color.White);
                }
            }
            using (var stream = new MemoryStream())
            {
                bmp.Save(stream, ImageFormat.Png);
                byte[] imageBytes = stream.ToArray();
                base64 = Convert.ToBase64String(imageBytes);
            }
            return base64;



            //String base64 = "";
            //BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>
            //{
            //    Renderer = new ZXing.Rendering.PixelDataRenderer(),
            //    Format = BarcodeFormat.PDF_417,
            //    Options = new QrCodeEncodingOptions
            //    {
            //        Width = 100,
            //        Height = 100,
            //    }
            //};
            ////var reder = new  BitmapRenderer()
            //using (var qrCodeImage = writer.Write(content))
            //{
            //    using (var stream = new MemoryStream())
            //    {
            //        qrCodeImage.Save(stream, ImageFormat.Png);
            //        byte[] imageBytes = stream.ToArray();
            //        base64 = Convert.ToBase64String(imageBytes);
            //    }
            //}
            //return base64;
        }
    }
}
