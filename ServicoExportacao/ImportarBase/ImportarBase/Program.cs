using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportarBase
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ContadorImportados = 0;
            var ContadorDuplicados = 0;
            var ContadorGeral = 0;
            try
            {
                Console.WriteLine("|==> Para iniciar pressione qualquer tecla...");
                Console.ReadKey();

                var Lista = File.ReadAllLines("C:\\Arquivo\\DDDs.csv")
                    .Select(F => F.Split(';'))
                    .Select(F => new Dados()
                    {
                        DDD = Replace(F[0]),
                        UF = Replace(F[5]),
                        Cidade = Replace(F[6]),
                        Operadora = Replace(F[9])
                    }).ToList();

                Console.WriteLine("|==> Foram encontrados " + Lista.Count + " registro(s) para importar deseja prosseguir?");
                Console.WriteLine("|==> Pressione qualquer tecla para confirmar...");
                Console.ReadKey();
                                
                var DadosConexao = "Server=tcp:new.database.windows.net,1433;Initial Catalog=BD_New_MeuDDD;Persist Security Info=False;User ID=administrador;Password=M1n3Rv@7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=0;";
                var Conexao = new SqlConnection();
                var Comando = new SqlCommand();

                Conexao.ConnectionString = DadosConexao;
                Conexao.Open();

                Comando.CommandText = "Truncate Table DDDs";
                Comando.Connection = Conexao;
                Comando.CommandTimeout = 0;
                Comando.ExecuteNonQuery();

                foreach (var Item in Lista)
                {
                    Comando = new SqlCommand();
                    Comando.Connection = Conexao;
                    Comando.CommandTimeout = 0;
                    Comando.Parameters.Add("@DDD", SqlDbType.NVarChar).Value = Item.DDD;
                    Comando.Parameters.Add("@ESTADO", SqlDbType.NVarChar).Value = Item.UF;
                    Comando.Parameters.Add("@CIDADE", SqlDbType.NVarChar).Value = Item.Cidade;
                    Comando.Parameters.Add("@OPERADORA", SqlDbType.NVarChar).Value = Item.Operadora;

                    String SQL;

                    SQL = @"Select IsNull(Count(ID),0) As TotalRegistro From DDDs Where DDD = @DDD And ESTADO = @ESTADO And CIDADE = @CIDADE And OPERADORA = @OPERADORA";
                    Comando.CommandText = SQL;
                    long TotalRegistro = long.Parse(Comando.ExecuteScalar().ToString());
                    if (TotalRegistro <= 0)
                    {
                        SQL = @"Insert Into DDDs 
                                (DDD, 
                                ESTADO, 
                                CIDADE, 
                                OPERADORA) 
                            Values
                                (@DDD,
                                @ESTADO,
                                @CIDADE,
                                @OPERADORA)";

                        Comando.CommandText = SQL;
                        Comando.ExecuteNonQuery();

                        ContadorImportados += 1;
                    }
                    else
                    {
                        ContadorDuplicados += 1;
                    }
                    ContadorGeral += 1;
                    Console.WriteLine("|==> Total: " + ContadorGeral + " | Importados: " + ContadorImportados + " | Duplicados: " + ContadorDuplicados);
                }

                Console.WriteLine("|==> Operação concluida com sucesso!");
                Console.WriteLine("|==> Pressione qualquer tecla para terminar...");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocorreu um erro no registro: " + ContadorGeral + "\nExceção gerada: " + ex.Message);
            }            
        }

        public static String Replace(String dado)
        {
            return dado.Replace("'","").Replace("\"", "");
        }
    }

    public class Dados
    {
        public String  DDD { get; set; }
        public String UF { get; set; }
        public String Cidade { get; set; }
        public String Operadora { get; set; }
    }
}
