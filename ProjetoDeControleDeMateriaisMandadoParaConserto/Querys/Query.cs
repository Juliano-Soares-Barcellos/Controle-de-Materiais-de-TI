using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDeControleDeMateriaisMandadoParaConserto.Querys
{
   public class Query
    {
      public String Achou = "Select id from computadorentrada where nome=@nome";

      public String acharIdDataEntrada = "select max(id) from datadeentrada where data_id=@idData;";

      public String SqlInserirComputadorVoltarUltimoId = "INSERT INTO computadorentrada(Nome, Marca, SistemaOperacional,  Programas) VALUES (@Nome, @Marca, @SistemaOperacional, @Programas);SELECT LAST_INSERT_ID();";

      public String InsereComputadorEntrada = "INSERT INTO datadeentrada(Descricao, data,data_id) VALUES (@Descricao,@data,@id);";

      public String InsereComputadorSaida = "INSERT INTO computadorsaida(Descricao , DataSaida,Computador_id,computadorEntrada_id) VALUES ( @Descricao, @DataSaida, @Computador_id,@computadorEntrada_id);";

      public String ResQuantiDataEntrada = "select count(data) from datadeentrada where data_id=@data_id;";

      public String ResQuantidadeDataSaida = "select count(DataSaida) from computadorsaida where computador_Id=@computador_id";

      public String UpdateNosPrograma = "update computadorentrada set programas=@programas where id=@id;";

        public String UpdateNosSistemaOperacional = "update computadorentrada set SistemaOperacional=@Operacional where id=@id;";
    }
}
