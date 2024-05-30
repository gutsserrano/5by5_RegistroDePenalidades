using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace RegistroDePenalidades
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            var lst = ReadFile.GetData("C:\\teste_linq\\motoristas_habilitados.json");
            Menu(lst);
        }

        static void MongoDBProcessData()
        {
            Console.Clear();
            Console.WriteLine("Digite uma descrição para esta inserção no MongoDB:");
            string desc = Console.ReadLine();
            int numberOfRecords = DataProcess.ProcessDataToMongoDB();

            Console.Clear();
            Console.WriteLine("Aguarde...");
            Banco.InsertProcessControl(desc, numberOfRecords);

            Console.Clear();
            if (numberOfRecords > 0)
            {
                Console.WriteLine("Dados inseridos com sucesso!");
            }
            else
            {
                Console.WriteLine("Banco de dados vazio");
            }

            Console.ReadKey();
        }

        static void ShowProcessingData()
        {
            Console.Clear();
            Filters.PrintData(Banco.getControleProcessamento());
            Console.ReadKey();
        }

        static void InsertInDB(List<PenalidadesAplicadas> lst)
        {
            Console.Clear();
            Console.WriteLine("Aguarde...");
            Banco.InsertDB(lst);
            Console.Clear();
        }

        static void Menu(List<PenalidadesAplicadas> lst)
        {
            int op;
            bool conversao;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(">>>>>Penalidades<<<<<");
                    Console.WriteLine("1 - Filtros da Lista");
                    Console.WriteLine("2 - Carregar Lista no Banco de Dados");
                    Console.WriteLine("3 - Inserir dados no MongoDB");
                    Console.WriteLine("4 - Ver Historico de processos");
                    Console.WriteLine("5 - Gerar XML");
                    Console.WriteLine("0 - Sair");

                    conversao = int.TryParse(Console.ReadLine(), out op);
                } while (!conversao || (op < 0 || op > 5));

                switch (op)
                {
                    case 1:
                        FiltersMenu(lst);
                        break;
                    case 2:
                        InsertInDB(lst);
                        break;
                    case 3:
                        MongoDBProcessData();
                        break;
                    case 4:
                        ShowProcessingData();
                        break;
                    case 5:
                        Console.WriteLine(Filters.GenerateXML(lst));
                        break;
                    case 0:
                        return;
                }
            } while (true);
        }

        static void FiltersMenu(List<PenalidadesAplicadas> lst)
        {
            int op;
            bool conversao;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(">>>>>Área de Filtros<<<<<<");
                    Console.WriteLine("1 - Lista completa");
                    Console.WriteLine("2 - Lista com todos os CPFs que começam com 237");
                    Console.WriteLine("3 - Lista com todos os registros de 2021");
                    Console.WriteLine("4 - Lista com todas as empresas LTDA");
                    Console.WriteLine("5 - Lista ordenada pela razão social");
                    Console.WriteLine("0 - Voltar");

                    conversao = int.TryParse(Console.ReadLine(), out op);
                } while (!conversao || (op < 0 || op > 5));

                switch (op)
                {
                    case 1:
                        Filters.PrintData(lst);
                        break;
                    case 2:
                        Console.WriteLine("\nListar Registros que tenham o numero do CPF iniciando com 237");
                        Filters.PrintData(Filters.FilterByCpfStart(lst, "237"));
                        break;
                    case 3:
                        Console.WriteLine("\nListar Registros que tenham a o ano de vigencia em 2021");
                        Filters.PrintData(Filters.FilterByYear(lst, 2021));
                        break;
                    case 4:
                        Console.WriteLine("\nQuantas empresas tem no nome da razao social a descricao LTDA?");
                        Filters.PrintData(Filters.FilterByName(lst, "LTDA"));
                        break;
                    case 5:
                        Console.WriteLine("\nOrdenar a lista de registros pela razao social");
                        Filters.PrintData(Filters.OrderByName(lst));
                        break;
                    case 0:
                        return;
                }

                Console.ReadKey();
            } while (true);
        }
        
    }
}
