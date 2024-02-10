using HCE.Domain.Entities;
using HCE.Domain.Entities.Audit;
using HCE.Domain.Entities.General;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.KPIs;
using HCE.Domain.Entities.Lookup;
using HCE.Interfaces.DBContext;
using HCE.Interfaces.Domain;
using HCE.Interfaces.UserResolverHandler;
using HCE.Persistence.Configuration.TablesConfigrations;
using HCE.Persistence.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Persistence.DBContext
{
    public class HCEDbContext : DbContext, IHCEDbContext
    {
        private readonly IUserResolverHandler _userResolverHandler;

        internal HCEDbContext(DbContextOptions<HCEDbContext> options) : base(options)
        {
        }

        public HCEDbContext(DbContextOptions<HCEDbContext> options, IUserResolverHandler userResolverHandler) : base(options)
        {
            _userResolverHandler = userResolverHandler;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurationDBTables.ApplyConfigurations(modelBuilder);
            modelBuilder.Seed();
            //modelBuilder.Ignore<EntityFrameworkCore.MemoryJoin.QueryModelClass>();
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.ApplyAuditInformation(_userResolverHandler);
            var temoraryAuditEntities = ChangeTracker.AuditNonTemporaryProperties(AuditChangedData, _userResolverHandler).Result;
            var result = base.SaveChanges();
            if (result >= 0)
            {
                var auditTemporaryPropertiesResult = ChangeTracker.AuditTemporaryProperties(temoraryAuditEntities, AuditChangedData).Result;
                if (auditTemporaryPropertiesResult)
                    base.SaveChanges();
            }
            return result;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.ApplyAuditInformation(_userResolverHandler);

            var temoraryAuditEntities = await ChangeTracker.AuditNonTemporaryProperties(AuditChangedData, _userResolverHandler);
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            if (result >= 0)
            {
                var auditTemporaryPropertiesResult = await ChangeTracker.AuditTemporaryProperties(temoraryAuditEntities, AuditChangedData);
                if (auditTemporaryPropertiesResult)
                    await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            return result;
        }

        #region Tables

        #region Audit
        public DbSet<AuditUserAction> AuditUserAction { get; set; }
        public DbSet<AuditChangedData> AuditChangedData { get; set; }
        #endregion

        #region Memory Join
        // This is virtual table used by MemoryJoin Libirary in memory only not create in DB
        public DbSet<EntityFrameworkCore.MemoryJoin.QueryModelClass> QueryData { get; set; }
        #endregion

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<RoleModules> RoleModules { get; set; }
        #endregion

        #region Common
        public DbSet<Attachment> Attachment { get; set; }
        #endregion
        #region Lookup

        public DbSet<WorldRegion> WorldRegion { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<StateRegion> StateRegion { get; set; }
        public DbSet<City> City { get; set; }

        public DbSet<District> District { get; set; }

        public DbSet<Cluster> Cluster { get; set; }
        public DbSet<Goal> Goal { get; set; }
        public DbSet<AccessTechnology> AccessTechnology { get; set; }
        public DbSet<CoreType> CoreType { get; set; }
        public DbSet<Vendor> Vendor { get; set; }


        public DbSet<Domains> Domains { get; set; }
        public DbSet<SubSystem> SubSystems { get; set; }
        public DbSet<Codec> Codec { get; set; }
        public DbSet<MeasuringUnit> MeasuringUnit { get; set; }

        public DbSet<DataSource> DataSource { get; set; }
        public DbSet<DieselGenerator> DieselGenerator { get; set; }

        public DbSet<ExternalBaseStation> ExternalBaseStation { get; set; }
        public DbSet<ExternalOperator> ExternalOperator { get; set; }

        public DbSet<Category> KPICategory { get; set; }
        public DbSet<KPIFeedingLog> KPIFeedingLog { get; set; }

        public DbSet<Operator> Operator { get; set; }

        public DbSet<OperatorGroup> OperatorGroup { get; set; }
        public DbSet<Priority> Priority { get; set; }

        public DbSet<Service> Service { get; set; }

        public DbSet<Site> Site { get; set; }

        #endregion

        #region KPIs
        public DbSet<Kpi> KPI { get; set; }

        public DbSet<Weight> Weights { get; set; }

        public DbSet<KpiCategories> KPICategories { get; set; }


        #endregion
        #endregion
    }
}
