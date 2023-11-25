using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrisCareSolutions.Models
{
    [Table("Tb_Tutelado")]
    public class Tutelado
    {
        [Column("Id"), HiddenInput]
        public int TuteladoId { get; set; }

        [Required, MaxLength(80)]
        public string? Nome { get; set; }

        [Required, MaxLength(11)]
        public string? Cpf { get; set; }

        [Column("Dt_Nascimento"), Display(Name = "Data de Nascimento"),
         DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Column("Ds_Modalidade"), Required, Display(Name = "Modalidade")]
        public ModalidadeAtendimento ModalidadeAtendimento { get; set; }

        public IList<Exame> Exames { get; set; }

        public IList<TuteladoLembrete> TuteladosLembretes { get; set; }

    }

    public enum ModalidadeAtendimento
    {
        Convênio, SUS
    }
}
