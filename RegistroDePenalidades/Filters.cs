using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RegistroDePenalidades
{
    internal class Filters
    {
        public static void PrintData(List<PenalidadesAplicadas> lista)
        {
            foreach (var item in lista)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }

        public static void PrintData(List<ControleProcessamento> lista)
        {
            foreach (var item in lista)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }

        public static int getCountRecords(List<PenalidadesAplicadas> lista) => lista.Count;

        public static List<PenalidadesAplicadas> FilterByCpfStart(List<PenalidadesAplicadas> lista, string start) => lista.Where(l => l.Cpf.Substring(0, start.Length) == start).ToList();

        public static List<PenalidadesAplicadas> FilterByYear(List<PenalidadesAplicadas> lista, int year) => lista.Where(l => l.VigenciaCadastro.Year == year).ToList();

        public static List<PenalidadesAplicadas> FilterByName(List<PenalidadesAplicadas> lista, string word) => lista.Where(l => l.RazaoSocial.Contains(word, StringComparison.OrdinalIgnoreCase)).ToList();

        public static List<PenalidadesAplicadas> OrderByName(List<PenalidadesAplicadas> lista) => lista.OrderBy(l => l.RazaoSocial).ToList();

        public static string GenerateXML(List<PenalidadesAplicadas> lista)
        {
            if(lista.Count > 0)
            {
                var penalidadeAplicada = new XElement("Root", from data in lista
                                                              select new XElement("motorista",
                                                              new XElement("razao_social", data.RazaoSocial),
                                                              new XElement("cnpj", data.Cnpj),
                                                              new XElement("nome_motorista", data.NomeMotorista),
                                                              new XElement("cpf", data.Cpf),
                                                              new XElement("vigencia_do_cadastro", data.VigenciaCadastro)
                                                              )
                                                        );
                return penalidadeAplicada.ToString();
            }
            else
            {
                return "Não existem registros";
            }
        }
    }
}
