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

        public Tutelado Tutelado { get; set; }
        public int TuteladoId { get; set; }

        public string ResultadoPath { get; set; } // Esta propriedade armazenará o caminho para o arquivo PDF enviado

        public string ResultadoFileName { get; set; } // Esta propriedade armazenará o nome do arquivo PDF enviado

    }
}
