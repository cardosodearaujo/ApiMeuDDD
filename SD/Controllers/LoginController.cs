using SD.Models;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SD.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Login(string email, string senha)
        {
            SqlDataReader consulta = DBCon.Read("select id, nome, sobrenome, email from users where email='" + email + "' and senha='" + senha + "'");
            var userData = new { id = 0, nome = "", sobrenome = "", email = "" };
            while (consulta.Read())
            {
                userData = new
                {
                    id = consulta.GetInt32(0),
                    nome = consulta.GetString(1).Trim(),
                    sobrenome = consulta.GetString(2).Trim(),
                    email = consulta.GetString(3).Trim()
                };
                Session["id"] = consulta.GetInt32(0);
            }
            consulta.Close();
            return Json(userData, JsonRequestBehavior.AllowGet);
        }

    }
}