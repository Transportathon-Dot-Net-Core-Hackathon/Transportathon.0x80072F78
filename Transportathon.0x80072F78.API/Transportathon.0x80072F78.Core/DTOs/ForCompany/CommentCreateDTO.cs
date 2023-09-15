namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class CommentCreateDTO
{
    public Guid OfferId { get; set; }
    public Guid CompanyId { get; set; }
    public int Score { get; set; }
    public string? Text { get; set; }
}