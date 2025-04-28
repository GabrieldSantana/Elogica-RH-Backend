using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class AtualizarCargosSetoresDto
    {
        [Required(ErrorMessage = "O ID do cargo antigo é obrigatório.")]
       
        public int CargosIdAntigo { get; set; }

        [Required(ErrorMessage = "O ID do setor antigo é obrigatório.")]
       
        public int SetoresIdAntigo { get; set; }

        [Required(ErrorMessage = "O ID do cargo novo é obrigatório.")]
       
        public int CargosIdNovo { get; set; }

        [Required(ErrorMessage = "O ID do setor novo é obrigatório.")]
        
        public int SetoresIdNovo { get; set; }
    }
}
