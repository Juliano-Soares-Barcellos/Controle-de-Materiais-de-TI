using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
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
    public partial class FiltroMes : Form
    {
        DataTable tabelaPivotada;
        private List<Object[]> Dados;
        public FiltroMes()
        {
            InitializeComponent();
        }


        private void BotaoProcurar_Click(object sender, EventArgs e)
        {
            int AnoNao = ComboBoxAno.SelectedIndex;
            int MesNao = ComboBoxMes.SelectedIndex;
            if (AnoNao < 0 || MesNao < 0)
            {
                MessageBox.Show("Por favor selecione os dois ComboBox: ");
            }
            else
            {
                String mesSelecionado = (String)ComboBoxMes.SelectedItem;
                String anoSelecionado = (String)ComboBoxAno.SelectedItem;
                int mesEscolhido = obterNumeroMes(mesSelecionado);
                int ano = Int32.Parse(anoSelecionado);

                SelectFiltro selectFiltro = new SelectFiltro();
                string sql = "SELECT * FROM Produto as p inner join Conserto as c on p.id=c.Produto_id WHERE MONTH(DATE(c.Data)) = @Mes AND YEAR(DATE(c.Data)) = @Ano;";
                Dados = selectFiltro.carregarTabela(mesEscolhido, ano,sql);
                tabelaPivotada = selectFiltro.PivotData(Dados);
                SelectTabela SelectTabel = new SelectTabela();
                // Calcula o total de consertos e produtos com garantia

                int MesConserto = selectFiltro.CalcularMesConserto(Dados);

               // int totalConserto = selectFiltro.CalcularTotalConserto(Dados);

               // int totalProdutosComGarantia = selectFiltro.CalcularQuantidadeGarantia(Dados);

               List<int> Produtos=SelectTabel.HEADSETDISCADORConserto(Dados);
                int Headset=Produtos[0];
                int Discador=Produtos[1];
                int Carrapatos=Produtos[2];

                //
                // Exibe a tabela pivotada no DataGridView
                Tabela.DataSource = tabelaPivotada;

                // Exibe o total de consertos e o número de produtos com garantia em MessageBox
                MessageBox.Show("Total de Consertos neste mês: " + MesConserto + "\n" + "\n" +
                                "Total de Headset: " + Headset+ "\n" + "\n" +
                                "total de Discadores : " + Discador +"\n"+"\n"+
                                "Total de Carrapatos :  "+ Carrapatos+"\n"+"\n"+
                                "                   Informações do mês !!!");//+ MessageBoxButtons.OK, MessageBoxIcon.Information);

                label4.Visible = true;
                LabelGarantia.Visible = true;
                LabelConsertado.Visible = true;
                label7.Visible = true;
                label5.Visible = true;
                label6.Visible = true;

               // LabelGarantia.Text = totalProdutosComGarantia.ToString();
                //LabelConsertado.Text = totalConserto.ToString();
                label7.Text = MesConserto.ToString();
            }
        }

        public int obterNumeroMes(string mesSelecionado)
        {
            switch (mesSelecionado)
            {
                case "Janeiro":
                    return 01;

                case "Fevereiro":
                    return 02;

                case "Março":
                    return 03;

                case "Abril":
                    return 04;

                case "Maio":
                    return 05;

                case "Junho":
                    return 06;

                case "Julho":
                    return 07;

                case "Agosto":
                    return 08;

                case "Setembro":
                    return 09;

                case "Outubro":
                    return 10;

                case "Novembro":
                    return 11;

                case "Dezembro":
                    return 12;

            }
            return 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GravadorCsv g = new GravadorCsv();
            g.GravarCSV(tabelaPivotada);
        }

      
    }
}
