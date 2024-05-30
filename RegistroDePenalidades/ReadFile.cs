using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDePenalidades
{
    internal class ReadFile
    {
        public static List<PenalidadesAplicadas>? GetData(string path)
        {
            StreamReader sr = new(path);
            string jsonString = sr.ReadToEnd();

            var list = JsonConvert.DeserializeObject<MotoristaHabilitado>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/mm/yyyy" });

            if (list != null) return list.PenalidadesAplicadas;
            return null;
        }

        public static string GetJson(List<PenalidadesAplicadas> lst)
        {
            return JsonConvert.SerializeObject(lst);
        }

        public static string GetPartOfJson(PenalidadesAplicadas obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
