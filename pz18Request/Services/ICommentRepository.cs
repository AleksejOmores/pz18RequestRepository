using pz18Request.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request.Services
{
    internal interface ICommentRepository
    {
        Task<List<Comment>> GetCommentAsync();
    }
}
