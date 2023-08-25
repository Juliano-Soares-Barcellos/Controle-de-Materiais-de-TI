using MySql.Data.MySqlClient;
using ProjetoDeControleDeMateriaisMandadoParaConserto.BancoConexao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Dao
{
    class AindaNaoConsertoDao
    {
        MySqlConnection con = null;

        //public void inserirProduto(Produto Produto, naoconserto conserto)
        //{
        //    try
        //    {
        //        con = new Banco().Conexao();
        //        con.Open();
        //        String sql = "INSERT INTO Produto(Nome, Numero, quantidade_conserto) VALUES (@Nome, @Numero, @Quantidade); SELECT LAST_INSERT_ID()";
        //        String sql2 = "INSERT INTO naoonserto(Data, Produto_id) VALUES (@Data, @Produto_id)";

        //        MySqlCommand comando = new MySqlCommand(sql, con);
        //        MySqlCommand incluir = new MySqlCommand(sql2, con);
        //        comando.Parameters.AddWithValue("@Nome", Produto.Nome);
        //        comando.Parameters.AddWithValue("@Numero", Produto.Numero);
        //        comando.Parameters.AddWithValue("@Quantidade", Produto.quantidade_conserto);

        //        int ProdutoId = Convert.ToInt32(comando.ExecuteScalar());
        //        conserto.Produto_id.id = ProdutoId;

        //        incluir.Parameters.AddWithValue("@Data", conserto.Data);
        //        incluir.Parameters.AddWithValue("@Produto_id", conserto.Produto_id.id);

        //        incluir.ExecuteNonQuery();
        //    }
        //    catch (MySqlException ex)
        //    {

        //        MessageBox.Show("Erro ao executar o comando SQL: " + ex.Message);
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        SelectFiltro s = new SelectFiltro();

        public void GravarCsv(List<object[]> dados)
        {
            try
            {
                string nomeArquivo = "Teste.csv"; // Nome do arquivo CSV
                string caminhoPastaAreaDeTrabalho = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                string caminhoArquivo = Path.Combine(caminhoPastaAreaDeTrabalho, nomeArquivo); // Caminho completo do arquivo CSV

                using (StreamWriter writer = new StreamWriter(caminhoArquivo, true, Encoding.UTF8))
                {

                    List<String> columnTitles = new List<String>
                    {
                     "NOME DO MATERIAL ","NUMERO", "QUANTIDADE DE CONSERTO","DATAS DE CONSERTO","DIAS DE GARANTIA ", "GARANTIA", "DATA ATUAL"
                    };
                                writer.WriteLine(String.Join(";", columnTitles));

                    foreach (Object[] rowData in dados)
                    {
                        List<String> row = new List<String>(); // Inicialize a lista row

                        for (int i = 1; i < rowData.Length; i++)
                        {
                            // Adicione os dados à lista de colunas, exceto as datas
                            if (i != rowData.Length ) // Ignora a penúltima coluna (última data antes da atual)
                            {
                                row.Add(rowData[i].ToString());
                            }
                        }
                 

                        string dataString = rowData[rowData.Length - 1].ToString();
                        string[] datas = dataString.Split(',');
                        int elemento = datas.Length;
                        DateTime dataAtual=DateTime.Now;

                        DateTime dataConserto = DateTime.MinValue;


                            if (elemento >= 2)
                            {
                            DateTime.TryParse(datas[elemento-1].Trim(), out dataConserto);
                            String dsia = s.CalcularDias(dataConserto);
                            String gasrantia = s.CalcularGarantia(dataConserto);
                            row.Add(dsia);
                            row.Add(gasrantia);


                            }
                            else
                            {
                                DateTime.TryParse(datas[elemento-1].Trim(), out dataConserto);
                                String dia = s.CalcularDias(dataConserto);
                                String garantia = s.CalcularGarantia(dataConserto);
                                row.Add(dia);
                                row.Add(garantia);


                            }

                        row.Add(dataAtual.ToString("yyyy-MM-dd"));

                        writer.WriteLine(String.Join(";", row));
                    }

                    Console.WriteLine("Dados salvos no arquivo CSV");
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Desculpe, não foi possível gravar esses dados: " + ex.Message);
                Console.WriteLine(ex.Message);
            }



        }
    }
}        
    


