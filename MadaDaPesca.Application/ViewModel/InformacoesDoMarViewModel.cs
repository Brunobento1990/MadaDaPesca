﻿namespace MadaDaPesca.Application.ViewModel;

public class InformacoesDoMarViewModel
{
    public IEnumerable<DateTime> Dias { get; set; } = [];
    public IEnumerable<double> AlturasDasOndas { get; set; } = [];
    public IEnumerable<double> TemperaturasDoMar { get; set; } = [];
    public IEnumerable<double> AlturasDaMare { get; set; } = [];
    public int PrevisaoDeDias { get; set; }
    public UnidadeDeMedidaMarViewModel UnidadeDeMedida { get; set; } = null!;
}

public class UnidadeDeMedidaMarViewModel
{
    public string AlturaDaOnda { get; set; } = string.Empty;
    public string TemperaturaDoMar { get; set; } = string.Empty;
    public string AlturaDaMare { get; set; } = string.Empty;
}
