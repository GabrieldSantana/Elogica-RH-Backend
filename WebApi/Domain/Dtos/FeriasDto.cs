using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class FeriasDto
{


    [Required(ErrorMessage = "A data de início é obrigatória")]
    public DateTime DataInicio { get; set; }

    [Required(ErrorMessage = "A data final é obrigatória")]
    public DateTime DataFim { get; set; }

    [Required(ErrorMessage = "O ID do funcionário é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O id do funcionário inválido!")]
    public int FuncionarioId { get; set; }
}
