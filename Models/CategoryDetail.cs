using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace eshop.Models
{
    public class CategoryDetail
    {
        public int categoryId { get; set; }
        [Required(ErrorMessage ="Category Name Required")]
        [StringLength(100,ErrorMessage ="Minimum 3 and minimum 5 and maximum 100 charactor are allowed",MinimumLength =3)]
        public string categoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }

    }

    public class ProuctDetails
    {
        public int productId { get; set; }
        [Required(ErrorMessage ="Product Name is Required.")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charactor are allowed", MinimumLength = 3)]
        public string productName { get; set; }
        [Required]
        [Range(1,50)]
        public Nullable<int> categoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string Description { get; set; }
        public string productImg { get; set; }
        [Required]
        [Range(typeof(int),"1","50",ErrorMessage ="Invalid Quantity")]
        public Nullable<int> quantity { get; set; }
        [Required]
        [Range(typeof(decimal),"1","200000",ErrorMessage ="invalid Price")]
        public Nullable<decimal> price { get; set; }
        public SelectList Categories { get; set; }
    }
}