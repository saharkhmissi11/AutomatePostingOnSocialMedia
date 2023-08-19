using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Task
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int Status { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int TaskType { get; set; }

    public int ObjectId { get; set; }

    public int ObjectType { get; set; }

    public int? MemberId { get; set; }

    public int ProjectId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Checklistitem> Checklistitems { get; set; } = new List<Checklistitem>();

    public virtual Member? Member { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Taskcomment> Taskcomments { get; set; } = new List<Taskcomment>();
}
