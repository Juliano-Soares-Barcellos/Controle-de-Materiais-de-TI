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
    public partial class AindaNaoConserto : Form
    {
        private List<Object[]> Dados;
        DataTable tabelaPivotada;
        public AindaNaoConserto()
        {
            InitializeComponent();
            Dados = new List<Object[]>();
        }



        //        tabelaPivotada = selectFiltro.PivotData(Dados);
        //        Tabela.DataSource = tabelaPivotada;
        //        
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            AindaNaoConsertoDao aindaNaoConsertoDa = new AindaNaoConsertoDao();
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Digite o Material.");
            }
            else if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Escolha o nome do material.");
            }
            else
            {
                ProcurarCodigo procurarCodigo = new ProcurarCodigo();

                string message = "Deseja prosseguir? Digite 's' para sim ou 'n' para não:";
                string caption = "Confirmação";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                Conserto conserto = new Conserto();
                DialogResult result = MessageBox.Show(message, caption, buttons);

                if (result == DialogResult.Yes)
                {
                    NumeroDao numeroDao = new NumeroDao();
                    //    InseriDataNaoConserto InseriDataNaoConsert = new InseriDataNaoConserto();
                    string numero = textBox1.Text;
                    string NomeMaterial = comboBox.SelectedItem.ToString();
                    List<Object[]> novosDados = procurarCodigo.carregarTabela(numero);
                    Dados.AddRange(novosDados);

                    //
                    if (Dados.Count > 0)
                    {


                        CarregarDadosNaTabela();
                        MessageBox.Show("Produto consertado novamente !");
                    }
                    else
                    {
                        MessageBox.Show("Produto consertado pela primeira vez !");
                        Produto produto = new Produto();
                        produto.Nome = NomeMaterial;
                        produto.Numero = numero;
                        produto.quantidade_conserto = 0;

             
                    }
                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("nenhum produto foi consertado !!! ");
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ArquivoTxtDao arquivo = new ArquivoTxtDao();
            arquivo.GeararArquivoTxt(LabelHeadset.Text);
        }

        private void CarregarDadosNaTabela()
        {
          
            SelectTabela selectTabela = new SelectTabela();
            tabelaPivotada = selectTabela.PivotData(Dados);
            Tabela.DataSource = tabelaPivotada;

            


        }

        private void Tabela_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica se uma linha foi selecionada
            {
                DataGridViewRow selectedRow = Tabela.Rows[e.RowIndex]; // Obtém a linha selecionada

                string coluna1 = selectedRow.Cells[1].Value.ToString();

                DialogResult result = MessageBox.Show("Deseja excluir os dados da linha selecionada?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (object[] rowData in Dados.ToList()) 
                    {
                        if (rowData.Contains(coluna1))
                        {
                            Dados.Remove(rowData);
                            Tabela.Rows.Remove(selectedRow);

                            MessageBox.Show("Dados excluídos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break; // Interrompe o loop após a exclusão
                        }
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AindaNaoConsertoDao aindaNaoConsertoDa = new AindaNaoConsertoDao();
            aindaNaoConsertoDa.GravarCsv(Dados);
        }
    }
}
