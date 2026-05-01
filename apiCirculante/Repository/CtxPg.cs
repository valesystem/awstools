using Npgsql;
using System.Data;

namespace apiCirculante.Repository
{
    public class CtxPg
    {
        //Base Oficial
        //string conexaoStr = "Database=selecao;Data Source=localhost;port=3306;User Id=root;Password=root; Pooling=False; convert zero datetime=True";
        private readonly string _conexaoStr = String.Empty;


        public CtxPg(int codigoBase = 1)
        {
            //_conexaoStr = "Host=pgsql03-farm36.kinghost.net;Username=vsx1;Password=250903Cg@123;Database=vsx1";
            _conexaoStr = "Host=aws-1-us-east-1.pooler.supabase.com;Database=postgres;Username=postgres.sidmzphxvqelssnxkcqg;Password=250903Cg@123#;SSL Mode=Require;Trust Server Certificate=true";
        }



        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public async Task<DataTable> ListarDados(string cmdSql, int usuario)
        {
            var factory = new DbConnectionFactory(_conexaoStr);
            DataTable dt = new DataTable();
            using (var conexao = factory.CreateConnection())
            {
                conexao.Open();
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = cmdSql;
                    comando.CommandType = CommandType.Text;
                    var reader = await comando.ExecuteReaderAsync();
                    dt.Clear(); // apago informacoes de sql anteriores se houver ...
                    dt.Load(reader);
                }
            }
            return dt;
        }

        public async Task<string> ExecutarComandoSql(string cmdSql, bool scalar, int usuario)
        {
            string? resposta = "";
            try
            {
                var factory = new DbConnectionFactory(_conexaoStr);
                using (var conexao = factory.CreateConnection())
                {
                    conexao.Open();
                    using (var comando = conexao.CreateCommand())
                    {
                        comando.CommandText = cmdSql;
                        if (!scalar)
                            await comando.ExecuteNonQueryAsync();
                        else
                            resposta = (await comando.ExecuteScalarAsync())!.ToString();
                    }
                }
            }
            catch
            {

            }


            return resposta != null ? resposta : "";
        }

    }
}
