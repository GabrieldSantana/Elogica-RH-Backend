namespace Domain.Models
{
    public class Cargos
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public float SalarioBase { get; set; }
        public List<Setor> Setores { get; set; } = new List<Setor>();
    }
}
