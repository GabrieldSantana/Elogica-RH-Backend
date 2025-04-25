using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class FuncionarioDto
{
    [Required(ErrorMessage = "O nome é um campo obrigatório.")]
    [MaxLength(50, ErrorMessage = "O nome só pode possuir no máximo 50 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CPF é um campo obrigatório.")]
    [MaxLength(11, ErrorMessage = "O CPF só pode possuir no máximo 11 caracteres.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "A Data de nascimento é um campo obrigatório.")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O E-mail é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O E-mail só pode possuir no máximo 50 caracteres.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O Telefone é obrigatório.")]
    [MaxLength(11, ErrorMessage = "O Telefone só pode possuir no máximo 11 caracteres.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O Endereço é obrigatório.")]
    [MaxLength(250, ErrorMessage = "O Endereço só pode possuir no máximo 250 caracteres.")]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "A Data de contratação é um campo obrigatório.")]
    public DateTime DataContratacao { get; set; }

    [Required(ErrorMessage = "O Salário é obrigatório.")]
    [Range(2000, double.MaxValue, ErrorMessage = "Salário inválido.")]
    public double Salario { get; set; }

    [Required(ErrorMessage = "A Situação do funcionário é um campo obrigatório")]
    public bool Ativo { get; set; }

    [Required(ErrorMessage = "O Id do cargo é obrigatório.")]
    [Range(1,int.MaxValue, ErrorMessage = "O id do cargo foi inválido.")]
    public int CargosId { get; set; }

    [Required(ErrorMessage = "O Id do setor é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O id do setor foi inválido.")]
    public int SetoresId { get; set; }

    [Required(ErrorMessage = "O Id do horário é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O id do horário foi inválido.")]
    public int HorariosId { get; set; }
}
