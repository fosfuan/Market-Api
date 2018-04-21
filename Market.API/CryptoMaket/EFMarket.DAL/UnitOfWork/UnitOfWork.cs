using CryptoMaket.EFMarket_DAL.Models.DB;
using EFMarket.DAL.EFRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private CryptoMarketContext Context;
        private IUserRepository userRepository;
        private IUserRoleRepository userRoleRepository;
        private ICoinsRepository coinsRepository;
        private IUserRefreshTokenRepository userRefreshTokenRepository;


        public IUserRoleRepository UserRoleRepository => userRoleRepository = userRoleRepository ?? new UserRoleRepository(this.Context);
        public ICoinsRepository CoinsRepository => coinsRepository = coinsRepository ?? new CoinsRepository(this.Context);
        public IUserRepository UserRepository => userRepository = userRepository ?? new UserRepository(this.Context);
        public IUserRefreshTokenRepository UserRefreshTokenRepository => userRefreshTokenRepository = userRefreshTokenRepository ?? new UserRefreshTokenRepository(this.Context);
        //private IUserRepository userRepository;

        // public IUserRepository UserRepository => userRepository = userRepository ?? new UserRepository(this.Context);
        public UnitOfWork(CryptoMarketContext dbContext)
        {
            this.Context = dbContext;
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await this.Context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return this.Context.Database.BeginTransaction();
        }
    }
}
