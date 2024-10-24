using KoiPondConstruct.Common;
using KoiPondConstruct.Data;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruction.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service
{
    public interface IDesgin
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(long id);
        Task<ServiceResult> Save(TblDesign tblDesign);
        Task<ServiceResult> DeleteById(long id);
        Task<ServiceResult> GetDesginList();
        Task<ServiceResult> GetDesginRequestDetailById(long id);
        Task<ServiceResult> GetRequestDetailList();
        Task<ServiceResult> SearchDesignByCreate(string createby);
        List<TblDesign> GetDesignDetail();
        List<TblCustomerRequestDetail> GetCustomerRequestList();
        Task<ServiceResult> DeleteAsync(TblDesign tblDesign);



    }

    public class KoiPondConstructionService : IDesgin
    {
        private readonly UnitOfWork _unitOfWork;

        public KoiPondConstructionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public async Task<ServiceResult> DeleteById(long id)
        {
            try
            {

                var templateResult = await _unitOfWork.DesginRepository.GetByIdAsync(id);

                if (templateResult == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new TblSampleDesign());

                }
                else
                {
                    var result = await _unitOfWork.DesginRepository.RemoveAsync(templateResult);
                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, templateResult);

                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, templateResult);

                    }


                }


            }
            catch (Exception ex)
            {


                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());



            }
        }

        public async Task<ServiceResult> GetAll()
        {

            //Business Rule
            //

            var templateDesign = await _unitOfWork.DesginRepository.GetAllAsync();

            if (templateDesign == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);

            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, templateDesign);


            }

            //return null;
        }

        public async Task<ServiceResult> GetDesginList()
        {
            var templateDesign = await _unitOfWork.DesginRepository.GetAlLDesignsAsync();

            if (templateDesign == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);

            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, templateDesign);


            }

        }

        public async Task<ServiceResult> GetById(long id)
        {

            var templateDesign = await _unitOfWork.DesginRepository.GetByIdAsync(id);
            if (templateDesign == null)
            {

                //return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,new TblSampleDesign());
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);


            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, templateDesign);


            }
        }

        public async Task<ServiceResult> Save(TblDesign tblDesign)
        {
            try
            {
                long result = -1;
                var templateTemp = _unitOfWork.DesginRepository.GetById(tblDesign.Id);
                if (templateTemp != null)
                {
                    result = await _unitOfWork.DesginRepository.UpdateAsync(tblDesign);
                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, tblDesign);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }

                else
                {
                    result = await _unitOfWork.DesginRepository.CreateAsync(tblDesign);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, tblDesign);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, tblDesign);
                    }
                }

            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());

            }
        }

        public async Task<ServiceResult> GetDesginRequestDetailById(long id)
        {
            #region Business Rule
            var templateTemp = await _unitOfWork.CustomerRequestDetailRepository.GetByIdWithDesignAsync(id);
            if (templateTemp == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, templateTemp);


            }

            #endregion 
        }

        public async Task<ServiceResult> GetRequestDetailList()
        {

            var requestDetails = await _unitOfWork.CustomerRequestDetailRepository.GetAllAsync();

            if (requestDetails == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);

            }
            else
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, requestDetails);


            }
        }
        public List<TblDesign> GetDesignDetail()
        {

            return _unitOfWork.DesginRepository.GetAll();
        }

        public List<TblCustomerRequestDetail> GetCustomerRequestList()
        {




            return _unitOfWork.CustomerRequestDetailRepository.GetAll();

        }

        public async Task<ServiceResult> SearchDesignByCreate(string search)
        {
            var design = await _unitOfWork.DesginRepository.GetAlLDesignsAsync();
            var result = design.Where(q => string.IsNullOrEmpty(search) || q.CreatedBy.ToLower().Contains(search) || q.ApprovedBy.ToLower().Contains(search) || q.Status.ToLower().Contains(search))
               .ToList();

            if (result == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new TblDesign());
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
        }

        public async Task<ServiceResult> DeleteAsync(TblDesign tblDesign)
        {
            try
            {


                if (tblDesign == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new TblSampleDesign());

                }
                else
                {
                    var result = await _unitOfWork.DesginRepository.RemoveAsync(tblDesign);
                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, tblDesign);

                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, tblDesign);

                    }


                }
            }
            catch (Exception ex)
            {


                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());



            }
        }
    }
}
