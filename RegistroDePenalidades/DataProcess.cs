using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Data.SqlClient;
using System.Xml.Linq;

namespace RegistroDePenalidades
{
    internal class DataProcess
    {

        public static int ProcessDataToMongoDB()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            SqlCommand cmd = new SqlCommand();

            List<PenalidadesAplicadas> lista = new();

            try
            {
                cmd.CommandText = "SELECT Cnpj, RazaoSocial, NomeMotorista, cpf, VigenciaCadastro FROM Penalidades";

                cmd.Connection = conexaosql;
                var returnValue = cmd.ExecuteReader();

                if (!returnValue.HasRows)
                {
                    Console.WriteLine("Nenhum dado cadastrado");
                    Console.ReadKey();
                }
                else
                {
                    while (returnValue.Read())
                    {
                       lista.Add(new PenalidadesAplicadas() { 
                           Cnpj = returnValue["Cnpj"].ToString(), 
                           RazaoSocial = returnValue["RazaoSocial"].ToString(), 
                           NomeMotorista = returnValue["NomeMotorista"].ToString(),
                           Cpf = returnValue["cpf"].ToString(),
                           VigenciaCadastro = DateTime.Parse(returnValue["VigenciaCadastro"].ToString())
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
            
            int count = SaveInMongoDB(lista);

            return count;
        }

        static int SaveInMongoDB(List<PenalidadesAplicadas> lista)
        {
            MongoDB mongoConn = new MongoDB();
            var client = new MongoClient(mongoConn.Caminho());
            var database = client.GetDatabase("DBPenalidades");
            string json;

            int count = 0;

            foreach (var item in lista)
            {

                json = ReadFile.GetPartOfJson(item);
                var document = BsonSerializer.Deserialize<BsonDocument>(json);
                var collection = database.GetCollection<BsonDocument>("DBPenalidades");
                collection.InsertOne(document);

                count++;
            }
            return count;
        }
    }
}
