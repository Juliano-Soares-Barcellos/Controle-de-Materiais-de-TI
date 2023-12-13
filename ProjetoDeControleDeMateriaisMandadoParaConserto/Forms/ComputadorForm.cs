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
        private List<Object[]> select;

        private DataTable DateTabela;
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

                DateTabela.Rows.Add(item);

            }
            Tabela.DataSource = DateTabela;

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

                if (select.Count == 0)
                {
                    MessageBox.Show("Computador não encontrado !");
                }
                else
                {
                    this.MinhasColunas();
                    foreach (var item in select)
                    {
                        DateTabela.Rows.Add(item);
                    }

                    Tabela.DataSource = DateTabela;
                    Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
                }
            }
        }



        public void Apagar()
        {
            DateTabela.Reset();

            for (int i = 0; i < select.Count; i++)
            {
                select.RemoveAt(i);
            }
        }

        public void MinhasColunas()
        {
            DateTabela = new DataTable();
            DateTabela.Columns.Add("Data Entrada", typeof(DateTime));
            DateTabela.Columns.Add("Problema Apresentado", typeof(String));
            DateTabela.Columns.Add("Numero", typeof(String));
            DateTabela.Columns.Add("Modelo", typeof(String));
            DateTabela.Columns.Add("Sistema Operacional", typeof(String));
            DateTabela.Columns.Add("Programas", typeof(String));
            DateTabela.Columns.Add("Data de Saida", typeof(DateTime));
            DateTabela.Columns.Add("O que foi feito", typeof(String));
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
                    DateTabela.Rows.Add(item);
                }

                Tabela.DataSource = DateTabela;
                Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
                GravadorCsv s = new GravadorCsv();
                if (DateTabela.Columns.Count == 0)
                {
                    MessageBox.Show("Impossivel gravar a tabela zerada");
                }
                else
                {
                    s.GravarCSV(DateTabela);
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.Apagar();
            TabelaDao t = new TabelaDao();
            select = t.ssd();
            this.MinhasColunas();

            foreach (var item in select)
            {
                DateTabela.Rows.Add(item);
            }

            Tabela.DataSource = DateTabela;
            Tabela.RowPrePaint += new DataGridViewRowPrePaintEventHandler(MudaCor);
            checkBox2.Checked = false;
        }

    }
}
