﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computadoresMapeadosEconsertado.consultas
{
    public class Query
    {
        public String buscaMapPc = "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id; ";

        public String quantidadePcGrupo = "SELECT COUNT(*) FROM computador INNER JOIN pa ON pa.id_pa = computador.fk_computador_Pa INNER JOIN grupos_pa ON grupos_pa.id_grupo = pa.fk_grupo_id WHERE grupos_pa.grupos = @grupo;";

        public String computadores_consertados = "SELECT * FROM computador INNER JOIN pa ON pa.id_pa = computador.fk_computador_Pa INNER JOIN grupos_pa ON grupos_pa.id_grupo = pa.fk_grupo_id inner join computador_conserto on computador.id_computador=computador_conserto.fk_computador;";

        public String LocalizacaoPa = "SELECT * FROM computador  INNER JOIN pa ON computador.fk_computador_Pa = pa.id_pa INNER JOIN grupos_pa  ON grupos_pa .id_grupo = pa.fk_grupo_id;";

        public String acharPcDetalhado = "SELECT * FROM computador left JOIN computador_conserto ON computador.id_computador = computador_conserto.fk_computador inner JOIN pa ON pa.id_pa = computador.fk_computador_Pa inner JOIN grupos_pa ON grupos_pa.id_grupo = pa.fk_grupo_id WHERE patrimonio=@patrimonio;;";

        public String InserirConsertoInicial = "insert into computador_conserto (data_entrada,Descricao_problema,fk_computador)values(@dataEntrada,@DescricaoProblema,@fk_computador);";

        public string queryAtualizacaoSistemaOpPc = "UPDATE computador SET sistemasOperacionais= @sistema WHERE id_computador = @IdComputador";

        public string consertoFinalizado = "update computador_conserto set descricao_problema_resolvido=@descricao ,data_conserto_finalizado=@dataFinalizado where id_conserto = @idConserto;";

        public string UpdateNosPrograma = "update computador set programa=@programa where id_computador=@id_computador;";

        public string InserirTabelaTemp = "insert into temp_conserto (fk_computador,fk_pa)values (@fk_computador,@fk_pa);";

        public string mudarParaTi = "update computador set fk_computador_pa = 340 where id_computador =  @id_computador;";

        public string selectTabelaTemp = "select * from temp_conserto where fk_computador = @id_computador;";

        public string retornarPaDeOrigem = "update computador set fk_computador_pa = @fk_pa where id_computador=@id_computador;";

        public string Condenar = "update computador set Conservacao = Condenada where id_computador=@id_computador;";

        public string DeletePaTemp = "delete from temp_conserto where id_tempConserto=@id_tempConserto;";

        public string atualizaModeloDoPc = "update computador set marca=@marca where id_computador = @id_computador;";

        public String acharPcsPeloSistema = "SELECT * FROM computador INNER JOIN pa ON pa.id_pa = computador.fk_computador_Pa INNER JOIN grupos_pa ON grupos_pa.id_grupo = pa.fk_grupo_id inner join computador_conserto on computador.id_computador=computador_conserto.fk_computador where sistemasOperacionais=@sistemasOperacionais;";


        public string TagFiltroCompMapeamento = "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id where empresa= @empresa and tag_servico = @Resfiltro;";

        public string gruposFiltroCompMapeamento= "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id where empresa= @empresa and grupos = @Resfiltro;";

        public string paFiltroCompMapeamento= "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id where empresa= @empresa and pa = @Resfiltro;";

        public string TalkfiltroCompMapeamento= "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id where empresa= @empresa and Talk_id = @Resfiltro;";

        public string PatrifiltroCompMapeamento= "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id where empresa= @empresa and patrimonio = @Resfiltro;";

        public string TodfiltroCompMapeamento = "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id where empresa= @empresa";

        public string TodClub = "select * from computador inner join pa on pa.id_pa=computador.fk_computador_Pa inner join grupos_pa on grupos_pa.id_grupo=pa.fk_grupo_id where empresa = 'clube maxivida' or grupos like '%clube%';";

        public string imobilizados = "";
    }

}