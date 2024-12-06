using System;
using System.Collections.Generic;

namespace pz18Request.Model;

public partial class Request
{
    public int RequestId { get; set; }

    public DateOnly DateAdded { get; set; }

    public int DeviceModelId { get; set; }

    public string ProblemDescription { get; set; } = null!;

    public int StatusId { get; set; }

    public int ClientId { get; set; }

    public int? TechnicianId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual DeviceModel DeviceModel { get; set; } = null!;

    public virtual RequestStatus Status { get; set; } = null!;

    public virtual Technician? Technician { get; set; }
}
