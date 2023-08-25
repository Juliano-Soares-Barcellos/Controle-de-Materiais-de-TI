//using MySql.Data.MySqlClient;
//using ProjetoDeControleDeMateriaisMandadoParaConserto.BancoConexao;
//using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Dao
//{
//    class InseriDataNaoConserto
//    {
//        MySqlConnection con = null;

//        public void inserirProdutos(Produto Produto, naoconserto conserto)
//        {
//            try
//            {
//                con = new Banco().Conexao();
//                con.Open();
//                String sql = "INSERT INTO Produto(Nome, Numero, quantidade_conserto) VALUES (@Nome, @Numero, @Quantidade); SELECT LAST_INSERT_ID()";
//                String sql2 = "INSERT INTO naoconserto(Data, Produto_id) VALUES (@Data, @Produto_id)";

//                MySqlCommand comando = new MySqlCommand(sql, con);
//                MySqlCommand incluir = new MySqlCommand(sql2, con);
//                comando.Parameters.AddWithValue("@Nome", Produto.Nome);
//                comando.Parameters.AddWithValue("@Numero", Produto.Numero);
//                comando.Parameters.AddWithValue("@Quantidade", Produto.quantidade_conserto);

//                int ProdutoId = Convert.ToInt32(comando.ExecuteScalar());
//                conserto.Produto_id.id = ProdutoId;

//                incluir.Parameters.AddWithValue("@Data", conserto.Data);
//                incluir.Parameters.AddWithValue("@Produto_id", conserto.Produto_id.id);

//                incluir.ExecuteNonQuery();
//            }
//            catch (MySqlException ex)
//            {

//                MessageBox.Show("Erro ao executar o comando SQL: " + ex.Message);
//                Console.WriteLine(ex.Message);
//            }
//            finally
//            {
//                con.Close();
//            }
//        }

//    }
//}
