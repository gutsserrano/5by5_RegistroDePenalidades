using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDePenalidades
{
    internal class ControleProcessamento
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ProcessingDate { get; set; }
        public int NumberOfRecords { get; set; }

        public ControleProcessamento()
        {
            
        }

        public override string? ToString()
        {
            return $"Id................: {Id}\n" +
                $"Description.......: {Description}\n" +
                $"Processing Date...: {ProcessingDate:dd/MM/yyyy}\n" +
                $"Number of Records.: {NumberOfRecords}";
        }
    }
}
