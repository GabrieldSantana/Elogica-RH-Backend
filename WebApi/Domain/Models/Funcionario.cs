namespace Domain.Models;

public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public DateTime DataContratacao { get; set; }
    public double Salario { get; set; }
    public bool Ativo { get; set; }
    public int CargosId { get; set; }
    public int SetoresId { get; set; }
    public int HorariosId { get; set; }
}
