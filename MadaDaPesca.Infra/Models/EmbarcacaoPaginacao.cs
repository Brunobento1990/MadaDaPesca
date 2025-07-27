using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Extensions;
using MadaDaPesca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MadaDaPesca.Infra.Models;

public class EmbarcacaoPaginacao : FilterModel<Embarcacao>
{
    public override Expression<Func<Embarcacao, bool>>? Where()
    {
        if (string.IsNullOrWhiteSpace(Search))
        {
            return x => x.GuiaDePescaId == GuiaDePescaId && !x.Excluido;
        }

        var searchPattern = Search.RemoverAcentos();

        return x => x.GuiaDePescaId == GuiaDePescaId && !x.Excluido &&
            EF.Functions.ILike(EF.Functions.Unaccent(x.Nome), $"%{searchPattern}%");
    }
}
