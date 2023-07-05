using AutoMapper;
using FullFledgedAPI.Modal;
using FullFledgedAPI.Repos.Models;

namespace FullFledgedAPI.Helper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<TblCustomer, Customermodal>().ForMember(item => item.Statusname, opt => opt.MapFrom(item => (item.IsActive !=null && item.IsActive.Value) ? "Active" : "In active")).ReverseMap();
        }
    }
}
