using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SD.Models
{
    public abstract class operador
    {
        private static List<resultado> resultados = new List<resultado>();

        //retornar a lista de DDDs de uma cidade
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

        //retorna a lista de cidades com o DDD associado
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

        //Recebe um endereco de email, verifica se o mesmo existe no banco de dados e retorna a senha associada ao mesmo via email cadastrado
        public static feedback recuperarConta(string email)
        {
            feedback feedback = new feedback();
            string senha = null;
            SqlDataReader reader = DBCon.Read("select senha from users where email='" + email + "'");
            while (reader.Read())
            {
                senha = reader.GetString(0).Trim();
            }
            MailServer ms = new MailServer("smtp.gmail.com", 587, "meuddd.app@gmail.com", "4cess0!DDD");

            if (senha != null)
            {
                feedback.status = "ok";
                feedback.mensagem = "Mensagem de recuperação encaminhada";
            }
            else
            {
                feedback.status = "erro";
                feedback.mensagem = "Email não cadastrado";
            }

            try
            {
                ms.enviarEmail("meuddd.app@gmail.com", email, "Recuperacao de senha", "Para acesso ao site, por favor utilize os dados abaixo:<br>Seu email: " + email + "<br>Sua senha: <b>" + senha + "</b>");
            }
            catch (FormatException ex)
            {
                feedback.status = "ok";
                feedback.mensagem = "Formato de email invalido";
            }

            reader.Close();
            return feedback;
        }

        //Recebe os dados submetidos na pagina de cadastro e realiza o input no banco de dados
        public static feedback criarConta(string nome, string email, string senha)
        {
            string resultado = DBCon.Exec("insert into users (nome,email,senha) values ('" + nome + "','" + email + "','" + senha + "')");
            feedback feedback = new feedback();
            switch (resultado)
            {
                case "0":
                    feedback.status = "ok";
                    feedback.mensagem = "Cadastro realizado com sucesso";
                    break;

                case "2627":
                    feedback.status = "erro";
                    feedback.mensagem = "Email já cadastrado";
                    break;
                default:
                    feedback.status = "erro";
                    feedback.mensagem = "Erro inesperado";
                    break;
            }
            return feedback;
        }

        //Retornar os dados da contao de usuario. Retorna dados vazios se a conta ou senha estiverem incorretos
        public static usuario login(string email, string senha)
        {

            SqlDataReader consulta = DBCon.Read("select id, nome, email from users where email='" + email + "' and senha='" + senha + "'");
            usuario usuario = new usuario();
            var userData = new { id = 0, nome = "", email = "" };
            while (consulta.Read())
            {
                usuario.id = consulta.GetInt32(0);
                usuario.nome = consulta.GetString(1).Trim();
                usuario.email = consulta.GetString(2).Trim();
            }
            consulta.Close();
            return usuario;
        }

        //Verifica a existencia de um usuario com base no ID
        public static bool usuarioExiste(int id)
        {
            SqlDataReader consulta = DBCon.Read("select id from users where id=" + id);
            while (consulta.Read())
            {
                consulta.Close();
                return true;
            }
            consulta.Close();
            return false;
        }
    }
}