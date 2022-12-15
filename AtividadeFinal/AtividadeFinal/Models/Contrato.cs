using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime TerminoVigencia { get; set; }
        public Dictionary<TipoEquipamento, int> Solicitacoes { get; set; }
        public Stack<TipoEquipamento> Equipamentos { get; private set; }
        public bool Liberado { get; set; }
        public float Valor
        {
            get => Equipamentos.ToList().Sum(e => e.ValorLocacaoDiaria * e.Equipamentos.Count);
        }

        public Contrato()
        {
            Equipamentos = new Stack<TipoEquipamento>();
        }

        public void AdicionarEquipamento(TipoEquipamento tipoEquipamento)
        {
            Equipamentos.Push(tipoEquipamento);
        }

        public override string ToString()
        {
            string retorno = $"Id: {Id} - Inicio Vigencia: {InicioVigencia.ToString("dd/MM/yy")} - Termino Vigencia: {TerminoVigencia.ToString("dd/MM/yy")} - Liberado: {Liberado}";

            retorno += "\nSolicitações: ";

            if (Solicitacoes.Count <= 0)
                retorno += "Sem solicitações";
            else
                foreach (var solicitacao in Solicitacoes)
                {
                    retorno += $"\n\t{solicitacao.Key.Descricao} - {solicitacao.Value}";
                }

            return retorno;
        }

        public string ToString(bool showEquipamentos = false)
        {
            string retorno = $"Id: {Id} - Inicio Vigencia: {InicioVigencia.ToString("dd/MM/yy")} - Termino Vigencia: {TerminoVigencia.ToString("dd/MM/yy")} - Liberado: {Liberado}";

            retorno += "\nSolicitações: ";

            if (Solicitacoes.Count <= 0)
                retorno += "Sem solicitações";
            else
                foreach (var solicitacao in Solicitacoes)
                {
                    retorno += $"\n\t{solicitacao.Key.Descricao} - {solicitacao.Value}";
                }

            if (showEquipamentos)
            {
                retorno += "\nEquipamentos liberados: ";

                if (Equipamentos.Count <= 0)
                    retorno += "Sem equipamentos";
                else
                    foreach (var tipoEquipamento in Equipamentos)
                    {
                        foreach (var equipamento in tipoEquipamento.Equipamentos)
                        {
                            retorno += $"\n\t{equipamento.ToString()}";
                        }
                    }
            }

            return retorno;
        }
    }
}
