using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class CriaMenuDto
{
    [Required]
    [MaxLength(50)]
    public string Titulo { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Descricao { get; set; }
    
    [MaxLength(20)]
    public string Url { get; set; }
    
    [MaxLength(40)]
    public string Icone { get; set; }
    
    [Required]
    public int Ordem { get; set; }
    
    public int? MenuPaiId { get; set; }
}