using IS.DocumenFormater.api.Extensions;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace IS.DocumenFormater.api.Factories
{
    public static class Constants
    {
        public static readonly String CompanyName = "In Solutions S.A.C.";
        public static readonly String ApplicationName = "Identity Sign";
        public static readonly String ProductName = "Sign Contract";

        public static readonly bool ProtectorActivated = true;
        public static readonly String ApplicationNameTitle = ApplicationName.FirstCharToUpper();
        public static readonly String KeyProtector = "$$1nS0lut10n.1DP$$";
        public static readonly String CDNPath = "cdn";
        public static readonly String ImagesExtension = ".jpg";
        public static readonly String TotalAccessRoles = "Admin";
        public static readonly String ConnectionStringName = "DB_Connection_DocumentFormater";

        public static readonly String RUC = "20202380621";
        public static readonly String RazonSocial = "Firmalo S.A.";
        public static readonly String TextSign = "Firmado";
        public static readonly String TextDigitallyBy = "digitalmente por";

        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var pathRoot = appPathMatcher.Match(exePath).Value;
            return pathRoot;
        }
    }
}
