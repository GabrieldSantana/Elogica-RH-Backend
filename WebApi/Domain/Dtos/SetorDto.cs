using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class SetorDto
{
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [MaxLength(50, ErrorMessage = "A descrição deve ter no máximo 50 caracteres.")]
    // regex que recusa números e aceita espaços, acentos e etc
    [RegularExpression(@"^(?!.*--)[A-Za-zÀ-ÿ]+(?:[-\s][A-Za-zÀ-ÿ]+)*$", ErrorMessage = "O nome do setor deve conter apenas letras, espaços e hífens (sem números ou símbolos).")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo descrição é obrigatório.")]
    [MaxLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
    public string Descricao { get; set; }
}
