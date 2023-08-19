using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Contributorsinproject
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int ProjectId { get; set; }

    public int ContributorId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Contributor Contributor { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
