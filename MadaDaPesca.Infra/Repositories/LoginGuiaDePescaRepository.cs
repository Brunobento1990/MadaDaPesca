using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MadaDaPesca.Infra.Repositories;

internal class LoginGuiaDePescaRepository : ILoginGuiaDePescaRepository
{
    private readonly AppDbContext _appDbContext;

    public LoginGuiaDePescaRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<GuiaDePesca?> LoginAsync(string cpf)
    {
        return await _appDbContext
            .GuiasDePesca
            .AsNoTracking()
            .Include(x => x.Pessoa)
            .Include(x => x.AcessoGuiaDePesca)
            .FirstOrDefaultAsync(x => x.Pessoa.Cpf == cpf && !x.Excluido && !x.Pessoa.Excluido);
    }
}
