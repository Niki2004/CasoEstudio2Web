using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CasoEstudio2.Models
{
    public class CasasModel
    {
        public long IdCasa { get; set; }  

        [StringLength(30)]
        public string DescripcionCasa { get; set; }  

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioCasa { get; set; }      

        [StringLength(30)]
        public string UsuarioAlquiler { get; set; }

        public string Fecha { get; set; }

        public string Estado { get; set; }
    }
}
