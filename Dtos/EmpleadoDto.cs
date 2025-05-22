using System;

namespace ExecutionPca.Api.Dtos
{
    public class EmpleadoDto
    {
        public decimal? Emplcod { get; set; } // W2GS78DT
        public string PrimerNombre { get; set; } = string.Empty;
        public string SegundoNombre { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public string? Email { get; set; }
        public decimal Sexo { get; set; } // W2GS7N2Z
        public string? CodigoReal { get; set; }
    }
}
