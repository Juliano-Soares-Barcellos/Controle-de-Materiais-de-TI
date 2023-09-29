using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class RelacaoPcsConserto : Form
    {  
        private Computador Computador;
        private ComputadorSaida _ComputadorSai;
        private ComputadorDao _computadorDao;

        public RelacaoPcsConserto()
        {
            this._computadorDao = new ComputadorDao();
            InitializeComponent();
            Tnome.KeyPress += this.textBox1_KeyPress;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Tnome.Text.Equals("") || Tdescricao.Equals("") || Tmarca.Equals(""))
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
                    Computador = new Computador();
                    Computador.Descricao = Tdescricao.Text;

                    int VerificarId=_computadorDao.AcharPcs(Tnome.Text);
                    Computador.Id = VerificarId;
                    
                    if (VerificarId >= 1)
                    {
                        _ComputadorSai = new ComputadorSaida();
                        _ComputadorSai.Computador_id = Computador;
                       List<int> datas=_computadorDao.CompararTamanhoData(_ComputadorSai);
                        if (datas.Count >= 1)
                        {
                            if (datas[0] == datas[1] )
                            {
                                if (comboBox.SelectedItem == null)
                                {
                                    _computadorDao.InsereDataEntrada(Tdescricao.Text, VerificarId);
                                    MessageBox.Show("Eu existo");
                                }
                                else
                                {
                                    _computadorDao.InsereDataEntrada(Tdescricao.Text, VerificarId);
                                    Computador.Sistema= comboBox.SelectedItem.ToString();
                                    _computadorDao.updateSistema(Computador);

                                }
                            }
                            else
                            {
                                MessageBox.Show("Voce nao pode Inserir ele se nao consertou da primeira vez");
                            }
                        }
                    }
                    else
                    {
                        if (comboBox.SelectedItem==null)
                        {
                            this.Computador = new Computador(VerificarId, Tnome.Text, Tmarca.Text, "", "");
                            _computadorDao.inserirComputadorEntrada(Computador, Tdescricao.Text, DateTime.Now);
                        }
                        else
                        {
                            this.Computador = new Computador(VerificarId, Tnome.Text, Tmarca.Text, comboBox.SelectedItem.ToString(), "");
                            _computadorDao.inserirComputadorEntrada(Computador, Tdescricao.Text, DateTime.Now);

                        }


                    }
                      
                }
                else
                {
                    MessageBox.Show("Problemas ao inserir no banco, favor contatar os profissionais de TI");
                }

                Tnome.Text = "";
                comboBox.SelectedIndex = -1;
                Tdescricao.Text = "";
                Tmarca.Text = "";
            }
        }

        private void computadorConsertadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComputadorConsertado _computadoresConsertados = new ComputadorConsertado();
            _computadoresConsertados.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
