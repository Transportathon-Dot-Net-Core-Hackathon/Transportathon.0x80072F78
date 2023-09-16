namespace Transportathon._0x80072F78.Core.Entities;

public class Message : BaseEntity<Guid>
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string MessageContent { get; set; }
    public DateTime SendTime { get; set; }
}