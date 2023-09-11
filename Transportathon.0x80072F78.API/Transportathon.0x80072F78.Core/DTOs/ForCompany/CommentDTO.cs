using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.DTOs.Company;

public class CommentDTO
{
    //public Guid Gereklibelge { get; set; } // son belge
    public int Score { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; }
}