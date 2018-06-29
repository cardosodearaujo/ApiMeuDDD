using SD.Models;
using System.Web.Mvc;
using System.Data.SqlClient;

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
            SqlDataReader consulta = DBCon.Read("select id, nome, email from users where email='" + email + "' and senha='" + senha + "'");
            var userData = new { id = 0, nome = "", email = "" };
            while (consulta.Read())
            {
                userData = new
                {
                    id = consulta.GetInt32(0),
                    nome = consulta.GetString(1).Trim(),
                    email = consulta.GetString(2).Trim()
                };
                Session["id"] = consulta.GetInt32(0);
            }
            consulta.Close();
            return Json(userData, JsonRequestBehavior.AllowGet);
        }

    }
}