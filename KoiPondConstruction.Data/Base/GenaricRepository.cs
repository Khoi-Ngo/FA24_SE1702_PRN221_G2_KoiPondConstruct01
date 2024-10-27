using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KoiPondConstruction.Data.Base
{
    public class GenericRepository<T> where T : class
    {
        protected FA24_SE1702_PRN221_G2_KoiPondConstructionContext _context;


        public GenericRepository(FA24_SE1702_PRN221_G2_KoiPondConstructionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Existing methods

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }


        public async Task<long> CreateAsync(T entity)
        {
            PrepareCreate(entity);
            return await SaveAsync();
        }

        public async Task<long> UpdateAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            PrepareRemove(entity);
            await SaveAsync();
        }

        public T? GetById(object id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity; 
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }


        #region Separating assign entity and save operators

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public long Save()
        {
            return _context.SaveChanges();
        }

        public async Task<long> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //public async Task AddAsync(TblQuotationCost entity)
        //{
        //    await _context.Set<TblQuotationCost>().AddAsync(entity);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(TblQuotationCost entity)
        //{
        //    _context.Set<TblQuotationCost>().Remove(entity);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task<List<TblQuotationCost>> SearchAsync(string searchTerm)
        //{
        //    return await _context.TblQuotationCosts
        //        .Where(q => q.Id.ToString().Contains(searchTerm) || q.ContentText.Contains(searchTerm)) 
        //        .ToListAsync();
        //}
        public async Task<List<TblQuotationCost>> SearchAsync(string createBy, string approvedBy, string contentText)
        {
            // Giả sử bạn đang sử dụng Entity Framework
            return await _context.TblQuotationCosts
                .Where(q =>
                    (string.IsNullOrEmpty(createBy) || q.CreatedBy.Contains(createBy)) &&
                    (string.IsNullOrEmpty(approvedBy) || q.ApprovedBy.Contains(approvedBy)) &&
                    (string.IsNullOrEmpty(contentText) || q.ContentText.Contains(contentText)))
                .ToListAsync();
        }
        #endregion
    }
}
