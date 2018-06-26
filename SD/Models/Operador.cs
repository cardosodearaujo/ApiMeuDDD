using System;
using System.Collections.Generic;
using System.Web;
using SD.Models;
using System.Data.SqlClient;

namespace SD.Models
{
    public abstract class operador
    {
        private static List<resultado> resultados = new List<resultado>();

        public static List<resultado> getDDD(string cidade)
        {
            DBCon.getCon();
            SqlDataReader consulta = DBCon.Read("select distinct ddd, estado, cidade, operadora from DDDs where cidade like '%" + cidade + "%'");
            resultados = new List<resultado>();
            while (consulta.Read())
            {
                string ddd = consulta.GetString(0);
                string estado = consulta.GetString(1);
                string nomeCidade = consulta.GetString(2);
                string operadora = consulta.GetString(3);
                resultados.Add(new resultado(ddd, estado, nomeCidade, operadora));
            }
            consulta.Close();
            return resultados;
        }

        public static List<resultado> getCidades(string ddd)
        {
            DBCon.getCon();
            SqlDataReader consulta = DBCon.Read("select distinct estado, cidade, operadora from DDDs where ddd=" + ddd);
            resultados = new List<resultado>();
            while (consulta.Read())
            {
                string estado = consulta.GetString(0);
                string cidade = consulta.GetString(1);
                string operadora = consulta.GetString(2);
                resultados.Add(new resultado(ddd, estado, cidade, operadora));
            }
            consulta.Close();
            return resultados;
        }

    }
}