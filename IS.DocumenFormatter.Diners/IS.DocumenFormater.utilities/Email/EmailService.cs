using IS.DocumenFormater.utilities.Email.Domain;
using IS.DocumenFormater.utilities.Email.Exchange;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS.DocumenFormater.utilities.Email
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async Task Send(EmailMessage emailMessage)
        {
            var client = new SendGridClient(_emailConfiguration.SendgridApiKey);
            var from = emailMessage.FromAddresses.Select(x => new SendGrid.Helpers.Mail.EmailAddress(x.Address, x.Name)).FirstOrDefault();
            var subject = emailMessage.Subject;
            var to = emailMessage.ToAddresses.Select(x => new SendGrid.Helpers.Mail.EmailAddress(x.Address, x.Name)).FirstOrDefault();
            var plainTextContent = "";
            var htmlContent = emailMessage.Content;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            msg.AddCategories(emailMessage.Categories);
            if (emailMessage.HaveAttrachment)
            {
                List<Attachment> attachs = new List<Attachment>();
                foreach (var file in emailMessage.FilesAttachment)
                {
                    attachs.Add(new Attachment()
                    {
                        Content = file.FileBase64,
                        Disposition = ContentDisposition.Attachment,
                        Filename = file.FileName,
                    });
                }
                msg.Attachments = attachs;
            }
            var response = await client.SendEmailAsync(msg);
        }

        //public void Send(EmailMessage emailMessage)
        //{

        //    var message = new MimeMessage();
        //    message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
        //    message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

        //    message.Subject = emailMessage.Subject;

        //    var multipart = new Multipart("mixed");

        //    var body = new TextPart(emailMessage.IsHTML ? "html" : "plain")
        //    {
        //        Text = emailMessage.Content
        //    };
        //    multipart.Add(body);

        //    if (emailMessage.HaveAttrachment)
        //    {
        //        foreach (var file in emailMessage.FilesAttachment)
        //        {
        //            var attachment = new MimePart(file.ContentType, file.Extension)
        //            {
        //                Content = new MimeContent(file.FileStream),
        //                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
        //                ContentTransferEncoding = ContentEncoding.Base64,
        //                FileName = file.FileName
        //            };
        //            multipart.Add(attachment);
        //        }
        //    }

        //    message.Body = multipart;

        //    using (var emailClient = new SmtpClient())
        //    {
        //        emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);
        //        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
        //        emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
        //        emailClient.Send(message);
        //        emailClient.Disconnect(true);
        //    }
        //}
    }
}
