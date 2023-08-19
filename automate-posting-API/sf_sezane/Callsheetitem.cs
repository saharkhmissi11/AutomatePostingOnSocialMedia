using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Callsheetitem
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime Date { get; set; }

    public string? Address { get; set; }

    public int ProjectId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Calltimemember> Calltimemembers { get; set; } = new List<Calltimemember>();

    public virtual ICollection<Calltime> Calltimes { get; set; } = new List<Calltime>();

    public virtual Project Project { get; set; } = null!;
}
