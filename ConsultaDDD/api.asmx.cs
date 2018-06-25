
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Mvc;
using System.Web;
using ConsultaDDD.Classes;
using System.Data.SqlClient;

namespace ConsultaDDD
{
    /// <summary>
    /// Summary description for api
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class api : System.Web.Services.WebService
    {


        List<resultado> resultados = new List<resultado>();


        [WebMethod]
        public List<resultado> GetCidades(string ddd)
        {
            SqlDataReader consulta = DBCon.Read("select distinct estado, cidade, operadora from DDDs where ddd=" + ddd);

            while (consulta.Read())
            {
                string estado = consulta.GetString(0);
                string cidade = consulta.GetString(1);
                string operadora = consulta.GetString(2);
                resultados.Add(new resultado(ddd, estado, cidade, operadora));
            }
            DBCon.closeCon();
            return resultados;
        }


        [WebMethod]
        public List<resultado> GetDDD(string cidade)
        {
            SqlDataReader consulta = DBCon.Read("select distinct ddd, estado, cidade, operadora from DDDs where cidade like '%" + cidade + "%'");

            while (consulta.Read())
            {
                string ddd = consulta.GetString(0);
                string estado = consulta.GetString(1);
                string nomeCidade = consulta.GetString(2);
                string operadora = consulta.GetString(3);
                resultados.Add(new resultado(ddd, estado, nomeCidade, operadora));
            }
            DBCon.closeCon();
            return resultados;
        }

    }
}
