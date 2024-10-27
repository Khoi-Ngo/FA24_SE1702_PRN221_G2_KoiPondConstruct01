using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Repository;

public class UnitOfWork : IDisposable
{
    private FA24_SE1702_PRN221_G2_KoiPondConstructionContext _unitOfWorkContext;
    private QuotationCostRepository? _quotationCostRepository;

    public UnitOfWork()
    {
        _unitOfWorkContext ??= new FA24_SE1702_PRN221_G2_KoiPondConstructionContext(); // Initialize context
    }

    public QuotationCostRepository QuotationCostRepository
    {
        get
        {
            return _quotationCostRepository ??= new QuotationCostRepository(_unitOfWorkContext);
        }
    }

    public async Task<long> SaveAsync()
    {
        return await _unitOfWorkContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _unitOfWorkContext.Dispose();
    }
}
