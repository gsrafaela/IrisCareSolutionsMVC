namespace IrisCareSolutions.Models
{
    public class TuteladoLembrete
    {
        public int TuteladoId { get; set; }
        public Tutelado Tutelado { get; set; }
        public int LembreteId { get; set; }
        public Lembrete Lembrete { get; set; }
    }
}
