using AtividadeFinal.Models;
using AtividadeFinal.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal
{
    public class Seletor
    {

        private readonly LocacaoController _locacaoController;

        public Seletor(LocacaoController locacaoController)
        {
            _locacaoController = locacaoController;
        }

        public int EscolherOpcao()
        {
            int opcao = 1;

            do
            {
                Console.WriteLine("0\tFinalizar\r\n1\tCadastrar tipo de equipamento\r\n2\tConsultar tipo de equipamento (com os respectivos itens cadastrados) \r\n3\tCadastrar equipamento (item em um determinado tipo)\r\n4\tRegistrar Contrato de Locação\r\n5\tConsultar Contratos de Locação (com os respectivos itens contratados)\r\n6\tLiberar de Contrato de Locação \r\n7\tConsultar Contratos de Locação liberados (com os respectivos itens)\r\n8\tDevolver equipamentos de Contrato de Locação liberado\r\n");
                Console.Write("\nDigite a opção: ");
                opcao = int.Parse(Console.ReadLine());

                separador();

                try
                {
                    switch (opcao)
                    {
                        case (int)OpcoesEnum.Finalizar:
                            finalizar();
                            break;
                        case (int)OpcoesEnum.CadastrarTipoEquipamento:
                            cadastrarTipoEquipamento();
                            break;
                        case (int)OpcoesEnum.ConsutlarTipoEquipamento:
                            consutlarTipoEquipamento();
                            break;
                        case (int)OpcoesEnum.CadastrarEquipamento:
                            cadastrarEquipamento();
                            break;
                        case (int)OpcoesEnum.RegistrarContratoLocacao:
                            registrarContratoLocacao();
                            break;
                        case (int)OpcoesEnum.ConsultarContratosLocacao:
                            consultarContratosLocacao();
                            break;
                        case (int)OpcoesEnum.LiberarContratoLocacao:
                            liberarContratoLocacao();
                            break;
                        case (int)OpcoesEnum.ConsultarContratosLocacaoLiberados:
                            consultarContratosLocacaoLiberados();
                            break;
                        case (int)OpcoesEnum.DevolerEsquipamentoContratoLocacao:
                            devolerEsquipamentoContratoLocacao();
                            break;
                        default:
                            Console.WriteLine("\n\nopção invalida, por favor selecione um valor entre 0 e 8\n\n");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n\n{e.Message}\n");
                }

                separador();
            } while (opcao < 0 || opcao > 10);

            return opcao;
        }

        private void devolerEsquipamentoContratoLocacao()
        {
            Console.Write("Digite o Id do contrato: ");
            int id = int.Parse(Console.ReadLine());

            _locacaoController.Devolver(id);

            Console.WriteLine("\n\nContrato devolvido com sucesso !\n");
        }

        private void consultarContratosLocacaoLiberados()
        {
            var contratos = _locacaoController.ConsultarContratosLocacao(liberados: true);

            if (contratos.Count <= 0)
            {
                Console.WriteLine("\n\nNão há contratos liberados.\n");
                return;
            }

            Console.WriteLine("\n\nContratos solicitados\n");
            foreach (var contrato in contratos)
            {
                Console.WriteLine(contrato.ToString(showEquipamentos: true));
            }
        }

        private void liberarContratoLocacao()
        {
            Console.Write("Digite o Id do contrato: ");
            int id = int.Parse(Console.ReadLine());

            _locacaoController.Liberar(id);

            Console.WriteLine("\n\nContrato liberado com sucesso !\n");
        }

        private void consultarContratosLocacao()
        {
            var contratos = _locacaoController.ConsultarContratosLocacao();

            if (contratos.Count <= 0)
            {
                Console.WriteLine("\n\nNão há contratos cadastrados.\n");
                return;
            }

            Console.WriteLine("\n\nContratos solicitados\n");
            foreach (var contrato in contratos)
            {
                Console.WriteLine(contrato.ToString());
            }

        }

        private void registrarContratoLocacao()
        {
            Contrato contrato = new Contrato();

            Console.Write("Digite o Id do contrato: ");
            contrato.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("\n");
            contrato.InicioVigencia = inputDate("do inicio de vigencia do contrato");

            Console.WriteLine("\n");
            contrato.TerminoVigencia = inputDate("do termino de vigencia do contrato");

            contrato.Solicitacoes = inputSolicitacao();

            _locacaoController.Cadastrar(contrato);

            Console.WriteLine("\n\nContrato registrado com sucesso !\n");
        }

        private void cadastrarEquipamento()
        {
            Console.Write("Digite o Id do Tipo de equipamento: ");
            int id = int.Parse(Console.ReadLine());

            var tipoEquipamento = _locacaoController.ConsultarTiposEquipamento(id);

            if (tipoEquipamento == null)
            {
                Console.WriteLine("\n\nTipo equipamento não encontrado.\n");
                return;
            }

            Equipamento equipamento = new Equipamento();

            Console.Write("Digite o Id do equipamento: ");
            equipamento.Id = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome equipamento: ");
            equipamento.Nome = Console.ReadLine();

            tipoEquipamento.AdicionarEquipamento(equipamento);

            Console.WriteLine("\n\nEquipamento cadastrado com sucesso!\n");
        }

        private void consutlarTipoEquipamento()
        {
            Console.Write("Digite o Id do Tipo de equipamento: ");
            int id = int.Parse(Console.ReadLine());

            var tipoEquipamento = _locacaoController.ConsultarTiposEquipamento(id);

            if (tipoEquipamento == null)
            {
                Console.WriteLine("\n\nTipo equipamento não encontrado.\n");
            }
            else
            {
                Console.WriteLine($"\n\n{tipoEquipamento.ToString()}\n");
            }
        }

        private void cadastrarTipoEquipamento()
        {
            TipoEquipamento tipoEquipamento = new TipoEquipamento();

            Console.Write("Digite o Id do Tipo de equipamento: ");
            tipoEquipamento.Id = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição do Tipo de equipamento: ");
            tipoEquipamento.Descricao = Console.ReadLine();

            Console.Write("Digite o valor do Tipo de equipamento: ");
            tipoEquipamento.ValorLocacaoDiaria = float.Parse(Console.ReadLine());

            _locacaoController.Cadastrar(tipoEquipamento);

            Console.WriteLine("\n\nTipo de equipamento cadastrado com sucesso!\n");
        }

        private void finalizar()
        {
            Console.WriteLine("Obrigado por usar o programa...");
            Console.WriteLine("Até a proxima :)");
            Console.ReadKey();
        }


        private DateTime inputDate(string label = "")
        {
            DateTime data;
            int dia, mes, ano;

            Console.Write($"Digite o dia {label}: ");
            dia = int.Parse(Console.ReadLine());

            Console.Write($"Digite o mes {label}: ");
            mes = int.Parse(Console.ReadLine());

            Console.Write($"Digite o ano {label}: ");
            ano = int.Parse(Console.ReadLine());

            data = new DateTime(ano, mes, dia);

            return data;
        }


        private Dictionary<TipoEquipamento, int> inputSolicitacao()
        {
            var solicitacoes = new Dictionary<TipoEquipamento, int>();
            char sair = ' ';

            while (sair != '0')
            {
                Console.Write("\n\nDigite o  Id do tipo de equipamento solicitado: ");
                int idTipoEquipamento = int.Parse(Console.ReadLine());

                var tipoEquipamento = _locacaoController.ConsultarTiposEquipamento(idTipoEquipamento);

                if (tipoEquipamento == null)
                {
                    Console.WriteLine("\n\nTipo equipamento não encontrado.\n");
                    continue;
                }

                Console.Write("Digite a quantidade solicitada de equipamentos: ");
                int qtdTipoEquipamento = int.Parse(Console.ReadLine());

                if (qtdTipoEquipamento <= 0)
                {
                    Console.WriteLine("\n\nA quantidade deve ser positiva.\n");
                    continue;
                }

                solicitacoes.Add(tipoEquipamento, qtdTipoEquipamento);

                Console.Write("Deseja adicionar mais algum tipo de quipamento (NÃO = 0 e SIM = Qualquer tecla)?  ");
                sair = Console.ReadLine()[0];
            }

            return solicitacoes;
        }
        private void separador()
        {
            Console.WriteLine();
            for (int i = 0; i < 30; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n");
        }

    }
}
