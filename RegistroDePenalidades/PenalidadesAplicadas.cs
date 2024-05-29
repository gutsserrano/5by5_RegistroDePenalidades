using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDePenalidades
{
    internal class PenalidadesAplicadas
    {
        [JsonProperty("razao_social")]
        public string RazaoSocial { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("nome_motorista")]
        public string NomeMotorista { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("vigencia_do_cadastro")]
        public DateTime VigenciaCadastro { get; set; }

        public override string ToString() => $"Razão Social.........: {RazaoSocial}\nCNPJ.................: {Cnpj}\nNome do Motorista....: {NomeMotorista}\nCPF..................: {Cpf}\nVigência do Cadastro.: {VigenciaCadastro:dd/MM/yyyy}";
    }
}
