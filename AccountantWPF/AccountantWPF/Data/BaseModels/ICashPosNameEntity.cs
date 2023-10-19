namespace AccountantWPF.Data.BaseModels
{
    public interface ICashPosNameEntity
    {
        string Name { get; set; }
        decimal Cash { get; set; }
        decimal Pos { get; set; }
    }
}
