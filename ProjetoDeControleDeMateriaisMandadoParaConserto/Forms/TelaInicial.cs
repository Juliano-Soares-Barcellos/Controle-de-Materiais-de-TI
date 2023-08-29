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
    public partial class TelaInicial : Form
    {
        private DataTable tabelaPivotada;
        private List<Object[]> dados;

        public TelaInicial()
        {
            InitializeComponent();
            textBox1.KeyPress += textBox1_KeyPress;
            CarregarDadosNaTabela();

        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                string message = "Deseja prosseguir? Digite 's' para sim ou 'n' para não:";
                string caption = "Confirmação";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, caption, buttons);

                if (result == DialogResult.Yes)
                {
                    NumeroDao numeroDao = new NumeroDao();
                    Teste t = new Teste();
                    string numero = textBox1.Text;
                    string NomeMaterial = comboBox.SelectedItem.ToString();
                    Object[] encontrar = numeroDao.EncontrarNumero(numero);

                    if (encontrar.Length > 0)
                    {
                        MessageBox.Show("Produto consertado novamente !");
                        string sql = "UPDATE Produto AS p  Inner JOIN Conserto as c on p.id=c.Produto_id set p.quantidade_conserto = p.quantidade_conserto  where p.Numero= @numero";
                        numeroDao.Update(numero,sql);
                        int idProduto = Convert.ToInt32(encontrar[0]);
                        String sql1 = "INSERT INTO Conserto (Data,Produto_id) VALUES (@Data,@Produto_id)";
                        numeroDao.NovaData(idProduto, sql1);
                    }
                    else
                    {
                        MessageBox.Show("Produto consertado pela primeira vez !");
                        Produto produto = new Produto();
                        produto.Nome = NomeMaterial;
                        produto.Numero = numero; // Certifique-se de que textBoxNumero contenha o número correto.
                        Conserto conserto = new Conserto();
                        conserto.Data = DateTime.Now;
                        t.inserirProduto(produto, conserto);
                    }
                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("nenhum produto foi consertado !!! ");
                }

                CarregarDadosNaTabela();
            }
        }

        private void CarregarDadosNaTabela()
        {
            SelectTabela selectTabela = new SelectTabela();
            dados = selectTabela.carregarTabela();
            // Atualizar a propriedade TotalConserto

            // Exibe a tabela pivotada no DataGridView
            tabelaPivotada = selectTabela.PivotData(dados);
            Tabela.DataSource = tabelaPivotada;


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se a tecla pressionada não é um dígito numérico ou a tecla de backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Cancela o evento de pressionar a tecla
                e.Handled = true;
            }
        }




        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GravadorCsv g = new GravadorCsv();
            g.GravarCSV(tabelaPivotada);
        }


        private void inserirArquivoCsvToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ArrastarArquivoCsv arrastarArquivoCsv = new ArrastarArquivoCsv();
            arrastarArquivoCsv.DadosEnviados += ArrastarArquivoCsv_DadosEnviados;
            arrastarArquivoCsv.ShowDialog(); // Altere aqui para usar ShowDialog()
        }

        private void qToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectTabela selectTabela = new SelectTabela();
            int HEADSET = 0;
            int CARRAPATO = 0;
            int DISCADOR = 0;
            int TotalConserto = selectTabela.CalcularTotalConserto(dados);
            int TotConserto = selectTabela.ProdutoConserto(dados);
            List<int> Consertado = selectTabela.HEADSETDISCADORConserto(dados);
            HEADSET = Consertado[0];
            DISCADOR = Consertado[1];
            CARRAPATO = Consertado[2];



            MessageBox.Show("Total de Consertos: " + TotalConserto + " \n \n quantidade de Produtos consertados : " + TotConserto +
                " \n \n quantidade de HEADSET : " + HEADSET + " \n \n quantidade de DISCADOR : " + DISCADOR + " \n \n quantidade de CARRAPATO : " + CARRAPATO);
        }

        private void filtrarPeloMêsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltroMes filtro = new FiltroMes();
            filtro.Show();
        }

        private void ProcurarProdutoExcluido(object sender, EventArgs e)
        {
            CarregarDadosNaTabela();
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcurarCodigoT p = new ProcurarCodigoT();
            // Inscreva-se no evento ProdutoExcluido
            p.ProdutoExcluido += ProcurarProdutoExcluido;
            p.Show();
        }
        private void ArrastarArquivoCsv_DadosEnviados(object sender, EventArgs e)
        {
            // Atualize a tabela na tela inicial quando os dados do arquivo forem enviados
            CarregarDadosNaTabela();
        }

     

        private void d_Click(object sender, EventArgs e)
        {
            AindaNaoConserto a = new AindaNaoConserto();
            a.Show();
        }
    }
}
