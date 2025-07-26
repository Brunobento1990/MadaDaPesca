using Azure.Storage.Blobs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.Services;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Infra.Services;

internal class UploadImagemService : IUploadImagemService
{
    private readonly string _key;
    private readonly string _container;

    public UploadImagemService(IConfiguration configuration)
    {
        _key = configuration["Azure:Key"]!;
        _container = configuration["Azure:Container"]!;
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var blobCliente = new BlobClient(_key, _container, id.ToString());
            await blobCliente.DeleteIfExistsAsync();
        }
        catch (Exception ex)
        {
            LogService.LogApi("Erro ao excluir imagem", ex);
        }
    }

    public async Task<string> UploadAsync(string base64, Guid id)
    {
        if (string.IsNullOrWhiteSpace(base64))
        {
            throw new ValidacaoException("A imagem selecionada é inválida!");
        }

        var fotoBytes = Convert.FromBase64String(base64);

        using var foto = new MemoryStream(fotoBytes);

        var blobCliente = new BlobClient(_key, _container, id.ToString());

        await blobCliente.UploadAsync(foto);

        return blobCliente.Uri.AbsoluteUri;
    }
}
