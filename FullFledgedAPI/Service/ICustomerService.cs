using FullFledgedAPI.Helper;
using FullFledgedAPI.Modal;
using FullFledgedAPI.Repos.Models;

namespace FullFledgedAPI.Service
{
    public interface ICustomerService
    {
        //List<TblCustomer> GetAll();

        Task<List<Customermodal>> GetAll();  //Async Task
        Task<Customermodal> GetByCode(int code);  //Async Task
        Task<APIResponse> Remove(int code);  //Async Task
        Task<APIResponse> Create(Customermodal data);  //Async Task
        Task<APIResponse> Update(Customermodal data,int code);  //Async Task
    }
}
