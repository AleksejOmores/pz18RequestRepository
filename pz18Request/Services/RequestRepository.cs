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

        public async Task<Request> AddRequestAsync(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<List<DeviceModel>> GetDeviceModelsAsync()
        {
            return await _context.DeviceModels.ToListAsync();
        }

        public Task<List<Request>> GetRequestAsync()
        {
            return _context.Requests.ToListAsync();
        }

        public Task<Request> GetRequstByIdAsync(int requestId)
        {
            return _context.Requests.FirstOrDefaultAsync(x => x.RequestId == requestId);
        }

        public async Task<Request> UpdateRequestAsync(Request request)
        {
            var existingEntity = await _context.Requests.FindAsync(request.RequestId);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(request);
            }
            else
            {
                _context.Requests.Attach(request);
                _context.Entry(request).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return request;
        }
    }
}
