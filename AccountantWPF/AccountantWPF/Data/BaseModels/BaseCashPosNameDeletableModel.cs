using System.ComponentModel.DataAnnotations.Schema;

namespace AccountantWPF.Data.BaseModels
{
    public abstract class BaseCashPosNameDeletableModel : BaseDeletableModel, ICashPosNameEntity
    {
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cash { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pos { get; set; }
    }
}
