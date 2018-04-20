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
        private EFUserRepository userRepo;

        public EFUserRepository UserRepository => userRepo = userRepo ?? new EFUserRepository(this.Context);

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
