using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class ComputadorForm : Form
    {
        private DataTable umPc= new DataTable();
        private DataTable retornaTudoConserto;
        private Form1 f;

        public ComputadorForm()
        {
            InitializeComponent();
            carregarTabela();
            Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
           
        }

        public void carregarTabela()
        {
           // retornaTudoConserto = computadoresMapeadosEconsertado.Dao.montarTabelasDao.Patrimonio("Maximidia","administrativo", "grupos") ;
            // isso é para usar a dll
           retornaTudoConserto = computadoresMapeadosEconsertado.Dao.montarTabelasDao.TabelaConserto();
            Tabela.DataSource = retornaTudoConserto;
          
            
            
            //computadoresMapeadosEconsertado.Dao.computadorDao.BuscaPAs();

            //            tabela pasfiltro
            //            taTable testeTabela = computadoresMapeadosEconsertado.Dao.montarTabelasDao.TabelaPas();
            //            DataTable filtrar = new DataTable();
            //            foreach (DataColumn coluna in testeTabela.Columns)
            //            {
            //                filtrar.Columns.Add(coluna.ColumnName, coluna.DataType);
            //            }

            //            foreach (DataRow item in testeTabela.Rows)
            //            {
            //                string teste = item[1].ToString();
            //                if (teste.Equals("operacao clube"))
            //                {
            //                    filtrar.Rows.Add(item.ItemArray);

            //                }


            //encontrarComputador
            //retornaTudoConserto = computadoresMapeadosEconsertado.Dao.montarTabelasDao.retornaTudoSobrePc("432");
           
            //DataTable filtrar = new DataTable();

       
            //Tabela.DataSource = retornaTudoConserto;

        }
        private void MudaCor(object sender, DataGridViewRowPrePaintEventArgs e)
        {

            if (e.RowIndex % 2 == 0)
            {
                Tabela.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                Tabela.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PaleTurquoise;
            }

            this.Tabela.GridColor = Color.BlueViolet;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Nao funciono se você nao digitar nada");
            }
            else
            {

                this.Apagar();
                TabelaDao t = new TabelaDao();
                 umPc = computadoresMapeadosEconsertado.Dao.montarTabelasDao.retornaTudoSobrePc(textBox1.Text);
                object pc=umPc.Rows.Count > 0? umPc.Rows[0][0].ToString() : "";
                if (pc.ToString()=="")
                {
                    MessageBox.Show("Computador não encontrado !");
                }
                else
                {
                   

                    Tabela.DataSource = umPc;
                    Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
                }
            }
        }



        public void Apagar()
        {
            if(retornaTudoConserto.Rows.Count > 0) retornaTudoConserto.Reset();

            if(umPc.Rows.Count>0) umPc.Reset();

            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Nao funciono se você nao escolher nada");
            }
            else
            {
                this.Apagar();
                TabelaDao t = new TabelaDao();
                umPc = computadoresMapeadosEconsertado.Dao.montarTabelasDao.MontarTabela(comboBox.SelectedItem.ToString());

           

                Tabela.DataSource = umPc;
                Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
                GravadorCsv s = new GravadorCsv();
                if (retornaTudoConserto.Columns.Count == 0)
                {
                    MessageBox.Show("Impossivel gravar a tabela zerada");
                }
                else
                {
                    s.GravarCSV(retornaTudoConserto);
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.Apagar();
            TabelaDao t = new TabelaDao();
            List<Object[]> select = t.ssd();
            DataTable tabela = new DataTable();
           

            foreach (var item in select)
            {
                tabela.Rows.Add(item);
            }

            Tabela.DataSource = retornaTudoConserto;
            Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
            checkBox2.Checked = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.Apagar();
            textBox1.Text = "";
            comboBox.Text = "";
            carregarTabela();
            Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
            checkBox3.Checked = false;
        }

        private void Tabela_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
