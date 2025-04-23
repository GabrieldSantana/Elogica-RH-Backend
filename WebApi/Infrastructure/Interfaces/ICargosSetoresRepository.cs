using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface ICargosSetoresRepository
    {
        Task<bool> AtualizarCargosSetores();
        Task<List<CargosSetores>> RetornarTodosCargosSetores();
        Task<int> AdicionarCargosSetores();
        Task<bool> DeletarCargosSetores();


        
    }
}
