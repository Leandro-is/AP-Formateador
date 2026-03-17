using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace IS.DocumenFormater.utilities.iPdf
{
    public class PdfWorker
    {

        public static (string, string) DividirOracionSinCortarPalabras(string input, int maxCaracteres)
        {
            if (input.Length <= maxCaracteres)
            {
                return (input, string.Empty);
            }

            int index = input.LastIndexOf(' ', maxCaracteres);

            if (index == -1)
            {
                index = maxCaracteres;
            }

            string direccionParte1 = input.Substring(0, index).Trim();
            string direccionParte2 = input.Substring(index).Trim();


            return (direccionParte1, direccionParte2);
        }

        public static (string, string) DividirOracionCortandoPalabras(string input, int maxCaracteres)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (string.Empty, string.Empty);

            input = input.Trim();

            if (input.Length <= maxCaracteres)
                return (input.ToUpper(), string.Empty);

            string parte1 = input.Substring(0, maxCaracteres).Trim().ToUpper();
            string parte2 = input.Substring(maxCaracteres).Trim().ToUpper();

            return (parte1, parte2);
        }


        public static String AddPage(String pdfInput, out int numberOfPages)
        {
            String pdfOuput;
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                byte[] pdf = Convert.FromBase64String(pdfInput);

                //ADD PAGE
                PdfReader reader = new PdfReader(pdf);
                PdfStamper stamper = new PdfStamper(reader, myMemoryStream);
                numberOfPages = reader.NumberOfPages + 1;
                stamper.InsertPage(numberOfPages, reader.GetPageSizeWithRotation(1));
                stamper.Close();
                reader.Close();

                byte[] content = myMemoryStream.ToArray();

                pdfOuput = Convert.ToBase64String(content);

                //WRITE
                //using (FileStream fs = File.Create(@"C:\Dev\Test\Files\test_blank.pdf"))
                //{
                //    fs.Write(content, 0, (int)content.Length);
                //}
            }
            return pdfOuput;
        }

        public static void GetMaxPageNumber(String pdfInput, out int numberOfPages)
        {
            byte[] pdf = Convert.FromBase64String(pdfInput);

            //ADD PAGE
            PdfReader reader = new PdfReader(pdf);
            numberOfPages = reader.NumberOfPages;
            reader.Close();
        }

        public static String WriteImageInPdf(String pdfbase64, String image, int page, int fromX, int fromY, int width, int height)
        {
            byte[] decodedPdf = Convert.FromBase64String(pdfbase64);
            Image imageFP = Image.GetInstance(Convert.FromBase64String(image));
            return WriteImageInPdf(decodedPdf, imageFP, page, fromX, fromY, width, height);
        }
        public static String WriteImageInPdf(String pdfbase64, Image image, int page, int fromX, int fromY, int width, int height)
        {
            byte[] decodedPdf = Convert.FromBase64String(pdfbase64);
            return WriteImageInPdf(decodedPdf, image, page, fromX, fromY, width, height);
        }
        public static String WriteImageInPdf(byte[] pdfBase, Image image, int page, int fromX, int fromY, int width, int height)
        {
            String base64 = "";
            PdfReader pdfReader = new PdfReader(pdfBase);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader.unethicalreading = true;
                PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);
                PdfContentByte pdfContentByte = pdfStamper.GetOverContent(page);

                image.SetAbsolutePosition(fromX, fromY);
                image.ScaleAbsoluteWidth(width);
                image.ScaleAbsoluteHeight(height);
                pdfContentByte.AddImage(image);

                pdfStamper.Close();
                pdfReader.Close();
                base64 = Convert.ToBase64String(stream.ToArray());
            }
            return base64;
        }
        public static String WriteTextInPdf(String pdfbase64, String text, int page, int fontsize, int alignment, int fromX, int fromY, float characterSpacing)
        {
            byte[] decodedPdf = Convert.FromBase64String(pdfbase64);
            return WriteTextInPdf(decodedPdf, text, page, fontsize, alignment, fromX, fromY, 0, characterSpacing);
        }
        public static String WriteTextInPdf(String pdfbase64, String text, int page, int fontsize, int alignment, int fromX, int fromY)
        {
            byte[] decodedPdf = Convert.FromBase64String(pdfbase64);
            return WriteTextInPdf(decodedPdf, text, page, fontsize, alignment, fromX, fromY, 0);
        }
        public static String WriteTextInPdf(byte[] pdfBase, String text, int page, int fontsize, int alignment, int fromX, int fromY)
        {
            return WriteTextInPdf(pdfBase, text, page, fontsize, alignment, fromX, fromY, 0);
        }
        public static String WriteTextInPdf(String pdfbase64, String text, int page, int fontsize, int alignment, int fromX, int fromY, int rotation)
        {
            byte[] decodedPdf = Convert.FromBase64String(pdfbase64);
            return WriteTextInPdf(decodedPdf, text, page, fontsize, alignment, fromX, fromY, rotation);
        }
        public static String WriteTextInPdf(byte[] pdfBase, String text, int page, int fontsize, int alignment, int fromX, int fromY, int rotation, float characterSpacing = 0.0f)
        {
            String base64 = "";
            PdfReader pdfReader = new PdfReader(pdfBase);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader.unethicalreading = true;
                PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);
                PdfContentByte pdfContentByte = pdfStamper.GetOverContent(page);

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                pdfContentByte.SetColorFill(BaseColor.DARK_GRAY);
                pdfContentByte.SetFontAndSize(bf, fontsize);
                pdfContentByte.SetCharacterSpacing(characterSpacing);

                pdfContentByte.BeginText();
                pdfContentByte.ShowTextAligned(alignment, text, fromX, fromY, rotation);
                pdfContentByte.EndText();

                pdfStamper.Close();
                pdfReader.Close();
                base64 = Convert.ToBase64String(stream.ToArray());
            }
            return base64;
        }
        public static String DrawRectangleInPdf(String pdfbase64, int page, double x, double y, double w, double h)
        {
            byte[] decodedPdf = Convert.FromBase64String(pdfbase64);
            return DrawRectangleInPdf(decodedPdf, page, x, y, w, h);
        }
        public static String DrawRectangleInPdf(byte[] pdfBase, int page, double x, double y, double w, double h)
        {
            String base64 = "";
            PdfReader pdfReader = new PdfReader(pdfBase);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader.unethicalreading = true;
                PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);
                PdfContentByte pdfContentByte = pdfStamper.GetOverContent(page);

                pdfContentByte.Rectangle(x, y, w, h);
                pdfContentByte.Stroke();

                pdfStamper.Close();
                pdfReader.Close();
                base64 = Convert.ToBase64String(stream.ToArray());
            }
            return base64;
        }

        public static String DrawLineInPdf(String pdfbase64, int page, double x, double y, double x2, double y2)
        {
            byte[] decodedPdf = Convert.FromBase64String(pdfbase64);
            return DrawLineInPdf(decodedPdf, page, x, y, x2, y2);
        }
        public static String DrawLineInPdf(byte[] pdfBase, int page, double x, double y, double x2, double y2)
        {
            String base64 = "";
            PdfReader pdfReader = new PdfReader(pdfBase);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader.unethicalreading = true;
                PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);
                PdfContentByte pdfContentByte = pdfStamper.GetOverContent(page);

                pdfContentByte.MoveTo(x, y);
                pdfContentByte.LineTo(x2, y2);
                pdfContentByte.Stroke();

                pdfStamper.Close();
                pdfReader.Close();
                base64 = Convert.ToBase64String(stream.ToArray());
            }
            return base64;
        }
    }
}
