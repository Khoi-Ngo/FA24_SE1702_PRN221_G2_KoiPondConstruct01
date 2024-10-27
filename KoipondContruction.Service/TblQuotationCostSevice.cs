using KoipondContruction.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiPondConstruction.Data;
using KoiPondConstruction.Common;
using KoiPondConstruction.Data.Models;
using System.Drawing;
namespace KoipondContruction.Service
{
    public interface ITblQuotationCostSevice
    {
        Task<BusinessResult> GetAll();
        Task<BusinessResult> GetById(long Id);
        Task<BusinessResult> Save(BusinessResult Quotations);
        Task<BusinessResult> DeleteById(long Id);

        List<TblQuotationCost> GetQuotations();
    }

    public class TblQuotationCostSevice
    {
        private readonly UnitOfWork _unitOfWork;

        //public TblQuotationCostSevice() => _unitOfWork ??= new UnitOfWork();
        public TblQuotationCostSevice(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public TblQuotationCostSevice() {
        _unitOfWork ??= new UnitOfWork();
        }
        // Get all quotations

        public async Task<BusinessResult> GetAllIteam()
        {
            var quotations = await _unitOfWork.QuotationCostRepository.GetAllAsync();

            if (quotations == null || !quotations.Any())
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<TblQuotationCost>());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, quotations);
        }
        public async Task<BusinessResult> GetAll()
        {
            var quotations = await _unitOfWork.QuotationCostRepository.GetAllAsync(q => q.RequestDetail);

            if (quotations == null || !quotations.Any())
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<TblQuotationCost>());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, quotations);
        }

        // Get a quotation by ID
        public async Task<BusinessResult> GetById(long id)
        {
            if (id <= 0)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            var quotation = await _unitOfWork.QuotationCostRepository.GetByIdAsync(id);
            if (quotation == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, quotation);
        }

        // Save (Create or Update) a quotation
        public async Task<BusinessResult> Save(TblQuotationCost quotationCost)
        {
            try
            {
                if (quotationCost == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                var existingQuotation = await _unitOfWork.QuotationCostRepository.GetByIdAsync(quotationCost.Id);
                if (existingQuotation != null)
                {
                    // Update existing quotation
                    await _unitOfWork.QuotationCostRepository.UpdateAsync(quotationCost);
                }
                else
                {
                    // Add new quotation
                    await _unitOfWork.QuotationCostRepository.AddAsync(quotationCost);
                }

                await _unitOfWork.SaveAsync();

                return new BusinessResult(Const.SUCCESS_SAVE_CODE, Const.SUCCESS_SAVE_MSG, quotationCost);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        // Delete a quotation by ID
        public async Task<BusinessResult> DeleteById(long id)
        {
            try
            {
                var quotation = await _unitOfWork.QuotationCostRepository.GetByIdAsync(id);
                if (quotation == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                await _unitOfWork.QuotationCostRepository.DeleteAsync(quotation);
                await _unitOfWork.SaveAsync();

                return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        // Search a quotation by ID
        public async Task<BusinessResult> Search(string searchTerm)
        {
            var quotations = await _unitOfWork.QuotationCostRepository.SearchAsync(searchTerm);

            if (quotations == null || !quotations.Any())
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<TblQuotationCost>());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, quotations);
        }
        public List<TblQuotationCost> GetQuotations()
        {
            return _unitOfWork.QuotationCostRepository.GetAll().ToList(); 
        }
        public async Task<BusinessResult> SearchQuotations(string createdBy, string approvedBy, string contentText)
        {
            var quotations = await _unitOfWork.QuotationCostRepository.SearchAsync(createdBy, approvedBy, contentText);

            if (quotations == null || !quotations.Any())
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<TblQuotationCost>());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, quotations);
        }



    }
}

