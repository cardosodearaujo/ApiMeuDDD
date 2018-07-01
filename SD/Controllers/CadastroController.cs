using System.Web.Mvc;
using SD.Models;
using System;

namespace SD.Controllers
{
    public class CadastroController : Controller
    {

        //Retorna a view Index (Pagina de criacao de conta)
        public ActionResult Index()
        {
            ViewBag.Title = "Cadastro";
            if (!getSessao())
            {
                return View();
            }
            else
            {
                ViewBag.nome = Request.Cookies.Get("nome").Value.ToString();
                ViewBag.email = Request.Cookies.Get("email").Value.ToString();
                ViewBag.id = Request.Cookies.Get("id").Value.ToString();
                TempData["mensagem"] = "Um usuario encontra-se conectado.<br>Por favor desconecte da sessão atual antes de prosseguir";
                return RedirectToAction("Index", "Home");
            }
        }

        //Retorna a view Recuperacao (Pagina de recuperacao de senha)
        public ActionResult Recuperacao()
        {
            if (getSessao())
            {
                ViewBag.nome = Request.Cookies.Get("nome").Value.ToString();
                ViewBag.email = Request.Cookies.Get("email").Value.ToString();
                ViewBag.id = Request.Cookies.Get("id").Value.ToString();
            }
            ViewBag.Title = "Recuperacao";
            return View();
        }

        bool getSessao()
        {
            int id = 0;
            try
            {
                id = int.Parse(Request.Cookies.Get("id").Value);
            }
            catch (Exception e)
            {
                id = 0;
            }

            if (id > 0)
            {
                if (!operador.usuarioExiste(id))
                {
                    Response.Cookies["id"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["nome"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["email"].Expires = DateTime.Now.AddDays(-1);
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
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