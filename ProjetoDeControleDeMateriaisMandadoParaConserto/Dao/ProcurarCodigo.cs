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
                string sql = "SELECT p.id, p.Nome, p.Numero, p.quantidade_conserto, GROUP_CONCAT(c.Data SEPARATOR ', ') AS Datas FROM Produto AS p INNER JOIN Conserto AS c ON p.id = c.Produto_id where p.Numero = @numero GROUP BY p.Nome ORDER BY c.Data ASC";
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
