using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;

namespace MadaDaPesca.Infra.Repositories;

internal class GuiaDePescaRepository : GenericRepository<GuiaDePesca>, IGuiaDePescaRepository
{
    public GuiaDePescaRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
