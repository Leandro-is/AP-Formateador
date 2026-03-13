using System;

namespace IS.DocumenFormater.api.Logging
{
    public class DBLoggerTrace
    {
        public String EntityId { get; set; }
        public String EntityName { get; set; }
        public String EntityField { get; set; }
        public String EntityValue { get; set; }
    }
}
