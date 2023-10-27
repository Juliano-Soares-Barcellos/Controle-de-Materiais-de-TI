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
            TelaCadastramentoFones t = new TelaCadastramentoFones(this);
            t.Show();
            this.Hide();
        }

        private void procurarPorCodigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcurarCodigoT p = new ProcurarCodigoT();
            p.Show();
        }

        private void porMêsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltroMes f = new FiltroMes();
            f.Show();
        }

        private void inserirArquivoCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrastarArquivoCsv a = new ArrastarArquivoCsv();
            a.Show();
        }

        private void criarArquivoAntesConsertoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AindaNaoConserto nao = new AindaNaoConserto(this);
            nao.Show();
            this.Hide();
        }

        private void paraConsertoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RelacaoPcsConserto r = new RelacaoPcsConserto(this);
            r.Show();
            this.Hide();
            
        }

        private void consertadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComputadorConsertado c = new ComputadorConsertado();
            c.Show();
        }

        private void listaDeTodosComputadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComputadorForm f = new ComputadorForm();
            f.Show();
           
        }

      
    }
}
