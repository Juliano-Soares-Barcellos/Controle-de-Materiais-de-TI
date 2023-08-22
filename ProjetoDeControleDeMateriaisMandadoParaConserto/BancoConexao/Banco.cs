using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.BancoConexao
{
    class Banco
    {
        private string connection = "Data Source=localhost;Username=root;Password=;Database=planilhas;";

        public MySqlConnection Conexao()

        {
            return new MySqlConnection(connection);
        }
    }
}
