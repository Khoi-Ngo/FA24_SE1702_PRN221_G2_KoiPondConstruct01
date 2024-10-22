using KoiPondConstruct.Data;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Data.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service
{
    public class DataInitService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserRepository _userRepository;
        private readonly CustomerRequestDetailRepository _customerRequestDetailRepository;
        private readonly CustomerRequestRepository _customerRequestRepository;
        private readonly SuggestionRepository _suggestionRepository;

        public DataInitService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.UserRepository;
            _customerRequestDetailRepository = _unitOfWork.CustomerRequestDetailRepository;
            _customerRequestRepository = _unitOfWork.CustomerRequestRepository;
            _suggestionRepository = _unitOfWork.SuggestionRepository;
        }

        public async Task InitializeDataAsync()
        {
            // Check and insert default users
            if (!_userRepository.GetAll().Any())
            {
                await SeedUsers();
            }

            // Check and insert customer requests
            if (!_customerRequestRepository.GetAll().Any())
            {
                await SeedCustomerRequests();
            }

            // Check and insert customer request details
            if (!_customerRequestDetailRepository.GetAll().Any())
            {
                await SeedCustomerRequestDetails();
            }

            // Check and insert suggestions
            if (!_suggestionRepository.GetAll().Any())
            {
                await SeedSuggestions();
            }
        }

        private async Task SeedUsers()
        {
            var users = new List<TblUser>
            {
                new TblUser
                {
                    Username = "admin",
                    FirstName = "System",
                    LastName = "Administrator",
                    Email = "admin@koipond.com",
                    PhoneNumber = "123456789",
                    Address = "123 Admin St.",
                    Role = "Admin",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    IsActive = true,
                    Password = HashPassword("Admin123!")
                },
                new TblUser
                {
                    Username = "john_doe",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "987654321",
                    Address = "456 User Blvd.",
                    Role = "Customer",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    IsActive = true,
                    Password = HashPassword("User123!")
                },
                // Add more users as needed
            };

            foreach (var user in users)
            {
                await _userRepository.CreateAsync(user);
            }
        }

        private async Task SeedCustomerRequests()
        {
            var requests = new List<TblCustomerRequest>
            {
                new TblCustomerRequest
                {
                    UserId = 1, // Link to a user (e.g., admin)
                    Description = "Request for koi pond installation.",
                    Priority = "High",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                    Status = "Pending",
                    IsDeleted = false
                },
                new TblCustomerRequest
                {
                    UserId = 2, // Link to a different user (e.g., John Doe)
                    Description = "Request for pond maintenance.",
                    Priority = "Low",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14)),
                    Status = "In Progress",
                    IsDeleted = false
                },
                // Add more customer requests as needed
            };

            foreach (var request in requests)
            {
                await _customerRequestRepository.CreateAsync(request);
            }
        }

        private async Task SeedCustomerRequestDetails()
        {
            var requestDetails = new List<TblCustomerRequestDetail>
            {
                new TblCustomerRequestDetail
                {
                    RequestId = 1, // Link to a customer request
                    SampleDesignId = 101,
                    HomeownerFirstName = "John",
                    HomeownerLastName = "Doe",
                    HomeownerPhone = "123456789",
                    HomeownerDateOfBirth = DateOnly.FromDateTime(new DateTime(1980, 5, 20)),
                    Height = 150,
                    Width = 300,
                    Length = 400,
                    Shape = "Oval",
                    Budget = 5000,
                    Type = "Koi Pond",
                    Address = "456 User Blvd.",
                    Note = "Add extra filtration.",
                    IsDeleted = false
                },
                new TblCustomerRequestDetail
                {
                    RequestId = 2, // Link to a different customer request
                    SampleDesignId = 102,
                    HomeownerFirstName = "Jane",
                    HomeownerLastName = "Smith",
                    HomeownerPhone = "987654321",
                    HomeownerDateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 8, 15)),
                    Height = 100,
                    Width = 200,
                    Length = 300,
                    Shape = "Rectangle",
                    Budget = 3500,
                    Type = "Maintenance",
                    Address = "789 Maintenance Ave.",
                    Note = "Regular cleaning and pump check.",
                    IsDeleted = false
                },
                // Add more customer request details as needed
            };

            foreach (var detail in requestDetails)
            {
                await _customerRequestDetailRepository.CreateAsync(detail);
            }
        }

        private async Task SeedSuggestions()
        {
            var suggestions = new List<TblSuggestionDoc>
            {
                new TblSuggestionDoc
                {
                    RequestDetailId = 1, // Link to a customer request detail
                    SampleDesignId = 101,
                    File = "suggestion1.pdf",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    Status = "Approved",
                    Note = "First suggestion approved.",
                    IsFirstItem = true,
                    ApprovedBy = "admin",
                    ApprovedTiem = DateTime.Now,
                    CreatedBy = "admin",
                    ContentText = "Install a high-efficiency filtration system.",
                    IsDeleted = false
                },
                new TblSuggestionDoc
                {
                    RequestDetailId = 2, // Link to a different customer request detail
                    SampleDesignId = 102,
                    File = "suggestion2.pdf",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    Status = "Pending",
                    Note = "Awaiting homeowner review.",
                    IsFirstItem = false,
                    ApprovedBy = null,
                    ApprovedTiem = default(DateTime),
                    CreatedBy = "user1",
                    ContentText = "Regular cleaning of the pond and replacing the pump filter.",
                    IsDeleted = false
                },
                // Add more suggestions as needed
            };

            foreach (var suggestion in suggestions)
            {
                await _suggestionRepository.CreateAsync(suggestion);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert the hash byte array back to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2")); // Convert byte to hex
                }
                return builder.ToString();
            }
        }
    }
}
