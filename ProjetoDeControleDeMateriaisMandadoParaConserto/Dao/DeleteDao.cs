using MySql.Data.MySqlClient;
using ProjetoDeControleDeMateriaisMandadoParaConserto.BancoConexao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Dao
{
    class DeleteDao
    {
        MySqlConnection con = null;

        public void deletarProduto(String numero)
        {
            List<Object[]> ArrayDeletar = new List<Object[]>(); ;
            try
            {
                con = new Banco().Conexao();
                string Sql = "Select * from Produto as p left join conserto as c on p.id=c.Produto_id where p.numero=@numero;";
                con.Open();
                MySqlCommand comando = new MySqlCommand(Sql, con);
                comando.Parameters.AddWithValue("@numero", numero);
                MySqlDataReader reader = comando.ExecuteReader();
               
                while (reader.Read())
                {
                    Object[] row = new Object[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i];
                    }
                    ArrayDeletar.Add(row);
                }
                reader.Close();

               
                int quantidadeConsertos=0;

                if (ArrayDeletar.Count > 0)
                {
                    // Usa variáveis para facilitar a leitura e evitar acessar diretamente por índices
                    int produtoStatus = Convert.ToInt32(ArrayDeletar[0][3].ToString());
                    int produtoId = Convert.ToInt32(ArrayDeletar[0][0].ToString());
                    int rownsDelete=0;
                    if(ArrayDeletar[0][6] != DBNull.Value && Convert.ToBoolean(ArrayDeletar[0][6]))
                 {
                        int consertoId = Convert.ToInt32(ArrayDeletar[0][6].ToString());
                          
                    if (produtoStatus == 1)

                    {
                        String sql0 = "delete from Conserto where Produto_id=@id;";
                        MySqlCommand deleteConserto = new MySqlCommand(sql0, con);
                        deleteConserto.Parameters.AddWithValue("@id", consertoId);
                        deleteConserto.ExecuteNonQuery();

                          string sqlUpdateQuantidadeConserto = "UPDATE Produto SET quantidade_conserto = quantidade_conserto - 1 WHERE id=@idProduto;";
                            MySqlCommand updateQuantidadeConserto = new MySqlCommand(sqlUpdateQuantidadeConserto, con);
                            updateQuantidadeConserto.Parameters.AddWithValue("@idProduto", produtoId);
                            updateQuantidadeConserto.ExecuteNonQuery();
                    }
                    else if (produtoStatus >= 2)
                    {
                        string sqlCount = "SELECT COUNT(*) FROM Conserto WHERE Produto_id=@id;";
                        MySqlCommand countConsertos = new MySqlCommand(sqlCount, con);
                        countConsertos.Parameters.AddWithValue("@id", produtoId);
                        quantidadeConsertos = Convert.ToInt32(countConsertos.ExecuteScalar());
}
                        if (quantidadeConsertos > 0)
                        {
                             
                            // Obtém a data do último registro de Conserto relacionado ao produto
                            string sqlUltimaData = "SELECT MAX(Data) FROM Conserto WHERE Produto_id=@id;";
                            MySqlCommand selectUltimaData = new MySqlCommand(sqlUltimaData, con);
                            selectUltimaData.Parameters.AddWithValue("@id", produtoId);
                            object ultimaData = selectUltimaData.ExecuteScalar();

                            if (ultimaData != DBNull.Value)
                            {
                                // Deleta o último registro de Conserto usando a data do último conserto
                                string sqlDeleteUltimoConserto = "DELETE FROM Conserto WHERE Produto_id=@id AND Data=@data;";
                                MySqlCommand deleteUltimoConserto = new MySqlCommand(sqlDeleteUltimoConserto, con);
                                deleteUltimoConserto.Parameters.AddWithValue("@id", produtoId);
                                deleteUltimoConserto.Parameters.AddWithValue("@data", ultimaData);
                                rownsDelete=deleteUltimoConserto.ExecuteNonQuery();
                            }

                         
                           string sqlUpdateQuantidadeConserto = "UPDATE Produto SET quantidade_conserto = quantidade_conserto - @rownsDelete WHERE id=@idProduto;";
                           MySqlCommand updateQuantidadeConserto = new MySqlCommand(sqlUpdateQuantidadeConserto, con);
                           updateQuantidadeConserto.Parameters.AddWithValue("@idProduto", produtoId);
                           updateQuantidadeConserto.Parameters.AddWithValue("@rownsDelete", rownsDelete);
                           updateQuantidadeConserto.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string sql1 = "delete from Produto where id=@idProduto;";
                        MySqlCommand deleteProduto = new MySqlCommand(sql1, con);
                        deleteProduto.Parameters.AddWithValue("@idProduto", produtoId);
                        deleteProduto.ExecuteNonQuery();

                    }
                    

                

                    MessageBox.Show("Produto Excluido com sucesso !!!");

                }
                else
                {
                    MessageBox.Show("Produto não encontrado");
                }


            }
            catch (MySqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
