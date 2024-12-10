using pz18Request.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz18Request.Services
{
    public interface IRequestRepository
    {
        // вывод всех заявок
        Task<List<Request>> GetRequestAsync();

        // вывод заявок по Id
        Task<Request> GetRequstByIdAsync(int requestId);

        //обновить заявку
        Task<Request> UpdateRequestAsync(Request request);

        //добавить заявку
        Task<Request> AddRequestAsync(Request request);

        //получение модели по ее Id
        Task<List<DeviceModel>> GetDeviceModelsAsync();
    }
}
