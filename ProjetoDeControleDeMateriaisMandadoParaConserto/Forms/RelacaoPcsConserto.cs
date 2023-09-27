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
        private ComputadorDao _computadorDao;


        public RelacaoPcsConserto()
        {
            
            this._computadorDao = new ComputadorDao();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Tnome.Text.Equals("") || comboBox.SelectedIndex == -1 || Tdescricao.Equals("") || Tmarca.Equals(""))
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
                    int VerificarId=_computadorDao.AcharPcs(Tnome.Text);
                    if (VerificarId >= 1)
                    {
                        _computadorDao.InsereDataEntrada(Tdescricao.Text, VerificarId);
                        MessageBox.Show("Eu existo");
                    }
                    else
                    {
                        this.Computador = new Computador(VerificarId,Tnome.Text, Tmarca.Text, comboBox.SelectedItem.ToString(), "");
                        _computadorDao.inserirComputadorEntrada(Computador, Tdescricao.Text, DateTime.Now);
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
    }
}
