using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class CommentCreateDTO
{
    //public Guid Gereklibelge { get; set; } // son belge
    public int Score { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; }
}