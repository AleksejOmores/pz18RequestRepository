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
        //вывод комментариев к заявкам
        Task<List<Comment>> GetCommentAsync();
        
        //вывод комментария определенной заявки
        Task<List<Comment>> GetCommentByRequestAsync(int requestId);
    }
}
