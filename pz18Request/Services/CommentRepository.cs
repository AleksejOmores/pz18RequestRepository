using Microsoft.EntityFrameworkCore;
using pz18Request.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request.Services
{
    internal class CommentRepository : ICommentRepository
    {
        readonly RegApplicationContext _context = new RegApplicationContext();
        public Task<List<Comment>> GetCommentAsync()
        {
            return _context.Comments.ToListAsync();
        }

        public Task<List<Comment>> GetCommentByRequestAsync(int requestId)
        {
            return _context.Comments.Where(x => x.RequestId == requestId).ToListAsync();
        }
    }
}
