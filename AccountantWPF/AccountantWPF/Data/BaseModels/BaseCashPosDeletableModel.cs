using System.ComponentModel.DataAnnotations.Schema;

namespace AccountantWPF.Data.BaseModels
{
    public abstract class BaseCashPosDeletableModel : BaseDeletableModel, ICashPosEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cash { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pos { get; set; }
    }
}
