using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Contributor
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int Status { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Role { get; set; }

    public string? Type { get; set; }

    public string? Agence { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Calltimemember> Calltimemembers { get; set; } = new List<Calltimemember>();

    public virtual ICollection<Contributorsinproject> Contributorsinprojects { get; set; } = new List<Contributorsinproject>();
}
