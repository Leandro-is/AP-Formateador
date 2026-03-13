using System;

namespace IS.DocumenFormater.api.Security.Exchange
{
    public interface ITripleDESConfiguration
    {
        String IV { get; set; }
        String sEncryptionKey { get; set; }
    }
}
