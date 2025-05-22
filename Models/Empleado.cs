using System;

namespace ExecutionPca.Api.Models
{
    public class Empleado
    {
        public decimal Emplcod { get; set; }               // W2GS78DT
        public string PrimerNombre { get; set; } = "";     // W2GS7FDI
        public string SegundoNombre { get; set; } = "";    // W2GS7FD3
        public string PrimerApellido { get; set; } = "";   // W2GS7ISI
        public string SegundoApellido { get; set; } = "";  // W2GS7IS3
        public decimal UsuarioId { get; set; }             // VNVIMLD
        public DateTime? FechaNacimiento { get; set; }     // QSNDLQEATE
        public string? Bitacora { get; set; }              // W85V3TSN3
        public decimal Sexo { get; set; }                  // W2GS7N2Z
        public string? ApellidoCasada { get; set; }        // W2GS7IS8
        public string? Email { get; set; }                 // W5MEFAELLX5ZZ
        public string? CodigoReal { get; set; }            // EM475MVQTD
        public string? SiempreN { get; set; }              // W2GS7LFN
    }
}
