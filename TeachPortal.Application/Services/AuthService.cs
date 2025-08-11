using System;
using Microsoft.AspNet.Identity;
using TeachPortal.Domain.Entities;
using TeachPortal.Domain.Repositories;
using TeachPortal.Application.Interfaces;
using TeachersPortal.Api.Application.Requests;
using TeachersPortal.Api.Application.Interfaces.Auth;
using TeachersPortal.Api.Domain.Repositories.Core;

namespace TeachPortal.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _hasher;
        private readonly IAppUserRepository _userRepo;
        private readonly ITokenService _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IAppUserRepository userRepo, IPasswordHasher hasher, ITokenService tokenGenerator, IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _hasher = hasher;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task SignupAsync(SignupRequest req)
        {
            if (await _userRepo.ExistsByUsername(req.Username))
                throw new Exception("Username already exists");

            var hash = _hasher.HashPassword(req.Password);

            var user = new AppUser
            {
                Username = req.Username,
                HashPassword = hash,
                TeacherId = req.TeacherId
            };

            await _userRepo.AddAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<string> LoginTeacherAsync(LoginRequest req)
        {
            var user = await _userRepo.GetByUsernameAsync(req.Username);

            if (user == null || _hasher.VerifyHashedPassword(user.HashPassword, req.Password) != PasswordVerificationResult.Success)
                throw new Exception("Invalid credentials");

            return _tokenGenerator.CreateToken(user);
        }
    }
}
