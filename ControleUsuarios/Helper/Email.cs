using System.Net;
using System.Net.Mail;

namespace ControleUsuarios.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;
    
        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string nome = _configuration.GetValue<string>("SMTP:Nome");
                string username = _configuration.GetValue<string>("SMTP:Username");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                int porta = _configuration.GetValue<int>("SMTP:Porta");

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(username, nome)
                };

                mailMessage.To.Add(email);
                mailMessage.Subject = assunto;
                mailMessage.Body = mensagem;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                using (var smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(username, senha);
                    smtp.EnableSsl = true;
                    
                    smtp.Send(mailMessage);

                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}