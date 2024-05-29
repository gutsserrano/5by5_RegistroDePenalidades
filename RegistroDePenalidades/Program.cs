namespace RegistroDePenalidades
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            var lst = ReadFile.GetData("C:\\teste_linq\\motoristas_habilitados.json");

            int op;
            bool conversao;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Qual opção deseja?");
                    Console.WriteLine("1 - Lista completa");
                    Console.WriteLine("2 - Lista com todos os CPFs que começam com 237");
                    Console.WriteLine("3 - Lista com todos os registros de 2021");
                    Console.WriteLine("4 - Lista com todas as empresas LTDA");
                    Console.WriteLine("5 - Lista ordenada pela razão social");
                    Console.WriteLine("0 - Sair");

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
