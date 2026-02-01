namespace BlogWebApplication.Core.Entities.Abstract
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }  
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
