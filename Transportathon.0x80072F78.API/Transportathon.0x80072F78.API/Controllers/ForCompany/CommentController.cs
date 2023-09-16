using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Company
{
    public class CommentController : CustomBaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public async Task<ActionResult<CustomResponse<NoContent>>> Create(CommentCreateDTO commentCreateDTO )
        {
            return CreateActionResultInstance(await _commentService.CreateAsync(commentCreateDTO));

        }
        [HttpPut]
        public async Task<ActionResult<CustomResponse<NoContent>>> Update(CommentUpdateDTO commentUpdateDTO)
        {
            return CreateActionResultInstance(await _commentService.UpdateAsync(commentUpdateDTO));

        }
        [HttpDelete]
        public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
        {
            return CreateActionResultInstance(await _commentService.DeleteAsync(id));

        }
        [HttpGet]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAll(bool relational)
        {
            return CreateActionResultInstance(await _commentService.GetAllAsync(relational));

        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetComment(Guid id)
        {
            return CreateActionResultInstance(await _commentService.GetByIdAsync(id));

        }
    }
}
