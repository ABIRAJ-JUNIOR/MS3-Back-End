using MS3_Back_End.DTOs.Email;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class SendMailService
    {
        private readonly SendMailRepository _sendMailRepository;
        private readonly EmailServiceProvider _emailServiceProvider;

        public SendMailService(SendMailRepository sendMailRepository, EmailServiceProvider emailServiceProvider)
        {
            _sendMailRepository = sendMailRepository;
            _emailServiceProvider = emailServiceProvider;
        }

        //OTP Mail 
        public async Task<string> OtpMail(SendOtpMailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            var bodyGenerated = await OtpEmailBodyGenerate(template.Body, sendMailRequest.Name, sendMailRequest.Otp);

            MailModel mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated ?? string.Empty,
                SenderName = "Way Makers",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);

            return "email was sent successfully";
        }

        public async Task<string> OtpEmailBodyGenerate(string emailbody, string? name = null, string? otp = null)
        {
            var replacements = new Dictionary<string, string?>()
            {
                {"{Name}", name},
                {"{Otp}" , otp}
            };

            foreach (var replace in replacements)
            {
                if (!string.IsNullOrEmpty(replace.Value))
                {
                    emailbody = emailbody.Replace(replace.Key, replace.Value, StringComparison.OrdinalIgnoreCase);
                }
            }

            return emailbody;
        }


        //Invoice Mail
        public async Task<string> InvoiceMail(SendInvoiceMailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            var bodyGenerated = await InvoiceEmailBodyGenerate(template.Body,sendMailRequest).ConfigureAwait(false);

            MailModel mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated ?? string.Empty,
                SenderName = "Way Makers",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);

            return "email was sent successfully";
        }

        public async Task<string> InvoiceEmailBodyGenerate(string emailbody ,SendInvoiceMailRequest sendMailRequest)
        {
            var replacements = new Dictionary<string, string?>()
            {
                {"{InvoiceId}", sendMailRequest.InvoiceId.ToString()},
                {"{StudentName}", sendMailRequest.StudentName},
                {"{StudentId}" , sendMailRequest.StudentId.ToString()},
                {"{Email}" , sendMailRequest.Email},
                {"{Address}" , sendMailRequest.Address},
                {"{CourseName}" , sendMailRequest.CourseName},
                {"{Amount}" , sendMailRequest.AmountPaid.ToString()},
                {"{PaymentType}" , sendMailRequest.PaymentType},
            };

            foreach (var replace in replacements)
            {
                if (!string.IsNullOrEmpty(replace.Value))
                {
                    emailbody = emailbody.Replace(replace.Key, replace.Value, StringComparison.OrdinalIgnoreCase);
                }
            }

            return emailbody;
        }



        //Contact Us Message
        public async Task<string> MessageMail(SendMessageMailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            var bodyGenerated = await MessageEmailBodyGenerate(template.Body, sendMailRequest).ConfigureAwait(false);

            MailModel mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated ?? string.Empty,
                SenderName = "Way Makers",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);
            return "email was sent successfully";
        }

        public async Task<string> MessageEmailBodyGenerate(string emailbody, SendMessageMailRequest sendMailRequest)
        {
            var replacements = new Dictionary<string, string?>()
            {
                {"{Name}", sendMailRequest.Name},
                {"{UserMessage}", sendMailRequest.UserMessage},
            };

            foreach (var replace in replacements)
            {
                if (!string.IsNullOrEmpty(replace.Value))
                {
                    emailbody = emailbody.Replace(replace.Key, replace.Value, StringComparison.OrdinalIgnoreCase);
                }
            }
            return emailbody;
        }
    }
}
