using HCE.Interfaces.Domain;
using HCE.Utility.Extensions;
using HCE.Utility.HelperOperation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Abstracts
{
    public abstract class EntityBase<TId> : IEntity<TId> where TId : struct
    {
        [NotMapped]
        public TId Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        private List<INotification> _domainEvents;
        public List<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(INotification eventItem)
        {
            if (_domainEvents is null) return;
            _domainEvents.Remove(eventItem);
        }
        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns>The cloned entity.</returns>
        public object Clone()
        {
            return this.DeepClone();
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(IEntity<TId> other)
        {
            if (ReferenceEquals(null, other))
                return false;

            return ReferenceEquals(this, other) || other.Id.Equals(Id);
        }

        /// <summary>
        ///     Sets the field values.
        /// </summary>
        /// <typeparam name="TFieldType">The type of the field.</typeparam>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property is set to a new value, <c>false</c> otherwise.</returns>
        protected virtual bool SetField<TFieldType>(ref TFieldType field, TFieldType value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<TFieldType>.Default.Equals(field, value))
                return false;
            field = value;
            return true;
        }

        /// <summary>
        ///     Sets a value object field while tracking changes if change tracking is enabled.
        /// </summary>
        /// <typeparam name="TValueObject">The type of the value object.</typeparam>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property is set to a new value, <c>false</c> otherwise.</returns>
        protected virtual bool SetValueObjectField<TValueObject>(ref TValueObject field, TValueObject value,
            [CallerMemberName] string propertyName = "") where TValueObject : ValueObject<TValueObject>
        {
            if (Equals(field, value))
                return false;
            field = value;
            return true;
        }
    }
}
