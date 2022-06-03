using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscalonamentoDeProcessos
{
    public class Processos
    {
        public string? Nome { get; set; }
        public int Prioridade { get; set; }
        public int Tempo { get; set; }
        public int? TipoFila { get; set; }
    }
}
