using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.ForCompany;

public interface ICommentService 
{
    Task<CustomResponse<List<CommentDTO>>> GetAllAsync();
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> UpdateAsync(CommentUpdateDTO commentUpdateDTO );
    Task<CustomResponse<NoContent>> CreateAsync(CommentCreateDTO commentCreateDTO );
    Task<CustomResponse<CommentDTO>> GetByIdAsync(Guid id);
}