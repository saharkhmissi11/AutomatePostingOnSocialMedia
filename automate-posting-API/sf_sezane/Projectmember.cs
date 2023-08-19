using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Projectmember
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int MemberId { get; set; }

    public int ProjectId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
