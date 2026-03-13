using System;

namespace IS.DocumenFormater.api.Security.Exchange
{
    public class TripleDESConfiguration : ITripleDESConfiguration
    {
        public String IV { get; set; }
        public String sEncryptionKey { get; set; }
    }
}
