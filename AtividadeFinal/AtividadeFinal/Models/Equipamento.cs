using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal.Models
{
    public class Equipamento
    {
        public int Id { get; set; }
        public  string Nome { get; set; }
        public bool isAvariado { get; set; }

        public Equipamento()
        {
            isAvariado = false;
        }

        public override string ToString()
        {
            return $"Id: {Id} - Nome: {Nome} - Está avariado: {isAvariado}";
        }
    }
}
