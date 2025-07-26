using System.Linq.Expressions;

namespace MadaDaPesca.Domain.Models;

public abstract class FilterModel<T>
{
    public string? Search { get; set; }
    public int Skip { get; set; } = 1;
    public Guid GuiaDePescaId { get; set; }
    public int Take { get; set; } = 25;
    public string OrderBy { get; set; } = "DataDeCadastro";
    public bool Asc { get; set; } = false;
    public virtual Expression<Func<T, object>>? Include()
    {
        return null;
    }
    public abstract Expression<Func<T, bool>>? Where();
}
