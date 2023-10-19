﻿using System.ComponentModel.DataAnnotations;

namespace AccountantWPF.Data.BaseModels
{
    public abstract class BaseModel : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}