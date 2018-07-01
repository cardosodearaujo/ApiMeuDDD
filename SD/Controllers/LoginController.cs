using SD.Models;
using System.Web.Mvc;
using System;
using System.Diagnostics;

namespace SD.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            ViewBag.Title = "Login";
            if (!getSessao())
            {
                return View();
            }
            else
            {
                ViewBag.nome = Request.Cookies.Get("nome").Value.ToString();
                ViewBag.email = Request.Cookies.Get("email").Value.ToString();
                ViewBag.id = Request.Cookies.Get("id").Value.ToString();
                TempData["mensagem"] = "Usuario ja encontra-se conectado";
                return RedirectToAction("Index", "Home");
            }

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

        public JsonResult Login(string email, string senha)
        {
            return Json(operador.login(email, senha), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Response.Cookies["id"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["nome"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["email"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Home");
        }
    }
}