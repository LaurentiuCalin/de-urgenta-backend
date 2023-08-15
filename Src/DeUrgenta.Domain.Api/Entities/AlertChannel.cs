using System.Collections.Generic;

namespace DeUrgenta.Domain.Api.Entities;

public class AlertChannel
{
    public ushort Id { get; set; }
    public string Type { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}