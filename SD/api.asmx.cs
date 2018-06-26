
using System.Collections.Generic;
using System.Web.Services;
using SD.Models;

namespace SD
{
    /// <summary>
    /// Summary description for api
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class api : WebService
    {

        List<resultado> resultados = new List<resultado>();


        [WebMethod]
        public List<resultado> GetCidades(string ddd)
        {
            return operador.getCidades(ddd);
        }


        [WebMethod]
        public List<resultado> GetDDD(string cidade)
        {
            return operador.getDDD(cidade);
        }

    }
}
