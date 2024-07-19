using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using System;
using System.Collections.Generic;
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
                    _computadorSaida.DescricaoSaida = TDescricao.Text;
                    _computadorSaida.DataSaida = DateTime.Now;
                    string selectedValues = "";

                    foreach (object item in CheckBox.CheckedItems)
                    {
                        selectedValues += item.ToString() + ",";
                    }

                    if (!string.IsNullOrEmpty(selectedValues))
                    {
                        selectedValues = selectedValues.TrimEnd(',');
                    }
                    
                    _computador =new Computador(_computadorDao.AcharPcs(Tnome.Text),Tnome.Text,"","", selectedValues);
                    
                    _computadorSaida.Computador_id = _computador;
                    //
                    
                   
                    if (_computador.Id >= 1)
                    {
                        _computadorSaida.computadorEntrada_id = _computadorDao.AcharIdDataEntrada(_computador.Id);

                        List<int>Datas= _computadorDao.CompararTamanhoData(_computadorSaida);
                        if (Datas.Count>1)
                        {
                            if (Datas[0] == Datas[1]+1)
                            {
                                if (comboBox.SelectedItem == null)
                                {
                                    _computadorDao.InserirComputadorSaida(_computadorSaida);
                                    _computadorDao.updaterComputador(_computador);
                                    MessageBox.Show("Baixa efetuado com sucesso no computador : " + Tnome.Text);
                                }
                                else
                                {
                                    _computador.Sistema = comboBox.SelectedItem.ToString();
                                    _computadorDao.updateSistema(_computador);
                                    _computadorDao.InserirComputadorSaida(_computadorSaida);
                                    _computadorDao.updaterComputador(_computador);
                                    MessageBox.Show("Baixa efetuado com sucesso no computador : " + Tnome.Text);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Não é possivel dar baixa nesse computador mais que uma vez");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Contate o Pessoal de TI");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este computador nao existe");

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
