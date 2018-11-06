using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }
        public int? DispalyOrder { get; set; }
        public string Image { get; set; }
        public bool? HomeFlag { get; set; }

        public virtual IEnumerable Products { get; set; }
    }
}
