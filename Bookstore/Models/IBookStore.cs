using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bookstore.Models
{
    interface iUserDetails
    {
        int userid { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Fname { get; set; }
        string  Lname { get; set; }
        int phone { get; set; }
        
    }
    public interface IBookDetails
    {
        int BookId { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        int? CategoryId { get; set; }
        string Categoryname { get; set; }
        IList<Categorymaster>Dropdowncategories { get; set; }
        string ImagePath { get; set; }
        HttpPostedFileBase bookupldimg { get; set; }
        DateTime? PublishedDate { get; set; }
        int StockQuality { get; set; }
        
    }
    public interface ICategorymaster
    {
        int CategoryID { get; set; }
        string CategoryName { get; set; }
    }


}
