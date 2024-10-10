using KoiPondConstruct.Data;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Data.Repository.Impl;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service
{
    public class DataInitService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserRepository _userRepository;
        private readonly IPasswordHasher<TblUser> _passwordHasher;

        public DataInitService(UnitOfWork unitOfWork, UserRepository userRepository, IPasswordHasher<TblUser> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Init()
        {
            // Check if there are any users in the database
            var users = await _userRepository.GetAllAsync();

            if (users == null || !users.Any())
            {
                // Create default users: 2 as CUSTOMER and 2 as STAFF
                var defaultUsers = new List<TblUser>
                {
                    new TblUser
                    {
                        Username = "customer1",
                        Email = "customer1@example.com",
                        FirstName = "John",
                        LastName = "Doe",
                        Role = "CUSTOMER",
                        CreatedTime = DateTime.UtcNow,
                        UpdatedTime = DateTime.UtcNow,
                        IsActive = true,
                        Status = true
                    },
                    new TblUser
                    {
                        Username = "customer2",
                        Email = "customer2@example.com",
                        FirstName = "Jane",
                        LastName = "Doe",
                        Role = "CUSTOMER",
                        CreatedTime = DateTime.UtcNow,
                        UpdatedTime = DateTime.UtcNow,
                        IsActive = true,
                        Status = true
                    },
                    new TblUser
                    {
                        Username = "staff1",
                        Email = "staff1@example.com",
                        FirstName = "Mike",
                        LastName = "Smith",
                        Role = "STAFF",
                        CreatedTime = DateTime.UtcNow,
                        UpdatedTime = DateTime.UtcNow,
                        IsActive = true,
                        Status = true
                    },
                    new TblUser
                    {
                        Username = "staff2",
                        Email = "staff2@example.com",
                        FirstName = "Anna",
                        LastName = "Smith",
                        Role = "STAFF",
                        CreatedTime = DateTime.UtcNow,
                        UpdatedTime = DateTime.UtcNow,
                        IsActive = true,
                        Status = true
                    }
                };

                foreach (var user in defaultUsers)
                {
                    // Encode the password for each user
                    user.Password = _passwordHasher.HashPassword(user, "P@ssw0rd");

                    // Save the user to the database
                    await _userRepository.CreateAsync(user);
                }
            }
        }
    }
}
