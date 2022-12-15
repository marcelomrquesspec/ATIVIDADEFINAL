using System.Collections.Generic;

namespace AtividadeFinal.Models
{
    public class TipoEquipamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public float ValorLocacaoDiaria { get; set; }

        public Queue<Equipamento> Equipamentos { get; }

        public TipoEquipamento()
            : this(0)
        {
        }

        public TipoEquipamento(int id)
        {
            Id = id;
            Equipamentos = new Queue<Equipamento>();
        }

        public void AdicionarEquipamento(Equipamento equipamento)
        {
            Equipamentos.Enqueue(equipamento);
        }

        public Equipamento RecuperarEquipamento()
        {
            return Equipamentos.Dequeue();
        }

        public override string ToString()
        {
            string retorno = $"Id: {Id} - Descricao: {Descricao} - Valor Locação Diária: {ValorLocacaoDiaria}\nEquipamentos: ";

            if (Equipamentos.Count <= 0)
                retorno += "Sem equipamentos";
            else
                foreach (var equipamento in Equipamentos)
                {
                    retorno += $"\n\t{equipamento .ToString()}";
                }

            return retorno;
        }

    }
}
