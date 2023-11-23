namespace IrisCareSolutions.Models
{
    public class Responsavel
    {
        public int ResponsavelId { get; set; }
        public int Cpf { get; set; }
        public string? Nome { get; set; }
        public decimal Telefone { get; set; }
        public Parentesco Parentesco { get; set; }
    }

    public enum Parentesco
    {
        Pai, Mae, ResponsavelLegal
    }
}
