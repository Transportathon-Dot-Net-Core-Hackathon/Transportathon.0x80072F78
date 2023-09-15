namespace Transportathon._0x80072F78.Core.Entities.ForCompany;

public class Comment : BaseEntity<Guid>
{
    public Guid OfferId { get; set; }
    public Guid CompanyId { get; set; }
    public int Score{ get; set; }   
    public DateTime Date { get; set; }
    public string? Text { get; set; }
    public Guid UserId { get; set; }
}