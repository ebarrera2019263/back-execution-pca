namespace ExecutionPca.Api.Dtos
{
    public class DepartamentoDto
    {
        public int Empcod { get; set; }
        public int Depcod { get; set; }
        public string Depdes { get; set; } = string.Empty;
        public string? Bitacora { get; set; }
        public string? MisionParticipa { get; set; }
    }
}
