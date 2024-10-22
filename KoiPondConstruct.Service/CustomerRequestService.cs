using AutoMapper;
using KoiPondConstruct.Common;
using KoiPondConstruct.Data;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Data.Repository.Impl;
using KoiPondConstruct.Service.DTOs;
using KoiPondConstruction.Service.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service
{
    public interface ICustomerRequestService
    {
        Task<ServiceResult> GetCustomerRequestsAsync();
        Task<ServiceResult> GetRequestDetailByIdAsync(long requestID);
        Task<ServiceResult> GetRequestDetailDetailByIdAsync(long? id);
        Task<ServiceResult> DeleteByIdAsync(long? id);
        Task<ServiceResult> GetCustomerUpdateResponseByIdAsync(long? id);
        Task<ServiceResult> UpdateCustomerRequestAsync(GetCustomerRequestUpdateDTO getCustomerRequestUpdateDTO);
        Task<ServiceResult> CreateCustomerRequestAsync(CustomerRequestDetailDTO customerRequestDetailDTO, int userid);
        Task<ServiceResult> GetAllCustomerRequestDetailsAsync();
        Task<ServiceResult> CreateCustomRequestDetailAsync(TblCustomerRequestDetail tblCustomerRequestDetail);
        Task<ServiceResult> DeleteCustomerRequestDetailByIdAsync(long? id);
        Task<ServiceResult> UpdateCustomerDetailAsync(TblCustomerRequestDetail tblCustomerRequestDetail);
    }
    public class CustomerRequestService : ICustomerRequestService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomerRequestRepository _customerRequestRepository;
        private readonly CustomerRequestDetailRepository _customerRequestDetailRepository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerRequestService(UnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _customerRequestRepository = _unitOfWork.CustomerRequestRepository;
            _customerRequestDetailRepository = _unitOfWork.CustomerRequestDetailRepository;
            _userRepository = _unitOfWork.UserRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<ServiceResult> CreateCustomerRequestAsync(CustomerRequestDetailDTO customerRequestDetailDTO, int userid)
        //{
        //    try
        //    {
        //        TblUser user = await _userRepository.GetByIdAsync((long)userid);
        //        if (user == null)
        //        {
        //            return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
        //        }

        //        customerRequestDetailDTO.CustomerFirstName = user.FirstName;
        //        customerRequestDetailDTO.CustomerLastName = user.LastName;
        //        customerRequestDetailDTO.PhoneNumber = user.PhoneNumber;
        //        customerRequestDetailDTO.Address = user.Address;
        //        customerRequestDetailDTO.Email = user.Email;
        //        customerRequestDetailDTO.Status = "NEW";

        //        var customerRequest = new TblCustomerRequest
        //        {
        //            UserId = userid,
        //            Description = customerRequestDetailDTO.Description,
        //            Priority = customerRequestDetailDTO.Priority,
        //            CreatedTime = DateTime.Now,
        //            StartDate = DateOnly.FromDateTime(DateTime.Now),
        //            EndDate = customerRequestDetailDTO.EndDate,
        //            Status = customerRequestDetailDTO.Status,
        //            IsDeleted = false,
        //            TblCustomerRequestDetails = new List<TblCustomerRequestDetail>(),
        //            UpdatedTime = DateTime.Now
        //        };

        //        foreach (var item in customerRequestDetailDTO.customerRequestDetailListDTOs)
        //        {
        //            var customerRequestDetail = _mapper.Map<TblCustomerRequestDetail>(item);
        //            customerRequest.TblCustomerRequestDetails.Add(customerRequestDetail);
        //        }

        //        await _customerRequestRepository.CreateAsync(customerRequest);
        //        await _unitOfWork.SaveChangesAsync();

        //        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, ex);
        //    }
        //}

        public async Task<ServiceResult> CreateCustomerRequestAsync(CustomerRequestDetailDTO customerRequestDetailDTO, int userid)
        {

            try
            {
                TblUser user = await _userRepository.GetByIdAsync((long)userid);
                if (user == null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }

                // Set the customer details
                customerRequestDetailDTO.CustomerFirstName = user.FirstName;
                customerRequestDetailDTO.CustomerLastName = user.LastName;
                customerRequestDetailDTO.PhoneNumber = user.PhoneNumber;
                customerRequestDetailDTO.Address = user.Address;
                customerRequestDetailDTO.Email = user.Email;
                customerRequestDetailDTO.Status = "NEW";

                // Create the customer request
                var customerRequest = new TblCustomerRequest
                {
                    UserId = userid,
                    Description = customerRequestDetailDTO.Description,
                    Priority = customerRequestDetailDTO.Priority,
                    CreatedTime = DateTime.Now,
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = customerRequestDetailDTO.EndDate,
                    Status = customerRequestDetailDTO.Status,
                    IsDeleted = false,
                    UpdatedTime = DateTime.Now
                };

                // Create customer request details
                foreach (var item in customerRequestDetailDTO.customerRequestDetailListDTOs)
                {
                    var customerRequestDetail = _mapper.Map<TblCustomerRequestDetail>(item);
                    customerRequestDetail.RequestId = customerRequest.Id; // Set foreign key reference
                    customerRequest.TblCustomerRequestDetails.Add(customerRequestDetail);
                }

                // Add customer request to the repository
                await _customerRequestRepository.CreateAsync(customerRequest);

                // Save changes within the transaction

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, ex);
            }
        }


        public async Task<ServiceResult> CreateCustomRequestDetailAsync(TblCustomerRequestDetail tblCustomerRequestDetail)
        {
            try
            {
                tblCustomerRequestDetail.SampleDesignId = 1; //temp as default
                await _customerRequestDetailRepository.CreateAsync(tblCustomerRequestDetail);
                return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteByIdAsync(long? id)
        {
            try
            {
                TblCustomerRequest tblCustomerRequest = await _customerRequestRepository.GetByIdAsync(id);
                List<TblCustomerRequestDetail> tblCustomerRequestDetails = await _customerRequestDetailRepository.GetAllByCuReqIdAsync(id);
                foreach (var detail in tblCustomerRequestDetails)
                {
                    await _customerRequestDetailRepository.RemoveAsync(detail);
                }
                await _customerRequestRepository.RemoveAsync(tblCustomerRequest);
                return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);

            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, ex.Message);

            }
        }

        public async Task<ServiceResult> DeleteCustomerRequestDetailByIdAsync(long? id)
        {
            try
            {
                TblCustomerRequestDetail tblCustomerRequestDetail = await _customerRequestDetailRepository.GetByIdAsync((long)id);

                await _customerRequestDetailRepository.RemoveAsync(tblCustomerRequestDetail);
                return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);

            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
            }
        }

        public async Task<ServiceResult> GetAllCustomerRequestDetailsAsync()
        {
            try
            {
                List<TblCustomerRequestDetail> tblCustomerRequestDetails = await _customerRequestDetailRepository.GetAllWithMainReuqestAsync();
                List<GetCustRequestDetailListDTOResponse> responses = _mapper.Map<List<GetCustRequestDetailListDTOResponse>>(tblCustomerRequestDetails);
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, responses);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, ex.Message);
            }
        }

        public async Task<List<string>> GetAllCustomerRequestStatusUniqueAsync()
        {
            try
            {
                return await _customerRequestRepository.GetAllStatusUniqueAsync();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public async Task<ServiceResult> GetCustomerRequestsAsync()
        {
            List<CustomerRequestListDTO> customerRequestListDTOs = new List<CustomerRequestListDTO>();
            try
            {
                customerRequestListDTOs =
              _mapper.Map<List<CustomerRequestListDTO>>(
                  await _customerRequestRepository.GetCustomerRequestsWithUserAsync()
                  );
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customerRequestListDTOs);

            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, ex.Message, customerRequestListDTOs);
            }

        }

        public async Task<ServiceResult> GetCustomerUpdateResponseByIdAsync(long? id)
        {
            try
            {
                GetCustomerRequestUpdateDTO dto = _mapper.Map<GetCustomerRequestUpdateDTO>(await _customerRequestRepository.GetByIdAsync(id));
                if (dto == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                }
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, dto);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }

        }

        public async Task<ServiceResult> GetRequestDetailByIdAsync(long requestID)
        {
            try
            {
                //call repo to get by id including all detail
                TblCustomerRequest tblCustomerRequest = await _customerRequestRepository.GetCustomerRequestDetailByIdAsync(requestID);
                //mapper with response
                CustomerRequestDetailDTO dto = _mapper.Map<CustomerRequestDetailDTO>(tblCustomerRequest);
                dto.Address = tblCustomerRequest.User.Address;

                //mapping customer information
                dto.CustomerFirstName = tblCustomerRequest.User.FirstName;
                dto.CustomerLastName = tblCustomerRequest.User.LastName;
                dto.CustomerUserName = tblCustomerRequest.User.Username;
                dto.Email = tblCustomerRequest.User.Email;


                dto.customerRequestDetailListDTOs = _mapper.Map<List<CustomerRequestDetailListDTO>>(tblCustomerRequest.TblCustomerRequestDetails);
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, dto);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, ex.Message);
            }
        }

        public async Task<ServiceResult> GetRequestDetailDetailByIdAsync(long? id)
        {
            try
            {
                TblCustomerRequestDetail tblCustomerRequestDetail = await _customerRequestDetailRepository.GetByIdAsync((long)id);
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, tblCustomerRequestDetail);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
        }

        public async Task<ServiceResult> UpdateCustomerDetailAsync(TblCustomerRequestDetail tblCustomerRequestDetail)
        {
            try
            {
                await _customerRequestDetailRepository.UpdateAsync(tblCustomerRequestDetail);
                return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
        }

        public async Task<ServiceResult> UpdateCustomerRequestAsync(GetCustomerRequestUpdateDTO getCustomerRequestUpdateDTO)
        {
            try
            {
                TblCustomerRequest tblCustomerRequest = await _customerRequestRepository.GetByIdAsync(getCustomerRequestUpdateDTO.Id);
                tblCustomerRequest.Status = getCustomerRequestUpdateDTO.Status;
                tblCustomerRequest.Priority = getCustomerRequestUpdateDTO.Priority;
                tblCustomerRequest.CreatedTime = getCustomerRequestUpdateDTO.CreatedTime;
                tblCustomerRequest.UpdatedTime = getCustomerRequestUpdateDTO.UpdatedTime;
                tblCustomerRequest.StartDate = getCustomerRequestUpdateDTO.StartDate;
                tblCustomerRequest.EndDate = getCustomerRequestUpdateDTO.EndDate;
                tblCustomerRequest.Description = getCustomerRequestUpdateDTO.Description;
                tblCustomerRequest.IsDeleted = getCustomerRequestUpdateDTO.IsDeleted;
                await _customerRequestRepository.UpdateAsync(tblCustomerRequest);
                return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, _mapper.Map<GetCustomerRequestUpdateDTO>(tblCustomerRequest));
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
        }
    }
}
