using System;
using System.Collections.Generic;

namespace pz18Request.Model;

public partial class DeviceModel
{
    public int DeviceModelId { get; set; }

    public string DeviceModelName { get; set; } = null!;

    public int DeviceTypeId { get; set; }

    public virtual DeviceType DeviceType { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
