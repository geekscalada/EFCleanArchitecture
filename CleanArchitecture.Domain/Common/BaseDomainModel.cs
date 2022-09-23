namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseDomainModel
    {
        
        //Importante que esta clase sea de tipo abstracta para que no permita instanciar

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
