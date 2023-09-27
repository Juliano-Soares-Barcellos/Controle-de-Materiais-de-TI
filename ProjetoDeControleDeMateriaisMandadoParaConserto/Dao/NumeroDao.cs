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
   internal class NumeroDao
    {
        private MySqlConnection con = null;

        public object[] EncontrarNumero(string numero)
        {
            List<object> resultados = new List<object>();
            try
            {
                con = new Banco().Conexao();
                con.Open();
                string sql = "SELECT * FROM Produto AS p left JOIN Conserto AS _computadorSaida ON p.id = _computadorSaida.Produto_id WHERE Numero = @numero";
                MySqlCommand comando = new MySqlCommand(sql, con);
                comando.Parameters.AddWithValue("@numero", numero);
                MySqlDataReader reader = comando.ExecuteReader();
               int tamanho = reader.FieldCount;
                while (reader.Read())
                {
                    object s = reader["Id"];
                    resultados.Add(s);
                    s = reader["Nome"];
                    resultados.Add(s);
                    s = reader["Numero"];
                    resultados.Add(s);
                    s = reader["quantidade_conserto"];
                    resultados.Add(s);
                    s = reader["Data"];
                    resultados.Add(s);
                        
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return resultados.ToArray();
        }

        public void Update(string numero,string sql)
        {
            try
            {
                con = new Banco().Conexao();
                con.Open();
                MySqlCommand comando = new MySqlCommand(sql, con);
                comando.Parameters.AddWithValue("@numero", numero);
                comando.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void NovaData(int id,String sql)
        {

            try
            {
                con = new Banco().Conexao();
                //String sql = "INSERT INTO Conserto (Data,Produto_id) VALUES (@Data,@Produto_id)";
                con.Open();
                MySqlCommand comando = new MySqlCommand(sql, con);
                DateTime Data = DateTime.Now;

                comando.Parameters.AddWithValue("@Data", Data);
                comando.Parameters.AddWithValue("Produto_id", id);
                comando.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
