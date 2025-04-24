using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    class CargosDto
    {
        [Required(ErrorMessage = "O Titulo é obrigatório.")]
        [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres para Titulo.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(250, ErrorMessage = "Máximo de 250 caracteres para Descrição.")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O salário base é obrigatório.")]
        [Range(2000.0, double.MaxValue, ErrorMessage = "O salário base deve ser no mínimo 2000,00.")]
        public float SalarioBase { get; set; }
    }
}
