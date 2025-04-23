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
        Task<bool> AtualizarCargosSetores(CargosSetores cargosSetores);
        Task<List<CargosSetores>> RetornarTodosCargosSetores();
        Task<int> AdicionarCargosSetores(CargosSetores cargosSetores);
        Task<bool> DeletarCargosSetores(int id );


        
    }
}
