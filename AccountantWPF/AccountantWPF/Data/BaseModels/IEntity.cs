namespace AccountantWPF.Data.BaseModels
{
    public interface IEntity
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}
