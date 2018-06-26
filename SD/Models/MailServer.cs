using System.Net.Mail;
using System.Net;
using System.Text;
using System.Diagnostics;


namespace SD.Models
{

    public class MailServer

    {
        private SmtpClient MailClient;

        public MailServer(string servidor, int porta, string usuario, string senha)
        {
            MailClient = new SmtpClient(servidor, porta);
            MailClient.UseDefaultCredentials = false;
            NetworkCredential basicAuthenticationInfo = new NetworkCredential(usuario, senha);
            MailClient.Credentials = basicAuthenticationInfo;
        }

        public void enviarEmail(string remetente, string destinatario, string assunto, string mensagem)
        {
            Debug.WriteLine("Enviando Mensagem");
            MailMessage email = new MailMessage(remetente, destinatario);
            email.Subject = assunto;
            email.SubjectEncoding = Encoding.UTF8;
            email.Body = mensagem;
            email.BodyEncoding = Encoding.UTF8;
            email.IsBodyHtml = true;
            try
            {
                MailClient.Send(email);
            }
            catch (SmtpException ex)
            {
                Debug.WriteLine("Erro no envio:" + ex.StatusCode);
            }

        }
    }
}