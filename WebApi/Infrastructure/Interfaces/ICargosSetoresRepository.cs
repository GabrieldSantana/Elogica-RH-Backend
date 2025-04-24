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
        Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetores);
        Task<List<CargosSetores>> BuscarCargosSetoresAsync();
        Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores);
        Task<bool> ExcluirCargosSetoresAsync(int id );


        
    }
}
