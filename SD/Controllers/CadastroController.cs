using System.Web.Mvc;
using SD.Models;
namespace SD.Controllers
{
    public class CadastroController : Controller
    {
        //Retorna a view Index (Pagina de criacao de conta)
        public ActionResult Index()
        {
            return View();
        }

        //Retorna a view Recuperacao (Pagina de recuperacao de senha)
        public ActionResult Recuperacao()
        {
            return View();
        }


        
        public JsonResult Cadastro(string nome, string email, string senha)
        {
            return Json(operador.criarConta(nome, email, senha), JsonRequestBehavior.AllowGet);
        }

        /*public JsonResult CadastroOld(string nome, string email, string senha)
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
        }*/

        
        public JsonResult Recuperar(string email)
        {
            return Json(operador.recuperarConta(email), JsonRequestBehavior.AllowGet);
        }
        /*public JsonResult RecuperarOld(string email)
        {
            var feedback = new { status = "", mensagem = "" };
            string senha = null;
            SqlDataReader reader = DBCon.Read("select senha from users where email='" + email + "'");
            while (reader.Read())
            {
                senha = reader.GetString(0).Trim();
            }
            MailServer ms = new MailServer("smtp.gmail.com", 587, "meuddd.app@gmail.com", "4cess0!DDD");

            if (senha != null)
            {
                feedback = new { status = "ok", mensagem = "Mensagem de recuperação encaminhada" };
            }
            else
            {
                feedback = new { status = "erro", mensagem = "Email não cadastrado" };
            }

            try
            {
                ms.enviarEmail("meuddd.app@gmail.com", email, "Recuperacao de senha", "Para acesso ao site, por favor utilize os dados abaixo:<br>Seu email: " + email + "<br>Sua senha: <b>" + senha + "</b>");
            }
            catch (FormatException ex)
            {
                feedback = new { status = "erro", mensagem = "Formato de email invalido" };
            }

            reader.Close();
            return Json(feedback, JsonRequestBehavior.AllowGet);
        }*/
    }
}