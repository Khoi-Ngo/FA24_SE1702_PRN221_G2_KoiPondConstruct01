using KoiPondConstruct.Data;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruction.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service
{
    public interface ICustomerRequestService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(long id);
        Task<ServiceResult> GetAllByCustomerName(string customerName);
        Task<ServiceResult> GetAllByDateRange(DateTime startDate, DateTime endDate);
        Task<ServiceResult> GetRequestDetailByRequestId(long id);
        Task<ServiceResult> Create();
        Task<ServiceResult> Delete(long id);
        Task<ServiceResult> UpdateRequest(UpdateCustomerRequestRequestDTO request);
        Task<ServiceResult> UpdateRequestDetail(UpdateCustomerRequestDetailRequestDTO request);
        Task<ServiceResult> DeleteRequestDetail(long id);
    }
    public class CustomerRequestService : ICustomerRequestService
    {
        private readonly UnitOfWork _unitOfWork;
        public CustomerRequestService(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public Task<ServiceResult> Create()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteRequestDetail(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetAll()
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

        public Task<ServiceResult> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> GetRequestDetailByRequestId(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> Update(UpdateCustomerRequestRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateRequest(UpdateCustomerRequestRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateRequestDetail(UpdateCustomerRequestDetailRequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
