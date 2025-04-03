namespace Platform.Entity
{
    public abstract class BaseEntity
    {
        public DateTime? DeletionDate { get; set; }

        public DateTime CreationDate { get; set; }
    
        public DateTime? UpdateDate { get; set; }

    }
}
