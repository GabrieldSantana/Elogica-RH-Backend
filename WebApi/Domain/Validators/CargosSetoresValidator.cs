using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class CargosSetoresValidator :  AbstractValidator<CargosSetores>
    {
        public CargosSetoresValidator()
        {
            RuleFor(x => x.CargosId).NotEmpty();
            RuleFor(x => x.SetoresId).NotEmpty();
            

        }
    }
}
