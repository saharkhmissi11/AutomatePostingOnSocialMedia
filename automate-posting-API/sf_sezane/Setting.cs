using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Setting
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? SettingName { get; set; }

    public string? SettingValue { get; set; }

    public string TenantId { get; set; } = null!;
}
