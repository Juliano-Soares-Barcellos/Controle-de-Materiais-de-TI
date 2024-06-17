using computadoresMapeadosEconsertado.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computadoresMapeadosEconsertado.Dao
{
    public static class montarTabelasDao
    {
        public static computadorDao pc = new computadorDao();

        public static DataTable TabelaConserto()
        {
            List<computadorComConsertos> TodosConsertados = pc.BuscaComputadorConsertado();
            DataTable tabela = new DataTable();
            try
            {
                tabela.Columns.Add("Data-Entrada", typeof(DateTime));
                tabela.Columns.Add("Descriçao", typeof(string));
                tabela.Columns.Add("Patrimonio", typeof(string));
                tabela.Columns.Add("Modelo", typeof(string));
                tabela.Columns.Add("Sistema", typeof(string));
                tabela.Columns.Add("PA", typeof(int));
                tabela.Columns.Add("Grupo", typeof(string));
                tabela.Columns.Add("Data-Saida", typeof(string));
                tabela.Columns.Add("o que foi feito", typeof(string));



                foreach (var consertado in TodosConsertados)
                {
                    var computadorTabela = consertado.Computador;
                    var ConsertosTabela = consertado.Conserto;

                    foreach (var item in ConsertosTabela)
                    {
                        System.Data.DataRow linha = tabela.NewRow();
                        linha["Data-Entrada"] = item.data_entrada;
                        linha["Descriçao"] = item.Descricao_problema;
                        linha["Patrimonio"] = computadorTabela.patrimonio;
                        linha["Modelo"] = computadorTabela.marca;
                        linha["Sistema"] = computadorTabela.sistemasOperacionais;
                        linha["PA"] = computadorTabela.fk_compComputador_Pa.pa;
                        linha["Grupo"] = computadorTabela.fk_compComputador_Pa.Fk_gurupoModel.grupos;
                        linha["Data-Saida"] = item.data_conserto_finalizado;
                        linha["o que foi feito"] = item.descricao_problema_resolvido;
                        tabela.Rows.Add(linha);

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return tabela;
        }

        public static DataTable TabelaPas()
        {
            List<computadorModel> Pas = pc.BuscaPAs();
            DataTable tabela = new DataTable();
            try
            {
                tabela.Columns.Add("PA", typeof(int));
                tabela.Columns.Add("Grupo", typeof(string));
                tabela.Columns.Add("patrimonio", typeof(string));
                tabela.Columns.Add("Modelo", typeof(string));
                tabela.Columns.Add("sistema opercaional", typeof(string));
                tabela.Columns.Add("Programas instalados", typeof(string));


                foreach (var item in Pas)
                {
                    System.Data.DataRow linha = tabela.NewRow();

                    linha["PA"] = item.fk_compComputador_Pa.pa;
                    linha["Grupo"] = item.fk_compComputador_Pa.Fk_gurupoModel.grupos;
                    linha["Patrimonio"] = item.patrimonio;
                    linha["Modelo"] = item.marca;
                    linha["sistema opercaional"] = item.sistemasOperacionais;
                    linha["Programas instalados"] = item.programa;

                    tabela.Rows.Add(linha);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return tabela;
        }


        public static DataTable retornaTudoSobrePc(String patrimonio)
        {
            computadorDao cp = new computadorDao();
            List<computadorComConsertos> TodosConsertados = pc.BuscaPatrimonio(patrimonio);
            DataTable tabela = new DataTable();

            try
            {
                tabela.Columns.Add("id computador", typeof(int));
                tabela.Columns.Add("patrimonio", typeof(int));
                tabela.Columns.Add("marca", typeof(string));
                tabela.Columns.Add("programa", typeof(string));
                tabela.Columns.Add("Sistema", typeof(string));
                tabela.Columns.Add("fk_Pa", typeof(string));
                tabela.Columns.Add("Pa", typeof(string));
                tabela.Columns.Add("fk_grupo", typeof(string));
                tabela.Columns.Add("grupo", typeof(string));
                tabela.Columns.Add("id_conserto", typeof(string));
                tabela.Columns.Add("data_entrada", typeof(DateTime));
                tabela.Columns.Add("Descricao Problema", typeof(string));
                tabela.Columns.Add("fk_computador", typeof(int));
                tabela.Columns.Add("descricao resolução", typeof(string));
                tabela.Columns.Add("data_saida", typeof(DateTime));



                foreach (var consertado in TodosConsertados)
                {
                    var computadorTabela = consertado.Computador;
                    var ConsertosTabela = consertado.Conserto;
                    System.Data.DataRow linha = tabela.NewRow();
                  
                            linha["id computador"] = computadorTabela.id_computador;
                            linha["patrimonio"] = computadorTabela.patrimonio;
                            linha["marca"] = computadorTabela.marca;
                            linha["programa"] = computadorTabela.programa;
                            linha["Sistema"] = computadorTabela.sistemasOperacionais;
                            linha["fk_Pa"] = computadorTabela.fk_compComputador_Pa.id_pa;
                            linha["Pa"] = computadorTabela.fk_compComputador_Pa.pa;
                            linha["fk_grupo"] = computadorTabela.fk_compComputador_Pa.Fk_gurupoModel.id_grupo;
                            linha["grupo"] = computadorTabela.fk_compComputador_Pa.Fk_gurupoModel.grupos;

                    if (ConsertosTabela != null)
                    {
                                foreach (var item in ConsertosTabela)
                                {
                                    linha["id_conserto"] = item.id_conserto;
                                    linha["data_entrada"] = item.data_entrada;
                                    linha["Descricao Problema"] = item.Descricao_problema;
                                    linha["fk_computador"] = item.fkComputador.id_computador;
                                    linha["descricao resolução"] = item.descricao_problema_resolvido;
                                    linha["data_saida"] = item.data_conserto_finalizado;
                                }
                    }

                    tabela.Rows.Add(linha);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return tabela;

        }

        public static DataTable TabelaTemp(String id_pc)
        {
            List<temp_conserto> TabelaTempSelect = pc.ObterDadosTemp(id_pc);
            DataTable tabela = new DataTable();
            try
            {
                tabela.Columns.Add("id_tempConserto", typeof(int));
                tabela.Columns.Add("fk_computador", typeof(string));
                tabela.Columns.Add("fk_pa", typeof(string));

                foreach (var table in TabelaTempSelect)
                {
                    var tabelaTempVar = table;
                    System.Data.DataRow linha = tabela.NewRow();
                    linha["id_tempConserto"] = tabelaTempVar.id_tempConserto;
                    linha["fk_computador"] = tabelaTempVar.fk_computador;
                    linha["fk_pa"] = tabelaTempVar.fk_pa;
                    tabela.Rows.Add(linha);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return tabela;
        }

        public static DataTable MontarTabela(string sistema)
        {
            List<computador_conserto> ConsertosTabela = new List<computador_conserto>();
            computadorDao cp = new computadorDao();
            List<computadorComConsertos> TodosConsertados = pc.BuscaPeloSistema(sistema);
            DataTable tabela = new DataTable();

            try
            {
                tabela.Columns.Add("patrimonio", typeof(int));
                tabela.Columns.Add("modelo", typeof(string));
                tabela.Columns.Add("programa", typeof(string));
                tabela.Columns.Add("sistemasOperacionais", typeof(string));
                tabela.Columns.Add("Pa", typeof(string));
                tabela.Columns.Add("grupo", typeof(string));
                tabela.Columns.Add("data_entrada", typeof(DateTime));
                tabela.Columns.Add("Descricao_Problema", typeof(string));
                tabela.Columns.Add("data_saida", typeof(DateTime));
                tabela.Columns.Add("descricao_resolucao", typeof(string));


                foreach (var consertado in TodosConsertados)
                {
                    var computadorTabela = consertado.Computador;
                    ConsertosTabela = consertado.Conserto;
                    DataRow linha = tabela.NewRow();
                    if (ConsertosTabela != null)
                    {
                        foreach (var item in ConsertosTabela)
                        {
                            

                            linha["patrimonio"] = computadorTabela.patrimonio;
                            linha["modelo"] = computadorTabela.marca;
                            linha["programa"] = computadorTabela.programa;
                            linha["sistemasOperacionais"] = computadorTabela.sistemasOperacionais;
                            linha["Pa"] = computadorTabela.fk_compComputador_Pa.pa;
                            linha["grupo"] = computadorTabela.fk_compComputador_Pa.Fk_gurupoModel.grupos;

                            linha["data_entrada"] = item.data_entrada;
                            linha["Descricao_Problema"] = item.Descricao_problema;
                            linha["data_saida"] = item.data_conserto_finalizado;
                            linha["descricao_resolucao"] = item.descricao_problema_resolvido;


                        }
                        tabela.Rows.Add(linha);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return tabela;
        }


        public static DataTable Patrimonio(string empresa,string filtro,string coluna)
            {
            CultureInfo cultura = new CultureInfo("pt-BR");
            string query = "";
            List<computadorModel> patrimonios = new List<computadorModel>();
            computadorDao computradorDAO = new computadorDao();
            if (!coluna.Equals(""))
            {
                query = computradorDAO.condicao(coluna);
                patrimonios = computradorDAO.FiltroMapPc(empresa, filtro, query);
            }
            else
            {
                query = "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id ;";
                patrimonios = computradorDAO.FiltroMapInitFiltroMapInit(query);
            }
            DataTable tabela = new DataTable();

            try
            {
                tabela.Columns.Add("Tag de Serviço", typeof(string));  
                tabela.Columns.Add("Patrimonio", typeof(int));
                tabela.Columns.Add("Empresa", typeof(string));
                tabela.Columns.Add("Modelo", typeof(string));
                tabela.Columns.Add("Programa", typeof(string));
                tabela.Columns.Add("sistemas Operacionais", typeof(string));
                tabela.Columns.Add("Pa", typeof(string));
                tabela.Columns.Add("Id da Pa", typeof(string));
                tabela.Columns.Add("Setor", typeof(string));
                tabela.Columns.Add("Data da Compra", typeof(string));
                tabela.Columns.Add("Valor do item", typeof(string));
                tabela.Columns.Add("Valor Residual", typeof(string));

                foreach (var item in patrimonios)
                {

                    Double depreciacao= computradorDAO.depreciacao(item.DataCompra, item.valor);
                    String ValorDepreciadoEmFormaDinheiro = depreciacao.ToString("C",cultura);
                     DataRow linha = tabela.NewRow();
                    string dataSemHoras = item.DataCompra.ToString("dd/MM/yyyy");


                    linha["Tag de Serviço"] = item.tag_servico;
                    linha["Patrimonio"] = item.patrimonio;
                    linha["Empresa"] = item.empresa;
                    linha["Modelo"] = item.marca;
                    linha["Programa"] = item.programa;
                    linha["Sistemas Operacionais"] = item.sistemasOperacionais;
                    linha["Pa"] = item.fk_compComputador_Pa.pa;
                    linha["Id da Pa"] = item.fk_compComputador_Pa.talk_id;
                    linha["Setor"] = item.fk_compComputador_Pa.Fk_gurupoModel.grupos;
                    linha["Data da Compra"] = dataSemHoras;
                    linha["Valor do item"] = item.valor.ToString("C",cultura);
                    linha["Valor Residual"] = ValorDepreciadoEmFormaDinheiro;

                    tabela.Rows.Add(linha);
                }   
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return tabela;
        }

       

    }

}