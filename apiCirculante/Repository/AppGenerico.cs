using System.Data;

namespace apiCirculante.Repository
{
    public class AppGenerico
    {
        private readonly int _codigoBase = 11;

        public AppGenerico(int codigoBase = 11)
        {
            _codigoBase = codigoBase;
        }


        public async Task<List<T>> ListarDados<T>(string cmdSql, int usuario)
        {
            CtxPg _contexto = new CtxPg();
            ParseSql parsesql = new ParseSql();
            DataTable dt = await _contexto.ListarDados(cmdSql, usuario);
            return parsesql.DataTabletoList<T>(dt);
        }


        public async Task<int> SalvarDados<T>(T dados, string nomeTabela, int usuario)
        {
            // montar o sql dos dados insert or update
            ParseSql parsesql = new ParseSql();
            string cmdsql = parsesql.ObjetoparaSql(dados, nomeTabela);
            if (cmdsql != "")
            {
                CtxPg _contexto = new CtxPg();
                await _contexto.ExecutarComandoSql(cmdsql, false, usuario);
            }
            return 0;
        }

        public async Task<string> ExecutarSQL(string cmdSql, bool scalar, int usuario)
        {
            CtxPg _contexto = new CtxPg();
            return await _contexto.ExecutarComandoSql(cmdSql, scalar, usuario);
        }


        public async Task<DataTable> RetornarDataTable(string cmdSql, int usuario)
        {
            CtxPg _contexto = new CtxPg();
            return await _contexto.ListarDados(cmdSql, usuario);
        }
    }
}
