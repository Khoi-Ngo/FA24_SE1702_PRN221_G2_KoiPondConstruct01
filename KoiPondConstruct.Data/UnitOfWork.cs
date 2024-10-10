using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Data.Repository.Impl;


namespace KoiPondConstruct.Data
{
    public class UnitOfWork
    {
        private FA24_SE1702_PRN221_G2_KoiPondConstructContext _context;
        private UserRepository _userRepository;
        private CustomerRequestDetailRepository _customerRequestDetailRepository;
        private CustomerRequestRepository _customerRequestRepository;
        private SuggestionRepository _suggestionRepository;

        public UnitOfWork()
        {
            _context ??= new FA24_SE1702_PRN221_G2_KoiPondConstructContext();// ??= meaning created new instance only if null

        }

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);

            }
        }
        public CustomerRequestRepository CustomerRequestRepository
        {
            get
            {
                return _customerRequestRepository ??= new CustomerRequestRepository(_context);
            }
        }

        public CustomerRequestDetailRepository CustomerRequestDetailRepository
        {
            get
            {
                return _customerRequestDetailRepository ??= new CustomerRequestDetailRepository(_context);
            }
        }

        public SuggestionRepository SuggestionRepository
        {
            get
            {
                return _suggestionRepository ??= new SuggestionRepository(_context);
            }
        }
    }
}
