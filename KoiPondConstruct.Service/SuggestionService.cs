
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruction.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service
{
    public interface ISuggestionService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(long id);
        Task<ServiceResult> GetAllByCustomerName(string customerName);
        Task<ServiceResult> GetAllByDateRange(DateTime startDate, DateTime endDate);
        Task<ServiceResult> GetAllByCustomerId(long customerId);
        Task<ServiceResult> GetAllByRequestDetailId(long requestDetailId);
        Task<ServiceResult> Create(); //for per request detail
        Task<ServiceResult> Delete(long id);
        Task<ServiceResult> Update(UpdateCustomerRequestRequestDTO request);
    }
    public class SuggestionService : ISuggestionService
    {
        public Task<ServiceResult> Create()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetAllByCustomerId(long customerId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetAllByCustomerName(string customerName)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetAllByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetAllByRequestDetailId(long requestDetailId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> Update(UpdateCustomerRequestRequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
