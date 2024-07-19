using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class RelacaoPcsConserto : Form
    {
        private Computador Computador;
        private ComputadorSaida _ComputadorSai;
        private ComputadorDao _computadorDao;
        private Form1 f;

        public RelacaoPcsConserto(Form1 f)
        {
            this.f = f;
            this._computadorDao = new ComputadorDao();
            InitializeComponent();
            Tnome.KeyPress += this.textBox1_KeyPress;
            comboboxpreenchida();



        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (Tnome.Text.Equals("") || Tdescricao.Equals(""))
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

                    int VerificarId = _computadorDao.AcharPcs(Tnome.Text);
                    Computador.Id = VerificarId;

                    if (VerificarId >= 1)
                    {
                        _ComputadorSai = new ComputadorSaida();
                        _ComputadorSai.Computador_id = Computador;
                        List<int> datas = _computadorDao.CompararTamanhoData(_ComputadorSai);
                        if (datas.Count >= 1)
                        {
                            if (datas[0] == datas[1])
                            {
                                if (comboBox.SelectedItem == null)
                                {
                                    _computadorDao.InsereDataEntrada(Tdescricao.Text, VerificarId);

                                }
                                else
                                {

                                    _computadorDao.InsereDataEntrada(Tdescricao.Text, VerificarId);
                                    Computador.Sistema = comboBox.SelectedItem.ToString();
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
                        if (!comboBox1.Text.ToString().Equals("") || comboBox1.SelectedItem != null)
                        {
                            string marca = "";
                            if (comboBox1.SelectedItem != null)
                            {
                                marca = comboBox1.SelectedItem.ToString();
                            }
                            else
                            {
                                marca = comboBox1.Text.ToString();
                            }

                            if (comboBox.SelectedItem == null)
                            {
                                String c = comboBox1.Text.ToString();

                                this.Computador = new Computador(VerificarId, Tnome.Text, marca, "", "");
                                _computadorDao.inserirComputadorEntrada(Computador, Tdescricao.Text, DateTime.Now);
                            }
                            else
                            {
                                this.Computador = new Computador(VerificarId, Tnome.Text, marca, comboBox.SelectedItem.ToString(), "");
                                _computadorDao.inserirComputadorEntrada(Computador, Tdescricao.Text, DateTime.Now);

                            }


                        }
                        else
                        {
                            MessageBox.Show("Preencha o modelo ou selecione um item");
                        }
                    }


                    comboboxpreenchida();
                }
                else
                {
                    MessageBox.Show("Problemas ao inserir no banco, favor contatar os profissionais de TI");
                }

                comboBox.SelectedIndex = -1;
                comboBox1.SelectedIndex = -1;
                Tdescricao.Text = "";
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

        private void RelacaoPcsConserto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (f.IsDisposed == false)
            {
                f.Close();
            }
        }

        private void tabelaDosComputadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComputadorForm f = new ComputadorForm();
            f.Show();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void comboboxpreenchida()
        {
            foreach (String item in _computadorDao.carregarCombobox())
            {
                comboBox1.Items.Add(item);
            }
        }

    }
}
