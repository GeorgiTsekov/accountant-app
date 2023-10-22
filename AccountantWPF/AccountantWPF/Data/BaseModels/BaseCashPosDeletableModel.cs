using System.ComponentModel.DataAnnotations.Schema;

namespace AccountantWPF.Data.BaseModels
{
    public abstract class BaseCashPosDeletableModel : BaseDeletableModel
    {
        public string Name { get; set; } = string.Empty;
    }
}
