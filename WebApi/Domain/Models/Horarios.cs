using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Horarios
    {
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "O horário de início é obrigatório.")]
        public DateTime HorarioInicio {  get; set; }
        [Required(ErrorMessage = "O horário de fim é obrigatório.")]
        public DateTime HorarioFim {  get; set; }
        [Required(ErrorMessage = "O horário de início do intervalo é obrigatório.")]
        public DateTime IntervaloInicio { get; set; }
        [Required(ErrorMessage = "O horário de fim do intervalo é obrigatório.")]
        public DateTime IntervaloFim { get; set; }
    }
}
