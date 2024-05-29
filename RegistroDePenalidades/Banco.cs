using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
