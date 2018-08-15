using System.Web.Mvc;
using SD.Models;

namespace SD.Controllers
{
    public class WebAPIController : Controller
    {
        // GET: API
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getDDD(string cidade, string UF)
        {
            return Json(operador.getDDD(cidade,UF), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getCidades(string ddd)
        {
            return Json(operador.getCidades(ddd), JsonRequestBehavior.AllowGet);
        }
    }
}