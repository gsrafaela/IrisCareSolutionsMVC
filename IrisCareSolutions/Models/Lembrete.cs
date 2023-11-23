using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrisCareSolutions.Models
{
    [Table("Tbl_Lembrete")]
    public class Lembrete
    {
        public int LembreteId { get; set; }
        [Required]
        public string Nome { get; set; }
        [DataType(DataType.Date)]
        public DateTime Validade { get; set; }
        public string Observacao { get; set; }
        public IList<TuteladoLembrete> TuteladosLembretes { get; set; }
    }
}
