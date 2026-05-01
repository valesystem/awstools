using apiCirculante.Models;
using apiCirculante.Repository;

namespace apiCirculante.Application
{
    public class RotinasKnr
    {
        public async Task<int> AtualizarKnr(List<mdKnr01> lstKnrs)
        {
            string cmdSql = string.Empty;
            int id = 0;
            AppGenerico app = new AppGenerico();
            for (int i = 0; i < lstKnrs.Count; i++)
            {
                // verificar se o knr ja Existe
                cmdSql = $"select coalesce(id,0) from knr k where (planta={lstKnrs[i].planta} and knr='{lstKnrs[i].knr}')";
                try
                {
                    id = int.Parse(await app.ExecutarSQL(cmdSql, true, 0));
                }
                catch
                {
                    id = 0;
                }

                lstKnrs[i].id = id;
                await app.SalvarDados<mdKnr01>(lstKnrs[i], "knr", 0);
            }


            return 0;
        }
    }
}
