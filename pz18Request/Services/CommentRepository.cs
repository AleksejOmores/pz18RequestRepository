using Microsoft.EntityFrameworkCore;
using pz18Request.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pz18Request.Services
{
    public class CommentRepository : ICommentRepository
    {
        readonly RegApplicationContext _context = new RegApplicationContext();
        public Task<List<Comment>> GetCommentAsync()
        {
            return _context.Comments.ToListAsync();
        }

        public Task<Comment> GetCommentByIdAsync(int customerId)
        {
            return _context.Comments.FirstOrDefaultAsync(c => c.RequestId == customerId);
        }

        public async Task<List<Comment>> GetCommentByRequestAsync(int requestId)
        {
            return await _context.Comments
                .Where(c => c.RequestId == requestId)
                .ToListAsync();
        }
    }
}
