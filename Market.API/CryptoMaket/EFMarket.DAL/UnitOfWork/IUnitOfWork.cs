﻿using EFMarket.DAL.EFRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL
{
    public interface IUnitOfWork
    {
        EFUserRepository UserRepository { get; }

        IUserRoleRepository UserRoleRepository { get; }
        void Save();

        Task SaveAsync();

        IDbContextTransaction BeginTransaction();
    }
}
