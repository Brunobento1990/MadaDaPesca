﻿namespace MadaDaPesca.Application.DTOs;

public class GuiaDePescaCreateDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? UrlFoto { get; set; }
    public bool AceitoDeTermos { get; set; }
}
