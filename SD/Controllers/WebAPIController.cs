using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public JsonResult getDDD(string cidade)
        {
            return Json(operador.getDDD(cidade), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getCidades(string ddd)
        {
            return Json(operador.getCidades(ddd), JsonRequestBehavior.AllowGet);
        }
    }
}