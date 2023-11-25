using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrisCareSolutions.Models
{
    [Table("Tbl_Exame")]
    public class Exame
    {
        public int ExameId { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required]
        public DateTime Data { get; set; }

        //N:1
        public Tutelado Tutelado { get; set; }
        public int TuteladoId { get; set; }

        public string ResultadoPath { get; set; } // This property will store the path to the uploaded PDF file

        // Property to hold the uploaded PDF file
        [NotMapped] // Exclude this property from the database schema
        public IFormFile ResultadoFile { get; set; }

    }
}
