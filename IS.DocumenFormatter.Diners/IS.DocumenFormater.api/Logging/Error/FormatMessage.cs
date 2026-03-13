using System.Text;

namespace IS.DocumenFormater.api.Logging.Errors
{
    public class FormatMessage
    {
        private StringBuilder ExtraData = new StringBuilder();
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Actions { get; set; }
        public override string ToString()
        {
            string resultado = string.Format(
                    "[General Information]\r\n" +
                    "Title=\"{0}\"\r\n" +
                    "Detail=\"{1}\"\r\n" +
                    "Actions=\"{2}\"\r\n" +
                    "[Extra Data]\r\n" +
                    "{3}",
                    Title.Replace("\"", "\\\""), Detail, Actions, ExtraData.ToString());

            return resultado;
        }
        public void AddExtraData(string nombre, string valor)
        {
            ExtraData.Append(string.Format("{0}=\"{1}\"\r\n", nombre, valor));
        }
        public void AddTitleExtraData(string Titulo)
        {
            ExtraData.Append(string.Format("\r\n[{0}]\r\n", Titulo));
        }
    }
}
