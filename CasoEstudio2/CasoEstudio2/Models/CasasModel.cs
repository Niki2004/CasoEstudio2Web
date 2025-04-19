using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CasoEstudio2.Models
{
    public class CasasModel
    {
        [Required]
        public long IdCasa { get; set; }  

        [StringLength(30)]
        public string? DescripcionCasa { get; set; }  

        [NotMapped]
        public decimal PrecioCasa { get; set; }      

        [StringLength(30)]
        public string? UsuarioAlquiler { get; set; }

        public string? Fecha { get; set; }

        public string? Estado { get; set; }
    }
}
