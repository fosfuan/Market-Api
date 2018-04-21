using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.EFMarket_DAL.Models.DB;
using EFMarket.DAL;
using EFMarket.DAL.EFRepositories;
using Market.DAL;
using Market.DAL.Repositories;

namespace Market.Services.Services
{
    public class UserRefreshTokenService : IUserRefreshTokenService
    {
        private readonly IUserRefreshTokenRepository userRefreshTokenRepository;
        
        public UserRefreshTokenService(IUnitOfWork unitOfWork)
        {
            this.userRefreshTokenRepository = unitOfWork.UserRefreshTokenRepository;
        }

        public async Task<bool> AddRefreshToken(int userId, string refreshToken)
        {
            return await this.userRefreshTokenRepository.AddRefreshToken(userId, refreshToken);
        }

        public async Task<UserRefreshToken> GetRefreshTokenByUserId(int userId)
        {
            return await this.userRefreshTokenRepository.GetRefreshTokenByUserId(userId);
        }

        public async Task<bool> RemoveRefreshToken(int userId)
        {
            return await this.userRefreshTokenRepository.RemoveRefreshToken(userId);
        }
    }
}
