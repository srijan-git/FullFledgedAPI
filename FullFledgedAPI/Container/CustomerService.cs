using AutoMapper;
using Azure;
using FullFledgedAPI.Helper;
using FullFledgedAPI.Modal;
using FullFledgedAPI.Repos;
using FullFledgedAPI.Repos.Models;
using FullFledgedAPI.Service;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FullFledgedAPI.Container
{
    public class CustomerService : ICustomerService
    {
        private readonly FullFledgedAPIContext _context;
        private readonly IMapper _mapper;

        public CustomerService(FullFledgedAPIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<APIResponse> Create(Customermodal data)
        {
            APIResponse _response = new APIResponse();
            try
            {
                TblCustomer _customer = _mapper.Map<Customermodal, TblCustomer>(data);
                await _context.TblCustomers.AddAsync(_customer);
                await _context.SaveChangesAsync();
                _response.ResponseCode = 201;
                _response.Result = data.Code.ToString();
            }
            catch (Exception ex)
            {
                _response.ResponseCode = 400;
                _response.ErrorMessage = ex.Message;
            }
            return _response;
        }

        public async Task<List<Customermodal>> GetAll()
        {
            List<Customermodal> _response = new List<Customermodal>();
            var _data = await _context.TblCustomers.ToListAsync();
            if (_data != null)
            {
                _response = _mapper.Map<List<TblCustomer>, List<Customermodal>>(_data);
            }
            return _response;
        }

        public async Task<Customermodal> GetByCode(int code)
        {
            Customermodal _response = new Customermodal();
            var _data = await _context.TblCustomers.FindAsync(code);
            if (_data != null)
            {
                _response = _mapper.Map<TblCustomer, Customermodal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> Remove(int code)
        {
            APIResponse _response = new APIResponse();
            try
            {
                var _customer = await _context.TblCustomers.FindAsync(code);
                if (_customer != null)
                {
                    _context.TblCustomers.Remove(_customer);
                    await _context.SaveChangesAsync();
                    _response.ResponseCode = 201;
                    _response.Result = code.ToString();
                }
                else
                {
                    _response.ResponseCode = 404;
                    _response.ErrorMessage = "Data Not Found";
                }

            }
            catch (Exception ex)
            {
                _response.ResponseCode = 400;
                _response.ErrorMessage = ex.Message;
            }
            return _response;
        }

        public async Task<APIResponse> Update(Customermodal data, int code)
        {
            APIResponse _response = new APIResponse();
            try
            {
                var _customer = await _context.TblCustomers.FindAsync(code);
                if (_customer != null)
                {
                    _customer.Name = data.Name;
                    _customer.Email = data.Email;
                    _customer.Phone = data.Phone;
                    _customer.IsActive = data.IsActive;
                    _customer.CreditLimit= data.CreditLimit;

                    await _context.SaveChangesAsync();
                    _response.ResponseCode = 201;
                    _response.Result = code.ToString();
                }
                else
                {
                    _response.ResponseCode = 404;
                    _response.ErrorMessage = "Data Not Found";
                }

            }
            catch (Exception ex)
            {
                _response.ResponseCode = 400;
                _response.ErrorMessage = ex.Message;
            }
            return _response;
        }
    }
}
