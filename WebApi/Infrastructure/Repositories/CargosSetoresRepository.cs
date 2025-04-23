using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class CargosSetoresRepository : ICargosSetoresRepository
    {

        private readonly IDbConnection _conn;

        public CargosSetoresRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public Task<int> AdicionarCargosSetores()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> AtualizarCargosSetores()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarCargosSetores()
        {
            throw new NotImplementedException();
        }

        public Task<List<CargosSetores>> RetornarTodosCargosSetores()
        {
            throw new NotImplementedException();
        }
    }
}
