using IS.DocumenFormater.utilities.Email.Domain;
using System.Threading.Tasks;

namespace IS.DocumenFormater.utilities.Email
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);
    }
}
