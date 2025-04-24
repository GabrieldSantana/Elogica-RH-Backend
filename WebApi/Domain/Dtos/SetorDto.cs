using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class SetorDto
{
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [MaxLength(50, ErrorMessage = "A descrição deve ter no máximo 50 caracteres.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo descrição é obrigatório.")]
    [MaxLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
    public string Descricao { get; set; }
}
