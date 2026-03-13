using System;

namespace IS.DocumenFormater.utilities.SSH.Exchange
{
    public class SftpConfiguration : ISftpConfiguration
    {
        public String SftpHost { get; set; }
        public int SftpPort { get; set; }
        public String SftpPathBase { get; set; }
        public String SftpUsername { get; set; }
        public String SftpPassword { get; set; }
    }
}
