using System;
using System.Collections.Generic;
using System.IO;

namespace IS.DocumenFormater.utilities.Email.Domain
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
            FromAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> FromAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsHTML { get; set; }
        public bool HaveAttrachment { get; set; }
        public List<String> Categories { get; set; }
        public List<FileMessageAttachment> FilesAttachment { get; set; }
    }
    public class FileMessageAttachment
    {
        public String FileName { get; set; }
        public Stream FileStream { get; set; }
        public String FileBase64 { get; set; }
        public String ContentType { get; set; }
        public String Extension { get; set; }
    }
}
