using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class RelacaoPcsConserto : Form
    {
        private Computador Computador;
        private ComputadorDao _computadorDao;
        private Form1 f;

        public RelacaoPcsConserto(Form1 f)
        {
            this.f = f;
            this._computadorDao = new ComputadorDao();
            InitializeComponent();
            Tnome.KeyPress += this.textBox1_KeyPress;
            comboboxpreenchida(comboBox1);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (Tnome.Text.Equals("") || Tdescricao.Text.Equals(""))
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
                    DataTable TodosDadosDoPc = computadoresMapeadosEconsertado.Dao.montarTabelasDao.retornaTudoSobrePc(Tnome.Text);
                    object valor = null;
                 try
                  {
                    valor = TodosDadosDoPc.Rows[0][0];

                    if (valor.ToString() != "")
                    {
                          int ultima = TodosDadosDoPc.Rows.Count - 1;
                            if (TodosDadosDoPc.Rows[ultima][9].ToString()=="" || (TodosDadosDoPc.Rows[ultima][9].ToString() != "" && TodosDadosDoPc.Rows[ultima][13].ToString() != ""))
                            {
                                string modelo = "";
                                if( comboBox1.SelectedIndex >=0 ) modelo=comboBox1.SelectedItem.ToString();
                                string id_pa = TodosDadosDoPc.Rows[ultima][5].ToString();

                                if (!modelo.Equals(""))computadoresMapeadosEconsertado.Dao.UpdateDao.InseririModelo(valor.ToString(), modelo);

                             
                                    computadoresMapeadosEconsertado.Dao.InsersaoDao.InserirComputador(Tdescricao.Text, valor.ToString());
                                    computadoresMapeadosEconsertado.Dao.InsersaoDao.InserirPaTabelaTemp(valor.ToString(),id_pa);
                                    computadoresMapeadosEconsertado.Dao.UpdateDao.updatePaTi(valor.ToString());
                                    String Sistema = comboBox.SelectedIndex  <= 0?"": comboBox.SelectedItem.ToString();

                                if (!Sistema.Equals(""))computadoresMapeadosEconsertado.Dao.UpdateDao.AtualizarComputador(valor.ToString(), Sistema);
                                MessageBox.Show("Computador mandado para conserto com sucesso !");
                            }
                            else
                            {
                                MessageBox.Show("este computador ja esta em processo de conserto");
                            }
                        }
                    }
                catch(Exception ex)
                {
                    MessageBox.Show("Esse computador nao existe !");
                }

                    comboboxpreenchida(comboBox1);
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

        public  void comboboxpreenchida(ComboBox combo)
        {
            combo.Items.Clear();
            foreach (String item in _computadorDao.carregarCombobox())
            {
                combo.Items.Add(item);
            }
        }

    }
}
