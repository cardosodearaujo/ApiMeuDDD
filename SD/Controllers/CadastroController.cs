using System.Web.Mvc;
using SD.Models;

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

    }
}