using MySql.Data.MySqlClient;
using ProjetoDeControleDeMateriaisMandadoParaConserto.BancoConexao;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Model;
using ProjetoDeControleDeMateriaisMandadoParaConserto.Querys;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Dao
{
    internal class ComputadorDao
    {

        private MySqlConnection con = null;
        private Query sql;
        private MySqlTransaction Transacao;


        public int AcharPcs(String computador)
        {
            int id = -1;
            con = new Banco().Conexaopcs();
            con.Open();

            try
            {
                MySqlCommand cmd = con.CreateCommand();
                Transacao = con.BeginTransaction();
                cmd.Transaction = Transacao;
                //
                sql = new Query(); // instanciando para usar a query da classe
                cmd.CommandText = sql.Achou; //Achou é a query criada em outra classe, isto é um teste
                cmd.Parameters.AddWithValue("@Nome", computador);
                object tese = cmd.ExecuteScalar();
                Transacao.Commit();

                if (tese != null)
                {
                    id = Convert.ToInt32(tese);
                }

            }
            catch (MySqlException ex)
            {
                Transacao.Rollback();
                MessageBox.Show(ex.Message + "-----------" + ex.ToString());
                con.Close();
            }
            finally
            {
                Transacao.Dispose();
                con.Close();
            }
            return id;
        }


        public void inserirComputadorEntrada(Computador computador, String Descricao, DateTime data)
        {
            con = new Banco().Conexaopcs();
            con.Open();
            try
            {

                // Executar suas consultas SQL aqui com commit e rollback
                MySqlCommand comando = con.CreateCommand();
                Transacao = con.BeginTransaction();
                comando.Transaction = Transacao;
                //MySqlCommand comando = new MySqlCommand(sql, con);
                sql = new Query();
                comando.CommandText = sql.SqlInserirComputadorVoltarUltimoId; // esta é a query
                comando.Parameters.AddWithValue("@Nome", computador.Nome);
                comando.Parameters.AddWithValue("@Marca", computador.Marca);
                comando.Parameters.AddWithValue("@SistemaOperacional", computador.Sistema);
                comando.Parameters.AddWithValue("@Programas", computador.Programas);

                int id = Convert.ToInt32(comando.ExecuteScalar()); //executando tudo e voltando o id
                if (con != null)
                {
                    Transacao.Commit();
                    con.Close();

                }
                InsereDataEntrada(Descricao, id);



            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao executar o comando SQL: " + ex.Message);
                Console.WriteLine(ex.Message);
                Transacao.Rollback();
            }
            finally
            {
                Transacao.Dispose();
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        public void InserirComputadorSaida(ComputadorSaida computador)
        {
            con = new Banco().Conexaopcs();
            con.Open();
            try
            {

                Transacao = con.BeginTransaction();
                MySqlCommand comandos = con.CreateCommand();
                comandos.Transaction = Transacao;
                sql = new Query();
                comandos.CommandText = sql.InsereComputadorSaida;

                comandos.Parameters.AddWithValue("@Descricao", computador.DescricaoSaida);
                comandos.Parameters.AddWithValue("@DataSaida", DateTime.Now);
                comandos.Parameters.AddWithValue("@computador_id", computador.Computador_id.Id);
                comandos.ExecuteNonQuery();
                Transacao.Commit();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao executar o comando SQL: " + ex.Message);
                Transacao.Rollback();
            }
            finally
            {
                Transacao.Dispose();
                con.Close();
            }
        }

        public void updaterComputador(Computador computador) //nao esqueca de me usar depois do InserirComputadorSaida
        {
            try
            {
                con = new Banco().Conexaopcs();
                con.Open();
                String sele = "update computadorentrada set programas=@programas where id=@id;";
                MySqlCommand comando = new MySqlCommand(sele, con);
                comando.Parameters.AddWithValue("@programas", computador.Programas);
                comando.Parameters.AddWithValue("@id", computador.Id);
                int rown = comando.ExecuteNonQuery();
                Console.WriteLine(rown);

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }

        }


        public void InsereDataEntrada(String Descricao, int id)
        {
            con = new Banco().Conexaopcs();
            con.Open();
            try
            {
                MySqlCommand dataentrada = con.CreateCommand();

                Transacao = con.BeginTransaction();
                dataentrada.Transaction = Transacao;
                sql = new Query();
                dataentrada.CommandText = sql.InsereComputadorEntrada;
                dataentrada.Parameters.AddWithValue("@Descricao", Descricao);
                dataentrada.Parameters.AddWithValue("@data", DateTime.Now);
                dataentrada.Parameters.AddWithValue("@id", id);
                dataentrada.ExecuteNonQuery();
                Transacao.Commit();
            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao executar o comando SQL: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Transacao.Dispose();
                con.Close();
            }
        }

        public List<int> CompararTamanhoData(ComputadorSaida computador)
        {
            List<int> resultados = new List<int>();
            int TamanhoDataSaida = 0;
            con = new Banco().Conexaopcs();
            con.Open();
            try
            {
                MySqlCommand cmd = con.CreateCommand();
                Transacao = con.BeginTransaction();
                cmd.Transaction = Transacao;
                sql = new Query();
                cmd.CommandText = sql.ResQuantiDataEntrada; //Achou é a query criada em outra classe, isto é um teste
                cmd.Parameters.AddWithValue("@data_id", computador.Computador_id.Id);
                int TanhoDataEn = Convert.ToInt32(cmd.ExecuteScalar());
                Transacao.Commit();
                TamanhoDataSaida = this.TamanhoDataSaida(computador);
                resultados.Add(TanhoDataEn);
                resultados.Add(TamanhoDataSaida);
            }
            catch (MySqlException e)
            {
                Transacao.Commit();
                MessageBox.Show(e.Message);

            }
            finally
            {
                Transacao.Dispose();
                con.Close();
            }
            return resultados;
        }

        public int TamanhoDataSaida(ComputadorSaida computador)
        {
            int TanhoDataEn = 0;
            con = new Banco().Conexaopcs();
            con.Open();
            try
            {
                MySqlCommand cmd = con.CreateCommand();
                Transacao = con.BeginTransaction();
                cmd.Transaction = Transacao;
                sql = new Query();
                cmd.CommandText = sql.ResQuantidadeDataSaida; //Achou é a query criada em outra classe, isto é um teste
                cmd.Parameters.AddWithValue("@computador_id", computador.Computador_id.Id);
                TanhoDataEn = Convert.ToInt32(cmd.ExecuteScalar());
                Transacao.Commit();

            }
            catch (MySqlException e)
            {
                Transacao.Rollback();
                MessageBox.Show(e.Message);

            }
            finally
            {
                Transacao.Dispose();
                con.Close();
            }

            return TanhoDataEn;
        }
    }

}


