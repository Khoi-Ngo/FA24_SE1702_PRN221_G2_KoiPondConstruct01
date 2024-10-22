using AutoMapper;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region list of customer  request with some customer information
            CreateMap<TblCustomerRequest, CustomerRequestListDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.AvatarImg, opt => opt.MapFrom(src => src.User.AvatarImg))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));


            #endregion

            //#region detail of customer request
            //// Map TblCustomerRequest to CustomerRequestDetailDTO
            //CreateMap<TblCustomerRequest, CustomerRequestDetailDTO>()
            //    .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.User.FirstName))
            //    .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.User.LastName))
            //    .ForMember(dest => dest.CustomerUserName, opt => opt.MapFrom(src => src.User.Username))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            //    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            //    // Map other fields directly
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            //    .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            //    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            //    .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            //    .ForMember(dest => dest.customerRequestDetailListDTOs, opt => opt.MapFrom(src => src.TblCustomerRequestDetails));

            //#endregion


            // Map TblCustomerRequest to CustomerRequestDetailDTO
            CreateMap<TblCustomerRequest, CustomerRequestDetailDTO>()
                .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.CustomerUserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                // Map other fields directly
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.customerRequestDetailListDTOs, opt => opt.MapFrom(src => src.TblCustomerRequestDetails));

            // Map TblCustomerRequestDetail to CustomerRequestDetailListDTO
            CreateMap<TblCustomerRequestDetail, CustomerRequestDetailListDTO>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.SampleDesignId, opt => opt.MapFrom(src => src.SampleDesignId))
                .ForMember(dest => dest.HomeownerFirstName, opt => opt.MapFrom(src => src.HomeownerFirstName))
                .ForMember(dest => dest.HomeownerLastName, opt => opt.MapFrom(src => src.HomeownerLastName))
                .ForMember(dest => dest.HomeownerPhone, opt => opt.MapFrom(src => src.HomeownerPhone))
                .ForMember(dest => dest.HomeownerDateOfBirth, opt => opt.MapFrom(src => src.HomeownerDateOfBirth))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Shape, opt => opt.MapFrom(src => src.Shape))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));


            #region GetCustomerRequestUpdateDTO Mapping
            CreateMap<TblCustomerRequest, GetCustomerRequestUpdateDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => src.UpdatedTime))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
            #endregion

            #region CustomerRequestDetail Mapping
            CreateMap<CustomerRequestDetailListDTO, TblCustomerRequestDetail>()
                .ForMember(dest => dest.HomeownerFirstName, opt => opt.MapFrom(src => src.HomeownerFirstName))
                .ForMember(dest => dest.HomeownerLastName, opt => opt.MapFrom(src => src.HomeownerLastName))
                .ForMember(dest => dest.HomeownerPhone, opt => opt.MapFrom(src => src.HomeownerPhone))
                .ForMember(dest => dest.HomeownerDateOfBirth, opt => opt.MapFrom(src => src.HomeownerDateOfBirth))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Shape, opt => opt.MapFrom(src => src.Shape))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
            #endregion



            #region CustomerRequestDetail Mapping
            CreateMap<TblCustomerRequestDetail, GetCustRequestDetailListDTOResponse>() // Correctly mapping from TblCustomerRequestDetail to GetCustRequestDetailListDTOResponse
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Request.UserId)) // Mapping UserId from TblCustomerRequest
                .ForMember(dest => dest.SampleDesignId, opt => opt.MapFrom(src => src.SampleDesignId))
                .ForMember(dest => dest.HomeownerFirstName, opt => opt.MapFrom(src => src.HomeownerFirstName))
                .ForMember(dest => dest.HomeownerLastName, opt => opt.MapFrom(src => src.HomeownerLastName))
                .ForMember(dest => dest.HomeownerPhone, opt => opt.MapFrom(src => src.HomeownerPhone))
                .ForMember(dest => dest.HomeownerDateOfBirth, opt => opt.MapFrom(src => src.HomeownerDateOfBirth))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Shape, opt => opt.MapFrom(src => src.Shape))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
            #endregion


        }
    }
}
