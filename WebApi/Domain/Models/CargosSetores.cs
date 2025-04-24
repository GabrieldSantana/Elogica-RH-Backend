using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class CargosSetores
{
    [Required]
    public int CargosId { get; set; }
    [Required]
    public int SetoresId { get; set; }

}
