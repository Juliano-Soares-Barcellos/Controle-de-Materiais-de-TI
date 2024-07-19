using computadoresMapeadosEconsertado.consultas;
using computadoresMapeadosEconsertado.Data;
using computadoresMapeadosEconsertado.model;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computadoresMapeadosEconsertado.Dao
{
    public class InsersaoDao
    {

        public static void InserirComputador(string Tdescricao, string VerificarId)
        {
            Query query = new Query();
            computador_conserto pc = new computador_conserto();
            var parametros = new
            {
                dataEntrada = DateTime.Now,
                DescricaoProblema = Tdescricao,
                fk_computador = VerificarId,
            };

            using (var conexao = new banco().conexao())
            {
                conexao.Open();
                int linhasAfetadas = conexao.Execute(query.InserirConsertoInicial, parametros);
            }
        }

        public static void InserirPaTabelaTemp(string fk_computador, string fk_pa)
        {
            Query query = new Query();
            computador_conserto pc = new computador_conserto();
            var parametros = new
            {
               fk_computador  = fk_computador,
               fk_pa = fk_pa,
            };

            using (var conexao = new banco().conexao())
            {
                conexao.Open();
                int linhasAfetadas = conexao.Execute(query.InserirTabelaTemp, parametros);
            }
        }

        public static int inserirComputadorNovo(MySqlParameter [] parametro )
        {
            int rowns = 0;
            var con = new banco().conexao();
            Query query = new Query();
            try
            {
                con.Open();
                MySqlCommand comando = new MySqlCommand(query.inseriComputadorNovo, con);
                comando.Parameters.AddRange(parametro);
                rowns = comando.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }finally
            {
                con.Close();
            }

            return rowns;
        }

    }
}
