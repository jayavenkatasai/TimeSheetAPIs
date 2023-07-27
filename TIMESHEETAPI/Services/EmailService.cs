using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace TIMESHEETAPI.Services
{
	public class emailService
	{
		public interface IEmailService
		{
			Task SendVerificationEmailAsync(string email, string verificationToken);
            Task SendOtpVerificationEmailAsync(string email, string verificationToken);
        }

		public class EmailService : IEmailService
		{
			public async Task SendVerificationEmailAsync(string email, string verificationToken)
			{
				try
				{
					var message = new MimeMessage();
					message.From.Add(MailboxAddress.Parse("skyla.jacobs@ethereal.email"));
					message.To.Add(MailboxAddress.Parse(email));
					message.Subject = "Account Verification";

					// Build the email body with the verification link containing the token
					var bodyBuilder = new BodyBuilder();
					bodyBuilder.HtmlBody = $"<p>Hello,</p><p>Please click the following link to verify your account:</p><h1>{verificationToken}</h1>";

					message.Body = bodyBuilder.ToMessageBody();

					using (var client = new SmtpClient())
					{
						client.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
						client.Authenticate("skyla.jacobs@ethereal.email", "xubBMe7mE9F8RFjmG6");

						await client.SendAsync(message);
						client.Disconnect(true);
					}
				}
				catch (Exception ex)
				{
					// Handle exceptions or log errors related to sending emails
					Console.WriteLine("Error sending email: " + ex.Message);
				}

			}
            public async Task SendOtpVerificationEmailAsync(string email, string verificationToken)
            {
                try
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse("skyla.jacobs@ethereal.email"));
                    message.To.Add(MailboxAddress.Parse(email));
                    message.Subject = "Otp Verification";

                    // Build the email body with the verification link containing the token
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = $"<p>Hello,</p><p>Please click the following link to verify your account:</p><h1>{verificationToken}</h1>";

                    message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                        client.Authenticate("skyla.jacobs@ethereal.email", "xubBMe7mE9F8RFjmG6");

                        await client.SendAsync(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or log errors related to sending emails
                    Console.WriteLine("Error sending email: " + ex.Message);
                }

            }
        }
	}
}
