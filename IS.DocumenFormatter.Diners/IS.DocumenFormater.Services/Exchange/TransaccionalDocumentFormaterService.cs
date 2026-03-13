using IS.DocumenFormater.Repository.Domain;
using IS.DocumenFormater.Repository.Exchange;

namespace IS.DocumenFormater.Services.Exchange
{
    public class TransaccionalDocumentFormaterService : GenericService<TransaccionalDocumentFormater>, ITransaccionalDocumentFormaterService
    {
        public TransaccionalDocumentFormaterService(ITransaccionalDocumentFormaterRepository repository) : base(repository)
        {
        }
    }
}
