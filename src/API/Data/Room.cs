using System;
using System.Collections.Generic;

namespace API.Data;

public partial class Room
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public Guid CampId { get; set; }

    public short Capacity { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Camp Camp { get; set; } = null!;
}
