using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDePenalidades
{
    internal class MongoDB
    {
        readonly string conexao = "mongodb://root:Mongo%402024%23@localhost:27017/";

        public MongoDB()
        {

        }

        public string Caminho()
        {
            return conexao;
        }
    }
}
