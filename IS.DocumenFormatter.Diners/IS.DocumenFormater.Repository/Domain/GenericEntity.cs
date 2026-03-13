using System.Collections.Generic;

namespace IS.DocumenFormater.Repository.Domain
{
    public class GenericEntity<T>
    {
        public int Count { get; set; }
        public List<T> Enitities { get; set; }
    }
}
