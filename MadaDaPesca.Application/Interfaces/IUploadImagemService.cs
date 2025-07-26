namespace MadaDaPesca.Application.Interfaces;

public interface IUploadImagemService
{
    Task<string> UploadAsync(string base64, Guid id);
    Task DeleteAsync(Guid id);
}
