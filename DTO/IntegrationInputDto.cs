using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeeryEscribir.DTO
{
    public class IntegrationInputDto
    {
        public string RNC { get; set; }
        public DateTime Periodo { get; set; }
        public DateTime Transimision { get; set; }
        public List<Empleados> Empleados { get; set; }
    }
}
