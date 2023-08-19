using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Calltimemember
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int? MemberId { get; set; }

    public int? ContributorId { get; set; }

    public int CallSheetItemId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Callsheetitem CallSheetItem { get; set; } = null!;

    public virtual Contributor? Contributor { get; set; }

    public virtual Member? Member { get; set; }
}
