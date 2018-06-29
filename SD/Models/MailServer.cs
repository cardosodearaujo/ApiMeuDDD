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
            MailClient.EnableSsl = true;
            NetworkCredential basicAuthenticationInfo = new NetworkCredential(usuario, senha);
            MailClient.Credentials = basicAuthenticationInfo;
        }

        public void enviarEmail(string remetente, string destinatario, string assunto, string mensagem)
        {

            MailMessage email = new MailMessage(new MailAddress(remetente,"MeuDDD App"), new MailAddress(destinatario));
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