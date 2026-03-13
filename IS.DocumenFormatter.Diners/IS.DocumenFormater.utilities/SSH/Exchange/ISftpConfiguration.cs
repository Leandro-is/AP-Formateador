using System;

namespace IS.DocumenFormater.utilities.SSH.Exchange
{
    public interface ISftpConfiguration
    {
        String SftpHost { get; set; }
        int SftpPort { get; set; }
        String SftpPathBase { get; set; }
        String SftpUsername { get; set; }
        String SftpPassword { get; set; }
    }
}
