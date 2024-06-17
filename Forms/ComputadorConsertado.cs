using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class ComputadorConsertado : Form
    {
        private Computador _computador;
        private ComputadorDao _computadorDao;
        private ComputadorSaida _computadorSaida;

        public ComputadorConsertado()
        {
            this._computadorDao = new ComputadorDao();
            this._computadorSaida = new ComputadorSaida();
            InitializeComponent();
            Tnome.KeyPress += this.textBox1_KeyPress;
        }

        private void Benter_Click(object sender, EventArgs e)
        {
            if (Tnome.Text.Equals("") || TDescricao.Text.Equals(""))
            {
                MessageBox.Show("Por favor preencha todos os campos ");
            }
            else
            {
                string message = "Deseja prosseguir? Digite 's' para sim ou 'n' para não:";
                string caption = "Confirmação";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, caption, buttons);

                if (result == DialogResult.Yes)
                {
                    String descricao=  TDescricao.Text;

                    string selectedValues = "";

                    foreach (object item in CheckBox.CheckedItems)
                    {
                        selectedValues += item.ToString() + ",";
                    }

                    if (!string.IsNullOrEmpty(selectedValues))
                    {
                        selectedValues = selectedValues.TrimEnd(',');
                    }

                    DataTable TodosDadosDoPc = computadoresMapeadosEconsertado.Dao.montarTabelasDao.retornaTudoSobrePc(Tnome.Text);
                    object id_pc = null;
                    try
                    {
                        id_pc = TodosDadosDoPc.Rows[0][0].ToString();
                       

                        if (id_pc.ToString() != "")
                        {
                            int final = TodosDadosDoPc.Rows.Count - 1;
                            {
                                if (TodosDadosDoPc.Rows[final][9].ToString() == "" || (TodosDadosDoPc.Rows[final][9].ToString() != "" && TodosDadosDoPc.Rows[final][13].ToString() == ""))
                                {
                                    String id_conserto = TodosDadosDoPc.Rows[final][9].ToString();

                                    DataTable tableTemp=computadoresMapeadosEconsertado.Dao.montarTabelasDao.TabelaTemp(id_pc.ToString());
                                    int ultimo= tableTemp.Rows.Count-1;
                                    String tabelaTemp_id = tableTemp.Rows[ultimo][0].ToString();
                                    String tabelaTemp_id_pa = tableTemp.Rows[ultimo][2].ToString();

                                    computadoresMapeadosEconsertado.Dao.UpdateDao.ConsertoFinalizadoComputador(descricao, id_conserto);
                                    computadoresMapeadosEconsertado.Dao.UpdateDao.updateProgramasPc(selectedValues, id_pc.ToString());

                                    if (string.Equals(TDescricao.Text, "Condenado", StringComparison.OrdinalIgnoreCase) || 
                                        (string.Equals(TDescricao.Text, "Condenada", StringComparison.OrdinalIgnoreCase)))
                                    {
                                        //se na descrição estiver como condenado aqui faremos para ela entrar no park das maquinas condenadas
                                        computadoresMapeadosEconsertado.Dao.UpdateDao.Condenar(id_pc.ToString());

                                    } else { computadoresMapeadosEconsertado.Dao.UpdateDao.retornarPaOrigem(id_pc.ToString(), tabelaTemp_id_pa);}


                                    computadoresMapeadosEconsertado.Dao.delete.DeletePaTemporaria(tabelaTemp_id);


                                    if (comboBox.SelectedItem != null)
                                        {

                                        String sistema = comboBox.SelectedItem.ToString();
                                        computadoresMapeadosEconsertado.Dao.UpdateDao.AtualizarComputador(id_pc.ToString(), sistema);
                                    }
                                       
                                            MessageBox.Show("Baixa efetuado com sucesso no computador : " + Tnome.Text);
                                }
                                else
                                {
                                    MessageBox.Show("Este computador não esta esperando conserto");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Este computador nao existe");

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Computador Nao encontrado em nosso sistema");
                    }
                }
                else
                {
                    MessageBox.Show("Operacao cancelada com sucesso!");
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se a tecla pressionada não é um dígito numérico ou a tecla de backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Cancela o evento de pressionar a tecla
                e.Handled = true;
            }
        }
    }
}
       

  


