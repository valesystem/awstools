namespace apiCirculante.Models
{
    public class mdKnr01
    {
        public int id{ get; set; }
        public int planta {  get; set; }
        public string? knr { get; set; }
        public string? locacao { get; set; }
        public DateTime locacaodata { get; set; }
        public string? config { get; set; }
        public string? cf01 { get; set; }
        public DateTime entradadata { get; set; }
        public string? trocapara { get; set; }
        public string? trocadata { get; set; }
        public int opre { get; set; }
        public string? opredata { get; set; }
        public int pontocod { get; set; }
        public string? ponto { get; set; }
        public string? pontodata { get; set; }
        public int circulante { get; set; }
        public DateTime circulantedata { get; set; }
        public string? inftransporte { get; set; }
        public DateTime atualizado { get; set; }
    }
}
