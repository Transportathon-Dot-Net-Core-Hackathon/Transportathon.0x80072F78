namespace Transportathon._0x80072F78.Core.DTOs.Company;

public class CommentDTO
{
    public Guid Id { get; set; }
    public Entities.Offer.Offer Offer { get; set; }
    public Entities.ForCompany.Company Company { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
    public string? Text { get; set; }
}