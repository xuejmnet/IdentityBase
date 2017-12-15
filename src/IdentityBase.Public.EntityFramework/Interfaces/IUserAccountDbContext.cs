namespace IdentityBase.Public.EntityFramework.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using IdentityBase.Public.EntityFramework.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IUserAccountDbContext : IDisposable
    {
        DbSet<UserAccount> UserAccounts { get; set; }
        DbSet<ExternalAccount> ExternalAccounts { get; set; }
        DbSet<UserAccountClaim> UserAccountClaims { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}