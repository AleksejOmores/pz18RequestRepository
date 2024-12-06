using pz18Request.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request.Services
{
    internal interface IRequestRepository
    {
        // вывод всех заявок
        Task<List<Request>> GetRequestAsync();

        // вывод заявок по Id
        Task<Request> GetRequstByIdAsync(int requestId);


    }
}
