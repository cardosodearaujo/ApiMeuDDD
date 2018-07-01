using System.Web.Mvc;
using SD.Models;
using System;
using System.Diagnostics;
using System.Web;

namespace SD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (getSessao())
            {
                ViewBag.nome = Request.Cookies.Get("nome").Value;
                ViewBag.email = Request.Cookies.Get("email").Value;
                ViewBag.id = Request.Cookies.Get("id").Value;
            }
            else
            {
            }
            ViewBag.Title = "Home Page";
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
    }
}
