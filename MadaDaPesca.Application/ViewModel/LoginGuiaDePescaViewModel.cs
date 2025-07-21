namespace MadaDaPesca.Application.ViewModel;

public class LoginGuiaDePescaViewModel
{
    public GuiaDePescaViewModel GuiaDePesca { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
    public string TokenSchema { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
