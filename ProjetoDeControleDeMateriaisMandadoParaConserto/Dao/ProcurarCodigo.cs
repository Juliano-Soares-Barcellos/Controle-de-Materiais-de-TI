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
    class ProcurarCodigo
    {
        public MySqlConnection con = null;

        public List<Object[]> carregarTabela(string Numero)
        {
            List<Object[]> resultados = new List<Object[]>();

            try
            {
                con = new Banco().Conexao();
                con.Open();
            //    string sql = "SELECT p.id, p.Nome, p.Numero, p.quantidade_conserto, GROUP_CONCAT(_computadorSaida.Data SEPARATOR ', ') AS Datas FROM Produto AS p INNER JOIN Conserto AS _computadorSaida ON p.id = _computadorSaida.Produto_id where p.Numero = @numero GROUP BY p.Nome ORDER BY _computadorSaida.Data ASC";
                  string sql = "SELECT p.id, p.Nome, p.Numero, p.quantidade_conserto, GROUP_CONCAT(_computadorSaida.Data SEPARATOR ', ') AS Datas FROM Produto AS p LEFT JOIN Conserto AS _computadorSaida ON p.id = _computadorSaida.Produto_id where p.Numero = @numero GROUP BY p.Nome ORDER BY _computadorSaida.Data ASC";
            
                MySqlCommand comando = new MySqlCommand(sql, con);
                comando.Parameters.AddWithValue("@numero", Numero);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Object[] row = new Object[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i];
                    }

                    resultados.Add(row);
                }

                reader.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return resultados;
        }

    }
}
