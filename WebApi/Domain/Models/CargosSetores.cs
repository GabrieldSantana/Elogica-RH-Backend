using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class CargosSetores
{
    [Required]
    public int CargosId { get; set; }
    [Required]
    public int SetoresId { get; set; }

}
