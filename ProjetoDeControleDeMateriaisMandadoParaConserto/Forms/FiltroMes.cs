using ProjetoDeControleDeMateriaisMandadoParaConserto.Dao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Querys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Forms
{
    public partial class FiltroMes : Form
    {
        private List<Object[]> Dados;
        private DataTable tabelaPivotada;
        public FiltroMes()
        {
            InitializeComponent();
            this.MaximumSize = new System.Drawing.Size(761, 150);
            this.MinimumSize = new System.Drawing.Size(761, 150);
        }

        private void testbtn_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            button2.Visible = true;
            BtnGrafico.Visible = false;

            this.panel3.Size = new System.Drawing.Size(756, 116);
            this.panel1.Size = new System.Drawing.Size(756, 626);
            this.Size = new System.Drawing.Size(770, 776);
            this.MaximumSize = new System.Drawing.Size(770, 776);
            this.MinimumSize = new System.Drawing.Size(770, 776);

            button2.Left = 300;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            button2.Visible = false;
            BtnGrafico.Visible = true;

            this.panel3.Size = new System.Drawing.Size(510, 116);
            this.panel2.Size = new System.Drawing.Size(510, 661);
            this.Size = new System.Drawing.Size(529, 814);
            this.MaximumSize = new System.Drawing.Size(529, 814);
            this.MinimumSize = new System.Drawing.Size(529, 814);
            BtnGrafico.Left = 180;


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
        public String obterStringMes(int mesN)
        {
            switch (mesN)
            {
                case 1:
                    return "Janeiro";

                case 2:
                    return "Fevereiro";

                case 3:
                    return "Março";

                case 4:
                    return "Abril";

                case 5:
                    return "Maio";

                case 6:
                    return "Junho";

                case 7:
                    return "Julho";

                case 8:
                    return "Agosto";

                case 9:
                    return "Setembro";

                case 10:
                    return "Outubro";

                case 11:
                    return "Novembro";

                case 12:
                    return "Dezembro";

            }
            return "";
        }



        private void Carregar_Click(object sender, EventArgs e)
        {
            String PrimeiroMes = (String)ComboMes1.SelectedItem;
            String SegundoMes = (String)ComboMes2.SelectedItem;
            String GraficoSelecionado = (String)cbxGrafico.SelectedItem;
            int MesNumero1 = obterNumeroMes(PrimeiroMes);
            int MesNumero2 = obterNumeroMes(SegundoMes);
            String AnoString = TextAno.Text;
            int ano = Convert.ToInt32(AnoString);


            if (MesNumero2 < MesNumero1)
            {
                int MesPassagem = MesNumero1;
                MesNumero1 = MesNumero2;
                MesNumero2 = MesPassagem;
            }

            if (MesNumero1 < 0 || MesNumero2 < 0 || GraficoSelecionado.Equals(""))
            {
                MessageBox.Show("Por favor preencha todos os campos");

            }
            else
            {
                if (ano <= 2022 || ano > DateTime.Now.Year)
                {
                    MessageBox.Show("Por favor preencha um ano válido");

                }
                else
                {
                    SelectFiltro selectFiltro = new SelectFiltro();
                    string data1 = $"{ano}-{MesNumero1:D2}-00";
                    string data2 = $"{ano}-{MesNumero2:D2}-31";
                    Query query = new Query();
                    String sql = query.FiltroGrafico;
                    Dados = selectFiltro.BuscaPreenchimentoGrafico(data1, data2, sql);
                    graficoBox.Series.Clear();
                    graficoBox.Titles.Clear();
                    graficoBox.Legends.Clear();

                    for (int i = 0; i < Dados.Count; i++)
                    {
                        DateTime Datas = (DateTime)Dados[i][1];
                        int data = Convert.ToInt32(Datas.Month);
                        String Mes = obterStringMes(data);
                        String MesVal = $"{Dados[i][0]}-{Mes}";


                        switch (cbxGrafico.Text)
                        {
                               

                            case "Pie":

                                if (graficoBox.Series.Count <= 0)
                                {
                                    graficoBox.Series.Add(cbxGrafico.Text);
                                }
                                graficoBox.Legends.Add($"{Mes}");
                                DataPoint dataPoint = new DataPoint();
                                dataPoint.SetValueY(Dados[i][0]);
                                dataPoint.AxisLabel = MesVal;
                                dataPoint.LegendText = Mes;

                                graficoBox.Series[0].Points.Add(dataPoint);
                                graficoBox.Series[0].ChartType = SeriesChartType.Pie;
                                break;


                            case "Column":
                                if (graficoBox.Series.Count <= 0)
                                {
                                    graficoBox.Series.Add(cbxGrafico.Text);
                                }
                                graficoBox.Legends.Add($"{Mes}");
                                Series series = new Series(MesVal);
                                series.Points.Add(Double.Parse(Dados[i][0].ToString()));
                                series.LegendText = Mes;

                                series.IsValueShownAsLabel = true;
                                series.LabelFormat = "#,0"; 

                                Legend legend = graficoBox.Legends[0];
                                legend.Alignment = StringAlignment.Center;
                                legend.Docking = Docking.Top;
                                ChartArea chartArea = graficoBox.ChartAreas[0];
                                chartArea.AxisX.MajorGrid.Enabled = true;
                                chartArea.AxisY.MajorGrid.Enabled = true;

                                graficoBox.Series.Add(series);
                                break;



                            case "Line":
                                if (graficoBox.Series.Count <= 0)
                                {
                                    graficoBox.Series.Add(cbxGrafico.Text);
                                }

                                graficoBox.Series[0].Points.AddXY(MesVal, Dados[i][0]);
                                graficoBox.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), cbxGrafico.Text, true);
                                break;



                        

                            case "Spline":
                                if (graficoBox.Series.Count <= 0)
                                {
                                    graficoBox.Series.Add(cbxGrafico.Text);
                                }
                                graficoBox.Legends.Add($"{Mes}");
                                String QuantidadeJunto = $"{Mes}";

                    
                                graficoBox.Series[0].IsValueShownAsLabel = true;
                                graficoBox.Series[0].LabelFormat = "#,0"; 

                               
                                Legend legendSpline = graficoBox.Legends[0];
                                legendSpline.Alignment = StringAlignment.Center;
                                legendSpline.Docking = Docking.Top;

                                graficoBox.Series[0].Points.AddXY(QuantidadeJunto, Dados[i][0]);
                                graficoBox.Series[0].ChartType = SeriesChartType.Spline;
                                break;



                            case "Area":
                                if (graficoBox.Series.Count <= 0)
                                {
                                    graficoBox.Series.Add(cbxGrafico.Text);
                                }
                                graficoBox.Legends.Add($"{Mes}");
                                String QuantidadeJunt = $"{Mes}";

                                graficoBox.Series[0].IsValueShownAsLabel = true;
                                graficoBox.Series[0].LabelFormat = "#,0"; 

                                Legend legendArea = graficoBox.Legends[0];
                                legendArea.Alignment = StringAlignment.Center;
                                legendArea.Docking = Docking.Top;

                                graficoBox.Series[0].Points.AddXY(QuantidadeJunt, Dados[i][0]);
                                graficoBox.Series[0].ChartType = SeriesChartType.Area;
                                break;

                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int AnoNao = comboBox1.SelectedIndex;
            int MesNao = comboBox2.SelectedIndex;
            if (AnoNao < 0 || MesNao < 0)
            {
                MessageBox.Show("Por favor selecione os dois ComboBox: ");
            }
            else
            {
                String mesSelecionado = (String)comboBox2.SelectedItem;
                String anoSelecionado = (String)comboBox1.SelectedItem;
                int mesEscolhido = obterNumeroMes(mesSelecionado);
                int ano = Int32.Parse(anoSelecionado);


               SelectFiltro selectFiltro = new SelectFiltro();
                string sql = "SELECT * FROM Produto as p inner join Conserto as _computadorSaida on p.id=_computadorSaida.Produto_id WHERE MONTH(DATE(_computadorSaida.Data)) = @Mes AND YEAR(DATE(_computadorSaida.Data)) = @Ano;";
                Dados = selectFiltro.carregarTabela(mesEscolhido, ano, sql);
                if (Dados.Count==0)
                {
                    Tabela.DataSource = null;
                    label11.Text = "";
                    label8.Text = "";
                    label10.Text = "";
                    MessageBox.Show("Nenhum item encontrado");
                }
                else
                {
                    tabelaPivotada = selectFiltro.PivotData(Dados);
                    SelectTabela SelectTabel = new SelectTabela();
                    // Calcula o total de consertos e produtos com garantia
                    int MesConserto = selectFiltro.CalcularMesConserto(Dados);
                    int totalConserto = selectFiltro.CalcularTotalConserto(Dados);
                    int totalProdutosComGarantia = selectFiltro.CalcularQuantidadeGarantia(Dados);
                    List<int> Produtos = SelectTabel.HEADSETDISCADORConserto(Dados);
                    int Headset = Produtos[0];
                    int Discador = Produtos[1];
                    int Carrapatos = Produtos[2];

                    // Exibe a tabela pivotada no DataGridView
                    Tabela.DataSource = tabelaPivotada;
                    // Exibe o total de consertos e o número de produtos com garantia em MessageBox
                    MessageBox.Show("Total de Consertos neste mês: " + MesConserto + "\n" + "\n" +
                                    "Total de Headset: " + Headset + "\n" + "\n" +
                                    "total de Discadores : " + Discador + "\n" + "\n" +
                                    "Total de Carrapatos :  " + Carrapatos + "\n" + "\n" +
                                    "                   Informações do mês !!!");//+ MessageBoxButtons.OK, MessageBoxIcon.Information);

                    label4.Visible = true;
                    label11.Visible = true;
                    label8.Visible = true;
                    label10.Visible = true;
                    label13.Visible = true;
                    label12.Visible = true;
                    label9.Visible = true;
                    label4.Visible = true;
                    label11.Text = totalProdutosComGarantia.ToString();
                    label8.Text = totalConserto.ToString();
                    label10.Text = MesConserto.ToString();
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
                GravadorCsv g = new GravadorCsv();
                g.GravarCSV(tabelaPivotada);
            }
        }
    }
}







