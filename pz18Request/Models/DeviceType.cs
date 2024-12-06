using System;
using System.Collections.Generic;

namespace pz18Request.Model;

public partial class DeviceType
{
    public int DeviceTypeId { get; set; }

    public string DeviceTypeName { get; set; } = null!;

    public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();
}
