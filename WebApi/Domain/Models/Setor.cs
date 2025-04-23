using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Setor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
}
