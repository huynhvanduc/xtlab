using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCore
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { set; get; }

        [Required]
        [StringLength(50)]
        public string Name { set; get; }

        [StringLength(50)]
        public string Provider { set; get; }

    }
}
