namespace MadaDaPesca.Domain.Models;

public class PaginacaoModel<T>
{
    public IEnumerable<T> Lista { get; set; } = [];
    public int QuantidadeDePaginas { get; set; }
}
