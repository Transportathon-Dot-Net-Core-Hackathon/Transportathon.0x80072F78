using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Infrastructure.Repository;

public class CommentRepository : AsyncRepository<Comment>, ICommentRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;

    public CommentRepository(AppDbContext AppDbContext, IFilter filter) : base(AppDbContext, filter)
    {
        _appDbContext = AppDbContext;
        _filter = filter;
    }

    public async Task<List<Comment>> GetAllCommentAsync(bool relational)
    {
        List<Comment> commentList = new();
        IQueryable<Comment> query = _appDbContext.Comments.AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(Comment));

        if (relational == true)
        {
            commentList = await query.ToListAsync();
        }
        else
        {
            commentList = await query.ToListAsync();
        }

        return commentList;
    }

    public async Task<Comment> GetCommentByIdAsync(Guid id)
    {
        Comment comment = await _appDbContext.Comments.AsNoTracking()
                                                              .Where(x => x.Id == id).
                                                                
                                                               FirstOrDefaultAsync();
        if (comment == null) return null;
        return comment;
    }
}