using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class ComputadorForm : Form
    {
        private List<Object[]> select;

        private DataTable teste;
        private Form1 f;

        public ComputadorForm()
        {
            InitializeComponent();
            carregarTabela();
            Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
        }

        public void carregarTabela()
        {
            TabelaDao t = new TabelaDao();
            select = t.TodaTabela();
            this.MinhasColunas();
            foreach (var item in select)
            {

                teste.Rows.Add(item);

            }
            Tabela.DataSource = teste;

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
                select = t.pegarNumnero(textBox1.Text);
                this.MinhasColunas();
                foreach (var item in select)
                {

                    teste.Rows.Add(item);

                }

                Tabela.DataSource = teste;
                Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
            }
        }



        public void Apagar()
        {
            teste.Reset();

            for (int i = 0; i < select.Count; i++)
            {
                select.RemoveAt(i);
            }
        }

        public void MinhasColunas()
        {
            teste = new DataTable();
            teste.Columns.Add("Data Entrada", typeof(DateTime));
            teste.Columns.Add("Problema Apresentado", typeof(String));
            teste.Columns.Add("Numero", typeof(String));
            teste.Columns.Add("Marca", typeof(String));
            teste.Columns.Add("Sistema Operacional", typeof(String));
            teste.Columns.Add("Programas", typeof(String));
            teste.Columns.Add("Data de Saida", typeof(DateTime));
            teste.Columns.Add("O que foi feito", typeof(String));
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
                select = t.pegarSistema(comboBox.SelectedItem.ToString());
                this.MinhasColunas();
                foreach (var item in select)
                {
                    teste.Rows.Add(item);
                }
                Tabela.DataSource = teste;
                Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GravadorCsv s = new GravadorCsv();
            s.GravarCSV(teste);
            checkBox1.Checked=false;
        }
    }
}
