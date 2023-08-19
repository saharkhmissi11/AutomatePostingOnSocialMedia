using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Calltime
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime Time { get; set; }

    public string? Description { get; set; }

    public int CallSheetItemId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Callsheetitem CallSheetItem { get; set; } = null!;
}
