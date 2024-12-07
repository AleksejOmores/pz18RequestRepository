using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace pz18Request.Model;

public partial class Request
{
    [JsonRequired]
    [JsonPropertyName("id")]
    public int RequestId { get; set; }

    [JsonRequired]
    [JsonPropertyName("date_added")]
    public DateOnly DateAdded { get; set; }

    public int DeviceModelId { get; set; }

    [JsonRequired]
    [JsonPropertyName("problem_description")]
    public string ProblemDescription { get; set; } = null!;

    public int StatusId { get; set; }

    public int ClientId { get; set; }

    public int? TechnicianId { get; set; }

    public virtual Client Client { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual DeviceModel DeviceModel { get; set; } = null!;

    public virtual RequestStatus Status { get; set; } = null!;

    public virtual Technician? Technician { get; set; }
}
