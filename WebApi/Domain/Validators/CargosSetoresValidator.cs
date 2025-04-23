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
            RuleFor(x => x.CargosId).NotEmpty().WithMessage("CargosId não pode ser vazio").
                NotNull().WithMessage("CargosId não pode ser nulo").
                GreaterThan(0).WithMessage("CargosId não pode ser nulo");

            RuleFor(x => x.SetoresId).NotEmpty().WithMessage("SetoresId não pode ser vazio").
                NotNull().WithMessage("SetoresId não pode ser nulo").
                GreaterThan(0).WithMessage("SetoresId deve ser maior que zero");

            

        }
    }
}
