

using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Crypto.Engines;

namespace Shared.Core.Services.Emails;

public class EmailSender_MailKit : IEmailSender
{
    private readonly EmailSenderOptions _options;
    public EmailSender_MailKit(EmailSenderOptions options) => _options = options;
    public EmailSender_MailKit(IOptionsSnapshot<EmailSenderOptions> options) => _options = options.Value;

    public async Task<bool> SendEmail(string Email, string content, string Subject)
    {
        var emailObj = new Email
        {
            From = new EmailPaticipant(_options.Email, _options.SenderName),
            Subject = Subject,
            Content = content,
            To = new List<EmailPaticipant> { new EmailPaticipant(Email, Email) },
        };
        return await this.SendEmail(emailObj); 
    }
    public Task<bool> SendEmail(Email Email)
    {
        try
        {
            var message = GetMessage(Email, _options);

            var attachment = new MimePart();
            Multipart body = new Multipart(Email.HasAttachments ? "mixed" : "plain") { message.Body };
            if (Email.HasAttachments)
            {
                if (Email.Attachements.Any(file => !_options.AllowedContentType.Split(',').Select(x => x.Trim()).Contains(file.ContentType))) return Task.FromResult(false);

                var attachments = Email.Attachements.ToList();
                var i = 0;
                attachments.ForEach(a =>
                {
                    attachment = new MimePart
                    {
                        Content = new MimeContent(a.File),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = a.Name,
                        IsAttachment = Email.HasAttachments
                    };
                    i++;
                    body.Add(attachment);
                }
                );
            }
            message.Body = body;

            Send(message);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(false);
            throw;
        }
    }
    private void Send(MimeMessage message)
    {
        using var emailClient = new SmtpClient();

        if (_options != null)
        {
            if (_options.EmailDomain == "smtp.gmail.com")
                emailClient.Connect(_options.EmailDomain, _options.Port,
                true);
            else
                emailClient.Connect(_options.EmailDomain, _options.Port,
                  MailKit.Security.SecureSocketOptions.Auto);

            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
            emailClient.Authenticate(_options.Email, _options.AppPassword);
            emailClient.Send(message);
            emailClient.Disconnect(true);
        }
    }
    private static MimeMessage GetMessage(Email Email, EmailSenderOptions _options)
    {
        var message = new MimeMessage();
        var fromEmail = MailboxAddress.Parse(_options!.Email);
        fromEmail.Name = Email.From.Name;
        message.From.Add(fromEmail);

        Email.To.ForEach(x =>
        {
            var t = MailboxAddress.Parse(x.Email);
            t.Name = x.Name;
            message.To.Add(t);
        });
        Email.Cc?.ForEach(x =>
        {
            var t = MailboxAddress.Parse(x.Email);
            t.Name = x.Name;
            message.Cc.Add(t);
        });
        Email.Bcc?.ForEach(x =>
        {
            var t = MailboxAddress.Parse(x.Email);
            t.Name = x.Name;
            message.Bcc.Add(t);
        });
        message.Subject = Email.Subject;


        var body = new TextPart("plain")
        {
            Text = Email.Content
        };
        message.Body = body;
        //message.HtmlBody= true;
        return message;
    }

}