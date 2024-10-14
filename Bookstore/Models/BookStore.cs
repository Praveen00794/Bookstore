using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Bookstore.Models
{
    public class UserDetails : iUserDetails
    {
        public int userid { get; set; }
        [Required(ErrorMessage = "Please enter User Name")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int phone { get; set; }
    }
    public class BookDetails : IBookDetails
    {
        
        public int BookId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        public int? CategoryId { get; set; }
        public string Categoryname { get; set; }


        public string ImagePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishedDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quality must be non-negative.")]
        public int StockQuality { get; set; }
        public IList<Categorymaster> Dropdowncategories { get; set; }
        public HttpPostedFileBase bookupldimg { get ; set ; }
    }

    public class Categorymaster : ICategorymaster
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}