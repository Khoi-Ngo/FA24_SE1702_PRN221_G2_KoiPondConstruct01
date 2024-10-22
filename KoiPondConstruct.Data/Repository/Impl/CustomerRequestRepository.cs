using KoiPondConstruct.Data.Base;
using KoiPondConstruct.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Data.Repository.Impl
{
    public class CustomerRequestRepository : BaseRepository<TblCustomerRequest>
    {
        public CustomerRequestRepository() { }

        public CustomerRequestRepository(FA24_SE1702_PRN221_G2_KoiPondConstructContext context) : base(context)
        {
            _context = context;
        }
        // Method to retrieve customer requests including related user data
        public async Task<List<TblCustomerRequest>> GetCustomerRequestsWithUserAsync()
        {
            return await _context.TblCustomerRequests
                .Include(cr => cr.User) // Eager load TblUser related to the request
                .Where(cr => !cr.IsDeleted) // Example filter to exclude deleted records
                .ToListAsync();
        }
        public async Task<TblCustomerRequest> GetCustomerRequestDetailByIdAsync(long id)
        {
            return await _context.TblCustomerRequests
                .Include(cr => cr.User) // Eager load the related User entity
                .Include(cr => cr.TblCustomerRequestDetails)
                .Where(cr => !cr.IsDeleted && cr.Id == id) // Ensure the request is not deleted and matches the given ID
                .FirstOrDefaultAsync(); // Retrieve the first matching request or null if none found
        }

        public async Task<List<string>> GetAllStatusUniqueAsync()
        {
            return await _context.TblCustomerRequests
         .Where(r => !r.IsDeleted) // Optional: Exclude deleted records
         .Select(r => r.Status) // Select the Status field
         .Distinct() // Get distinct values
         .ToListAsync(); // Execute the query and return the results as a list
        }
    }
}
