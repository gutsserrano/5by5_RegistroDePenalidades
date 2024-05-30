using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDePenalidades
{
    internal class Banco
    {
        readonly string Conexao = "Data Source=127.0.0.1; Initial Catalog=DBPenalidades; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=Yes";

        public Banco()
        {

        }
        public string Caminho()
        {
            return Conexao;
        }

        public static void InserirBD(List<PenalidadesAplicadas> lst)
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());

            try
            {
                conexaosql.Open();
                foreach (var item in lst)
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[InserirMotoristas]", conexaosql);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Cnpj", item.Cnpj));
                    cmd.Parameters.Add(new SqlParameter("@RazaoSocial", item.RazaoSocial));
                    cmd.Parameters.Add(new SqlParameter("@NomeMotorista", item.NomeMotorista));
                    cmd.Parameters.Add(new SqlParameter("@Cpf", item.Cpf));
                    cmd.Parameters.Add(new SqlParameter("@VigenciaCadastro", item.VigenciaCadastro));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conexaosql.Close();
            }
        }

        public static void InsertDB(List<PenalidadesAplicadas> lst)
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            SqlCommand cmd = new SqlCommand();

            int insertSize = 1000;
            int loops = (int)Math.Floor((double)lst.Count / insertSize);

            for(int i = 0; i <= loops; i++)
            {
                string values = "INSERT INTO Penalidades (Cnpj, RazaoSocial, NomeMotorista, cpf, VigenciaCadastro) VALUES ";

                foreach(var item in lst.Skip(insertSize * i).Take(insertSize))
                {
                    if(item.RazaoSocial != null)
                    {
                        values += $"('{item.Cnpj}', " +
                                $"'{item.RazaoSocial.Replace("'", "''")}', " +
                                $"'{item.NomeMotorista.Replace("'", "''")}', " +
                                $"'{item.Cpf}', " +
                                $"'{item.VigenciaCadastro.ToString("MM/dd/yyyy")}'),";
                    }
                }

                values = values.Remove(values.Length - 1);

                conexaosql.Open();
                cmd.CommandText = values;
                cmd.Connection = conexaosql;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(SqlException)
                {
                    throw;
                }
                finally
                {
                    conexaosql.Close();
                }
            }

        }

        public static void InsertProcessControl(string desc, int records)
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO controle_processamento (description, processing_date, Number_of_records) VALUES (@description, @date, @records) ";

            cmd.Parameters.AddWithValue("@description", SqlDbType.VarChar).Value = desc;
            cmd.Parameters.AddWithValue("@date", System.Data.SqlDbType.Date).Value = DateTime.Now;
            cmd.Parameters.AddWithValue("@records", System.Data.SqlDbType.Int).Value = records;

            cmd.Connection = conexaosql;
            conexaosql.Open();
            try
            { 
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }

        }

        public static List<ControleProcessamento> getControleProcessamento()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            SqlCommand cmd = new SqlCommand();

            List<ControleProcessamento> lista = new();

            try
            {
                cmd.CommandText = "SELECT Id, description, processing_date, Number_of_records FROM controle_processamento";

                cmd.Connection = conexaosql;
                var returnValue = cmd.ExecuteReader();

                if (!returnValue.HasRows)
                {
                    Console.WriteLine("Nenhum processamento feito");
                    Console.ReadKey();
                }
                else
                {
                    while (returnValue.Read())
                    {
                        lista.Add(new ControleProcessamento()
                        {
                            Id = int.Parse(returnValue["Id"].ToString()),
                            Description = returnValue["description"].ToString(),
                            ProcessingDate = DateTime.Parse(returnValue["processing_date"].ToString()),
                            NumberOfRecords = int.Parse(returnValue["Number_of_records"].ToString())
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }

            return lista;
        }
    
    }
}
