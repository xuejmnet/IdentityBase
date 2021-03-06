// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityBase.EntityFramework.DbContexts
{
    using System;
    using System.Threading.Tasks;
    using IdentityBase.EntityFramework.Entities;
    using IdentityBase.EntityFramework.Extensions;
    using IdentityBase.EntityFramework.Interfaces;
    using IdentityBase.EntityFramework.Configuration;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// DbContext for the IdentityServer configuration data.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="Interfaces.IConfigurationDbContext" />
    public class ConfigurationDbContext : DbContext, IConfigurationDbContext
    {
        private readonly EntityFrameworkOptions _storeOptions;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ConfigurationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <exception cref="ArgumentNullException">storeOptions</exception>
        public ConfigurationDbContext(
            DbContextOptions<ConfigurationDbContext> options,
            EntityFrameworkOptions storeOptions)
            : base(options)
        {
            this._storeOptions = storeOptions ?? throw
                new ArgumentNullException(nameof(storeOptions));
        }

        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Gets or sets the identity resources.
        /// </summary>
        /// <value>
        /// The identity resources.
        /// </value>
        public DbSet<IdentityResource> IdentityResources { get; set; }

        /// <summary>
        /// Gets or sets the API resources.
        /// </summary>
        /// <value>
        /// The API resources.
        /// </value>
        public DbSet<ApiResource> ApiResources { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Override this method to further configure the model that was
        /// discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" />
        /// properties on your derived context. The resulting model may be
        /// cached and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the
        /// model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure
        /// aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context
        /// (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureClientContext(this._storeOptions);
            modelBuilder.ConfigureResourcesContext(this._storeOptions);

            base.OnModelCreating(modelBuilder);
        }
    }
}