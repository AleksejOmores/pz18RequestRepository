using Microsoft.EntityFrameworkCore;
using pz18Request.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request.Services
{
    public class RequestRepository : IRequestRepository
    {
        readonly RegApplicationContext _context = new RegApplicationContext();
        public Task<List<Request>> GetRequestAsync()
        {
            return _context.Requests.ToListAsync();
        }

        public Task<Request> GetRequstByIdAsync(int requestId)
        {
            return _context.Requests.FirstOrDefaultAsync(x => x.RequestId == requestId);
        }
    }
}
