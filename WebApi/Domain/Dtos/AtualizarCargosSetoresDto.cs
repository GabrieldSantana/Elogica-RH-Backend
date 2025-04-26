using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class AtualizarCargosSetoresDto
    {
        [Required(ErrorMessage = "O ID do cargo antigo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do cargo antigo deve ser positivo.")]
        public int CargosIdAntigo { get; set; }

        [Required(ErrorMessage = "O ID do setor antigo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do setor antigo deve ser positivo.")]
        public int SetoresIdAntigo { get; set; }

        [Required(ErrorMessage = "O ID do cargo novo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do cargo novo deve ser positivo.")]
        public int CargosIdNovo { get; set; }

        [Required(ErrorMessage = "O ID do setor novo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do setor novo deve ser positivo.")]
        public int SetoresIdNovo { get; set; }
    }
}
