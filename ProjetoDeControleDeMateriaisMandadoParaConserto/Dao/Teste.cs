using MySql.Data.MySqlClient;
using ProjetoDeControleDeMateriaisMandadoParaConserto.BancoConexao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Dao
{
    class Teste
    {
        MySqlConnection con = null;

        public void inserirProduto(Produto Produto, Conserto conserto)
        {
            try
            {
                con = new Banco().Conexao();
                con.Open();
                String sql = "INSERT INTO Produto(Nome, Numero, quantidade_conserto) VALUES (@Nome, @Numero, @Quantidade+1); SELECT LAST_INSERT_ID()";
                String sql2 = "INSERT INTO Conserto(Data, Produto_id) VALUES (@Data, @Produto_id)";

                MySqlCommand comando = new MySqlCommand(sql, con);
                MySqlCommand incluir = new MySqlCommand(sql2, con);
                comando.Parameters.AddWithValue("@Nome", Produto.Nome);
                comando.Parameters.AddWithValue("@Numero", Produto.Numero);
                comando.Parameters.AddWithValue("@Quantidade", Produto.quantidade_conserto);

                int ProdutoId = Convert.ToInt32(comando.ExecuteScalar());
                conserto.Produto_id.id = ProdutoId;

                incluir.Parameters.AddWithValue("@Data", conserto.Data);
                incluir.Parameters.AddWithValue("@Produto_id", conserto.Produto_id.id);

                incluir.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        public void InsereBancoArquivo(List<Object[]> s)
        {
            try
            {
                if (s.Count > 0)
                {
                    con = new Banco().Conexao();
                    con.Open();

                    string sqlProduto = "INSERT INTO Produto(Nome, Numero, quantidade_conserto) VALUES (@Nome, @Numero, @Quantidade)";
                    string sqlConserto = "INSERT INTO Conserto(Data, Produto_id) VALUES (@Data, @Produto_id)";

                    MySqlCommand comandoProduto = new MySqlCommand(sqlProduto, con);
                    MySqlCommand comandoConserto = new MySqlCommand(sqlConserto, con);

                    foreach (Object[] valores in s)
                    {
                        string nome = valores[0].ToString();
                        string numero = valores[1].ToString();
                        int quantidadeConserto = Convert.ToInt32(valores[2]);

                        if (quantidadeConserto == 0)
                        {
                            comandoProduto.Parameters.Clear(); // Limpar parâmetros antes de cada inserção
                            comandoProduto.Parameters.AddWithValue("@Nome", nome);
                            comandoProduto.Parameters.AddWithValue("@Numero", numero);
                            comandoProduto.Parameters.AddWithValue("@Quantidade", quantidadeConserto + 1);
                            comandoProduto.ExecuteNonQuery();
                            int produtoId = Convert.ToInt32(comandoProduto.LastInsertedId);

                            DateTime data = DateTime.Now;
                            comandoConserto.Parameters.Clear();
                            comandoConserto.Parameters.AddWithValue("@Data", data);
                            comandoConserto.Parameters.AddWithValue("@Produto_id", produtoId);

                            comandoConserto.ExecuteNonQuery();


                        }
                        else
                        {
                            comandoProduto.Parameters.Clear(); // Limpar parâmetros antes de cada inserção
                            comandoProduto.Parameters.AddWithValue("@Nome", nome);
                            comandoProduto.Parameters.AddWithValue("@Numero", numero);
                            comandoProduto.Parameters.AddWithValue("@Quantidade", quantidadeConserto);
                            comandoProduto.ExecuteNonQuery();


                            // Verifica se há datas presentes no array de valores
                            for (int i = 3; i < valores.Length; i++)
                            {
                                if (DateTime.TryParse(valores[i].ToString(), out DateTime dataConserto))
                                {
                                    int produtoId = Convert.ToInt32(comandoProduto.LastInsertedId);

                                    comandoConserto.Parameters.Clear(); // Limpar parâmetros antes de cada inserção
                                    comandoConserto.Parameters.AddWithValue("@Data", dataConserto); // Você precisa definir a data corretamente aqui
                                    comandoConserto.Parameters.AddWithValue("@Produto_id", produtoId);

                                    comandoConserto.ExecuteNonQuery();
                                }
                                else
                                {
                                    int produtoId = Convert.ToInt32(comandoProduto.LastInsertedId);
                                    DateTime data = DateTime.Now;
                                    comandoConserto.Parameters.Clear();
                                    comandoConserto.Parameters.AddWithValue("@Data", data);
                                    comandoConserto.Parameters.AddWithValue("@Produto_id", produtoId);

                                    comandoConserto.ExecuteNonQuery();
                                }
                            }

                        }

                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
