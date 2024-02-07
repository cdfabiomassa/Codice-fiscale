using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class DatiUtente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string Sesso { get; set; } = null!;

    public DateTime DataDiNascita { get; set; }

    public string Comune { get; set; } = null!;

    public string CodiceFiscale { get; set; } = null!;
}
