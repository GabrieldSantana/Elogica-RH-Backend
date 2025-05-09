﻿using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface ICargosRepository
    {
        Task<RetornoPaginado<Cargos>> BuscarCargosPaginadoAsync(int pagina, int quantidade);
        Task<IEnumerable<Cargos>> BuscarCargosAsync();
        Task<Cargos> BuscarCargosPorIdAsync(int id);
        Task<bool> AtualizarCargosAsync(Cargos cargos);
        Task<bool> ExcluirCargosAsync(int id);
        Task<bool> AdicionarCargosAsync(Cargos cargos);
    }
}
