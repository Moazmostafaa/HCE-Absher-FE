using HCE.Domain.Entities.Audit;
using HCE.Domain.Entities.Enum;
using HCE.Interfaces.Domain;
using HCE.Interfaces.UserResolverHandler;
using HCE.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.Extentions
{
    public static class ChangeTrackerExtentions
    {
        public static void ApplyAuditInformation(this ChangeTracker changeTracker, IUserResolverHandler _userResolverHandler)
        {
            DateTime tempdate = DateTime.Now.GetCurrentDateTime();
            foreach (var entry in changeTracker.Entries().Where(e => e.Entity.GetType().InheritsOrImplements(typeof(IEntity<>)) && (e.State == EntityState.Added || e.State == EntityState.Modified)))
            {
                TirmStrings(entry.Entity);
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(nameof(IEntity<Guid>.CreatedDate)).CurrentValue = tempdate;
                        entry.Property(nameof(IEntity<Guid>.CreatedBy)).CurrentValue = !string.IsNullOrEmpty(entry.Property(nameof(IEntity<Guid>.CreatedBy)).CurrentValue?.ToString()) ? entry.Property(nameof(IEntity<Guid>.CreatedBy)).CurrentValue.ToString() : _userResolverHandler?.GetUserId() ?? string.Empty;
                        break;
                    case EntityState.Modified:
                        entry.Property(nameof(IEntity<Guid>.CreatedBy)).IsModified = false;
                        entry.Property(nameof(IEntity<Guid>.CreatedDate)).IsModified = false;
                        if (entry.GetType().InheritsOrImplements(typeof(IUpdateEntity<>)))
                        {
                            entry.Property(nameof(IUpdateEntity<Guid>.UpdatedDate)).CurrentValue = tempdate;
                            entry.Property(nameof(IUpdateEntity<Guid>.UpdatedBy)).CurrentValue = !string.IsNullOrEmpty(entry.Property(nameof(IUpdateEntity<Guid>.UpdatedBy)).CurrentValue?.ToString()) ? entry.Property(nameof(IUpdateEntity<Guid>.UpdatedBy)).CurrentValue.ToString() : _userResolverHandler?.GetUserId().ToString() ?? string.Empty;
                        }
                        break;
                }
            }

            foreach (var entity in changeTracker.Entries().Where(e => e.Entity.GetType().InheritsOrImplements(typeof(ISoftDeletable)) && e.State == EntityState.Deleted))
            {
                entity.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
                entity.Property(nameof(ISoftDeletable.DeletedDate)).CurrentValue = tempdate;
                entity.State = EntityState.Modified;
            }
        }

        public static async Task<List<AuditEntry>> AuditNonTemporaryProperties(this ChangeTracker changeTracker, DbSet<AuditChangedData> AuditDataChanges, IUserResolverHandler _userResolverHandler)
        {
            var auditEntries = new List<AuditEntry>();
            //var temporaryAuditDataChanges = new List<Tuple<EntityEntry, AuditDataChanges>>();
            try
            {
                changeTracker.DetectChanges();
                Guid identifierSaveChangesId = Guid.NewGuid();
                DateTime tempdate = DateTime.Now.GetCurrentDateTime();
                var entitiesToTrack = changeTracker.Entries().Where(e => e.Entity.GetType().InheritsOrImplements(typeof(IEntity<>)) &&
                                                      (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted) &&
                                                     !(e.Entity is AuditChangedData || e.State == EntityState.Detached || e.State == EntityState.Unchanged));

                if (entitiesToTrack != null && entitiesToTrack.Any())
                {
                    foreach (var entry in entitiesToTrack)
                    {
                        var auditEntry = new AuditEntry(entry);
                        auditEntry.CreationDate = tempdate;
                        auditEntry.IdentifierSaveChangesId = identifierSaveChangesId;
                        auditEntry.CreatedBy = !string.IsNullOrEmpty(entry.Property(nameof(IEntity<Guid>.CreatedBy)).CurrentValue?.ToString()) ? entry.Property(nameof(IEntity<Guid>.CreatedBy)).CurrentValue.ToString() : _userResolverHandler?.GetUserId() ?? string.Empty;

                        if (entry.GetType().InheritsOrImplements(typeof(ISoftDeletable)))
                        {
                            auditEntry.ChangeType = AuditDataChangeType.SoftDelete.ToString();
                        }
                        else
                        {
                            switch (entry.State)
                            {
                                case EntityState.Added:
                                    auditEntry.ChangeType = AuditDataChangeType.Create.ToString();
                                    break;
                                case EntityState.Deleted:
                                    auditEntry.ChangeType = AuditDataChangeType.Delete.ToString();
                                    break;
                                case EntityState.Modified:
                                    auditEntry.ChangeType = AuditDataChangeType.Update.ToString();
                                    break;
                            }
                        }
                        //List<string> changedColumns = new List<string>();
                        //Dictionary<string, object> primaryKey = new Dictionary<string, object>();
                        //Dictionary<string, object> oldValues = new Dictionary<string, object>();
                        //Dictionary<string, object> newValues = new Dictionary<string, object>();
                        foreach (var property in entry.Properties)
                        {
                            if (property.IsTemporary)
                            {
                                // value will be generated by the database, get the value after saving
                                auditEntry.TemporaryProperties.Add(property);
                                continue;
                            }

                            string propertyName = property.Metadata.Name;
                            if (property.Metadata.IsPrimaryKey())
                            {
                                auditEntry.PrimaryKey[propertyName] = property.CurrentValue;
                                continue;
                            }

                            if (entry.GetType().InheritsOrImplements(typeof(ISoftDeletable)))
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                            }
                            else
                            {
                                switch (entry.State)
                                {
                                    case EntityState.Added:
                                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                                        break;
                                    case EntityState.Deleted:
                                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                                        break;
                                    case EntityState.Modified:
                                        if (property.IsModified)
                                        {
                                            auditEntry.ChangedColumns.Add(propertyName);
                                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                                        }
                                        break;
                                }
                            }
                        }
                        auditEntries.Add(auditEntry);
                    }

                    await AuditDataChanges.AddRangeAsync(auditEntries.Where(p => !p.HasTemporaryProperties).Select(p => p.ToAudit()));
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "");
            }
            return auditEntries.Where(p => p.HasTemporaryProperties).ToList();
        }

        public static async Task<bool> AuditTemporaryProperties(this ChangeTracker changeTracker, List<AuditEntry> auditEntries, DbSet<AuditChangedData> AuditDataChanges)
        {
            try
            {
                if (auditEntries == null || auditEntries.Count == 0)
                    return await Task.FromResult(true);

                foreach (var auditEntry in auditEntries)
                {
                    // Get the final value of the temporary properties
                    foreach (var prop in auditEntry.TemporaryProperties)
                    {
                        if (prop.Metadata.IsPrimaryKey())
                            auditEntry.PrimaryKey[prop.Metadata.Name] = prop.CurrentValue;
                        else
                            auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }

                    // Save the Audit entry
                    AuditDataChanges.Add(auditEntry.ToAudit());
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "");
            }

            return await Task.FromResult(true);
        }

        #region Helper Methods
        private static void TirmStrings(object entity)
        {
            var stringProperties = entity.GetType().GetProperties()
                             .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(entity, null);
                if (!string.IsNullOrEmpty(currentValue))
                    stringProperty.SetValue(entity, currentValue.Trim(), null);
            }
        }
        #endregion
    }

    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string CreatedBy { get; set; }
        public string TableName { get; set; }
        public string SchemaName { get; set; }
        public string ChangeType { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid IdentifierSaveChangesId { get; set; }
        public List<string> ChangedColumns { get; set; } = new List<string>();
        public Dictionary<string, object> PrimaryKey { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public AuditChangedData ToAudit()
        {
            var audit = new AuditChangedData();
            audit.CreatedBy = CreatedBy;
            audit.ChangeType = ChangeType;
            audit.CreatedDate = CreationDate;
            audit.AuditDataChangesId = Guid.NewGuid();
            audit.SchemaName = Entry.Metadata.GetSchema();
            audit.TableName = Entry.Metadata.GetTableName();
            audit.IdentifierSaveChangesId = IdentifierSaveChangesId;
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues); // In .NET Core 3.1+, you can use System.Text.Json instead of Json.NET
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.PrimaryKey = PrimaryKey.Count == 0 ? null : JsonConvert.SerializeObject(PrimaryKey);
            audit.ChangedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
