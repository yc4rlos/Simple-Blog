using System.Net;
using System.Net.Mail;

namespace Blog.Infrastructure.Services.Services;

public class EmailService(IOptions<EmailOptions> options) : IEmailService
{
    private readonly EmailOptions _emailOptions = options.Value;

    public Task SendNewUserMailAsync(User user, string password)
    {
        var body = "<h1>Welcome to the blog!</h1><br>" +
                   $"<p>Your user was created and your password for first login is: <b>{password}</b></p>";

        var mailMessage = new MailMessage
        {
            To = { user.Email },
            IsBodyHtml = true,
            Body = body,
            Subject = "Blog - User Created",
        };
        
        return SendMailAsync(mailMessage);
    }

    public Task SendTemporaryPasswordMailAsync(User user, string password)
    {
        var body = "<h1>Password Recovery</h1><br>" +
                   $"<p>Your password was reset and you can login with the following password: <b>{password}</b></p>";

        var mailMessage = new MailMessage
        {
            To = { user.Email },
            IsBodyHtml = true,
            Body = body,
            Subject = "Blog - Password Reset",
        };
        
        return SendMailAsync(mailMessage);
    }

    private async Task SendMailAsync(MailMessage message)
    {
        using var client = new SmtpClient(_emailOptions.Host, _emailOptions.Port);
        var credentials = new NetworkCredential(_emailOptions.Email, _emailOptions.Password);

        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
        client.Credentials = credentials;
        
        message.From = new MailAddress(_emailOptions.Email);
        
        await client.SendMailAsync(message);
    }
}