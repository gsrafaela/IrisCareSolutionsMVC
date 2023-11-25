using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrisCareSolutions.Models
{
    [Table("Tbl_Exame")]
    public class Exame
    {
        [Key]
        public int ExameId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório.")]
        public DateTime Data { get; set; }

        // Relacionamento com a classe Tutelado
        public Tutelado Tutelado { get; set; }

        [ForeignKey("Tutelado")]
        public int TuteladoId { get; set; }

        // Esta propriedade armazenará o caminho para o arquivo PDF enviado
        public string ResultadoPath { get; set; }

        // Esta propriedade armazenará o nome do arquivo PDF enviado
        public string ResultadoFileName { get; set; }
    }
}
