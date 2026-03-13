namespace IS.DocumenFormater.utilities.Email.Exchange
{
    public interface IEmailConfiguration
    {
        string SendgridApiKey { get; set; }
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }

        string PopServer { get; }
        int PopPort { get; }
        string PopUsername { get; }
        string PopPassword { get; }
    }
}
