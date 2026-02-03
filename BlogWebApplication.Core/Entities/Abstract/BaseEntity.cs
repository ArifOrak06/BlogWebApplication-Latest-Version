namespace BlogWebApplication.Core.Entities.Abstract
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual string? ModifiedBy { get; set; }
        public virtual string? DeletedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }  
        public virtual DateTime ModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
