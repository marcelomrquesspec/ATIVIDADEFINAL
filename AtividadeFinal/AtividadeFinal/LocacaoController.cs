using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal.Models
{
    public class LocacaoController
    {
        public List<TipoEquipamento> Estoque { get; private set; }
        public List<Contrato> ContratosLocacao { get; private set; }

        public LocacaoController()
        {
            Estoque = new List<TipoEquipamento>();
            ContratosLocacao = new List<Contrato>();
        }

        public void Cadastrar(TipoEquipamento tipoEquipamento)
        {
            Estoque.Add(tipoEquipamento);
        }

        public void Cadastrar(int idTipo, Equipamento equipamento)
        {
            var tipoEquipamento = Estoque.FirstOrDefault(te => te.Id == idTipo);

            if (tipoEquipamento == null)
                throw new Exception($"Não há um tipo de equipamento com o id: ${idTipo}");

            tipoEquipamento.Equipamentos.Enqueue(equipamento);
        }

        public void Cadastrar(Contrato contratoLocacao)
        {
            ContratosLocacao.Add(contratoLocacao);
        }

        public void Liberar(int idContrato)
        {
            var contrato = ContratosLocacao.FirstOrDefault(c => c.Id == idContrato && !c.Liberado);

            if (contrato == null)
                throw new Exception("Contrato não encontrato");

            foreach (var solicitacao in contrato.Solicitacoes)
            {
                var tipoEquipamento = solicitacao.Key;
                var qtde = solicitacao.Value;

                var contratoEquipamento = new TipoEquipamento(tipoEquipamento.Id);
                for (int i = 0; i < qtde && tipoEquipamento.Equipamentos.Count > 0; i++)
                {
                    var item = tipoEquipamento.RecuperarEquipamento();

                    if (item.isAvariado)
                    {
                        tipoEquipamento.AdicionarEquipamento(item);
                    }
                    else
                    {
                        contratoEquipamento.AdicionarEquipamento(item);
                    }
                }

                contrato.AdicionarEquipamento(contratoEquipamento);
            }

            contrato.Liberado = true;
        }

        public void Devolver(int idContrato)
        {
            var contrato = ContratosLocacao.FirstOrDefault(c => c.Id == idContrato && c.Liberado);

            if (contrato == null)
                throw new Exception("Contrato não encontrato");

            foreach (var tipoEquipamento in contrato.Equipamentos)
            {
                var currentTipoEquipamento = Estoque.FirstOrDefault(e => e.Id == tipoEquipamento.Id);

                while (tipoEquipamento.Equipamentos.Count > 0)
                {
                    currentTipoEquipamento.AdicionarEquipamento(tipoEquipamento.RecuperarEquipamento());
                }
            }
        }
        public TipoEquipamento ConsultarTiposEquipamento(int idTipoEquipamento)
        {
            return Estoque.FirstOrDefault(e => e.Id == idTipoEquipamento);
        }
        public List<Contrato> ConsultarContratosLocacao(bool liberados = false)
        {
            if(liberados)
                return ContratosLocacao.Where(c => c.Liberado).ToList();

            return ContratosLocacao.ToList();
        }
    }
}
