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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void arquivoCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void inserirFoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TelaInicial t = new TelaInicial();
            t.Show();
        }
    }
}
