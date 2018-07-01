
using System.Collections.Generic;
using System.Web.Services;
using SD.Models;
using System.Data.SqlClient;

namespace SD
{
    /// <summary>
    /// Summary description for api
    /// </summary>
    [WebService(Namespace = "http://hiruke.ddns.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class api : WebService
    {

        List<resultado> resultados = new List<resultado>();

        public bool autenticado(string email, string senha)
        {
            usuario usuario = operador.login(email, senha);
            if (usuario.id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        public List<resultado> consultaCidades(string ddd, string email, string senha)
        {
            if (autenticado(email, senha))
            {
                return operador.getCidades(ddd);
            }
            else
            {
                return new List<resultado>();
            }
        }


        [WebMethod]
        public List<resultado> consultatDDD(string cidade, string email, string senha)
        {
            if (autenticado(email, senha))
            {
                return operador.getDDD(cidade);
            }
            else
            {
                return new List<resultado>();
            }
        }

        [WebMethod]
        public feedback recuperarConta(string email)
        {
            return operador.recuperarConta(email);
        }

        [WebMethod]
        public feedback criarConta(string nome, string email, string senha)
        {
            return operador.criarConta(nome, email, senha);
        }

        [WebMethod]
        public usuario login(string email, string senha)
        {
            return operador.login(email, senha);
        }

    }
}
