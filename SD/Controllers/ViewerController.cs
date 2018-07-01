using System;
using System.Web.Mvc;
using SD.Models;

namespace SD.Controllers
{
    public class ViewerController : Controller
    {
        // GET: Viewer
        public ActionResult Index()
        {
            if (getSessao())
            {
                ViewBag.nome = Request.Cookies.Get("nome").Value.ToString();
                ViewBag.email = Request.Cookies.Get("email").Value.ToString();
                ViewBag.id = Request.Cookies.Get("id").Value.ToString();
            }
            else
            {
                TempData["mensagem"] = "É necessario estar conectado para acessar esta area";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Title = "Visualizador";
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