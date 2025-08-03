using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Extensions;
using MadaDaPesca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MadaDaPesca.Infra.Models;

public class FaturaPaginacao : FilterModel<FaturaAgendaPescaria>
{
    public override Expression<Func<FaturaAgendaPescaria, bool>>? Where()
    {
        if (string.IsNullOrWhiteSpace(Search))
        {
            return x => x.GuiaDePescaId == GuiaDePescaId && !x.Excluido;
        }

        var searchPattern = Search.RemoverAcentos();

        return x => x.GuiaDePescaId == GuiaDePescaId && !x.Excluido &&
                    EF.Functions.ILike(EF.Functions.Unaccent(x.Descricao!), $"%{searchPattern}%");
    }

    public override Expression<Func<FaturaAgendaPescaria, object>>? Include()
    {
        return x => x.Transacoes!;
    }
}
