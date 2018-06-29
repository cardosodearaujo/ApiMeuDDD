using System.Web.Mvc;
using SD.Models;
using System.Data.SqlClient;

namespace SD.Controllers
{
    public class CadastroController : Controller
    {
        // GET: Cadastro
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Cadastro(string nome, string email, string senha)
        {
            string retorno = DBCon.Exec("insert into users (nome,email,senha) values ('" + nome + "','" + email + "','" + senha + "')");
            var feedback = new { status = "", mensagem = "" };
            switch (retorno)
            {
                case "0":
                    feedback = new { status = "ok", mensagem = "Cadastro realizado com sucesso" };
                    break;

                case "2627":
                    feedback = new { status = "erro", mensagem = "Email já cadastrado" };
                    break;
                default:
                    feedback = new { status = "erro", mensagem = "Erro inesperado" };
                    break;
            }
            return Json(feedback, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Recuperar(string email)
        {
            var feedback = new { status = "", mensagem = "" };
            string senha = null;
            SqlDataReader reader = DBCon.Read("select senha from users where email='" + email + "'");
            while (reader.Read())
            {
                senha = reader.GetString(0).Trim();
            }
            MailServer ms = new MailServer("localhost", 25, "contato@meuddd.com.br", "123*abc");
            ms.enviarEmail("contato@meuddd.com.br", email, "Recuperacao de senha", "Sua senha é: <b>" + senha + "</b>");
            if (senha != null)
            {
                feedback = new { status = "ok", mensagem = "Mensagem de recuperação encaminhada" };
            }
            else
            {
                feedback = new { status = "erro", mensagem = "Email não cadastrado" };
            }
            reader.Close();
            return Json(feedback, JsonRequestBehavior.AllowGet);
        }

    }
}