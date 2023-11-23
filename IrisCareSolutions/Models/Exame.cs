using System;
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

        // N:1
        public Tutelado Tutelado { get; set; }
        public int TuteladoId { get; set; }

        [Required]
        public byte[] ResultadoData { get; set; }
        public string ResultadoFileName { get; set; }
    }
}
