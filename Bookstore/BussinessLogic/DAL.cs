using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Bookstore.Models;

namespace Bookstore.BussinessLogic
{
    public class BookstoreDAL
    {
        string constr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
        DataTable DT;
        public bool Verifylogin(string username,string pass)
        {
            bool isvalid = false;
            using(SqlConnection con=new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from Users where Username=@Username and PasswordHash=@password", con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@password", pass);
                int count = (int)cmd.ExecuteScalar();
                isvalid = count > 0;
            }


            return isvalid;
        }
        public  IList<BookDetails>GetBookdetails()
        {
            DT = new DataTable();
            IList<BookDetails> lst = new List<BookDetails>();
            using(SqlConnection con=new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GetBookDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DA.Fill(DT);
                lst = (from DataRow r in DT.Rows
                       select new BookDetails()
                       {
                           BookId=Convert.ToInt32(r["BookId"]),
                           Title = Convert.ToString(r["Title"]),
                           Author = Convert.ToString(r["Author"]),
                           Description = Convert.ToString(r["Description"]),
                           Price = Convert.ToInt32(r["Price"]),
                           StockQuality = Convert.ToInt32(r["StockQuantity"]),
                           Categoryname = Convert.ToString(r["CategoryName"]),
                           //PublishedDate = r["PublishedDate"] != DBNull.Value ? Convert.ToDateTime(r["PublishedDate"]) : (DateTime?)null
                           PublishedDate = r["PublishedDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["PublishedDate"]) : null


            }).ToList();



            }
            return lst;
        }
        public IList<Categorymaster> GetBookCategory()
        {
            DT = new DataTable();
            IList<Categorymaster> lst = new List<Categorymaster>();
            using(SqlConnection con=new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("select CategoryID,CategoryName from Categories", con);
                con.Open();
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DA.Fill(DT);
                lst = (from DataRow r in DT.Rows
                       select new Categorymaster()
                       {
                           CategoryID = Convert.ToInt32(r["CategoryID"]),
                           CategoryName = Convert.ToString(r["CategoryName"])
                       }
                     ).ToList();
            }
            return lst;
        }
        public BookDetails Editbookload(int id)
        {
            BookDetails obj = new BookDetails();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM Books WHERE BookID = @BookID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookID", id);
                    con.Open();

                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        if (DR.Read())
                        {
                            obj.BookId = (int)DR["BookID"];
                            obj.Title = DR["Title"] != DBNull.Value ? DR["Title"].ToString() : string.Empty;
                            // Check for each field's existence and handle DBNull
                            obj.Author = DR["Author"] != DBNull.Value ? DR["Author"].ToString() : string.Empty;
                            obj.Description = DR["Description"] != DBNull.Value ? DR["Description"].ToString() : string.Empty;

                            // Check if StockQuality exists and is not DBNull
                            obj.StockQuality = DR["StockQuantity"] != DBNull.Value ? (int)DR["StockQuantity"] : 0;

                            // Price check
                            obj.Price = DR["Price"] != DBNull.Value ? Convert.ToDecimal(DR["Price"]) : 0;


                            // PublishedDate check
                            obj.PublishedDate = DR["PublishedDate"] != DBNull.Value ? (DateTime)DR["PublishedDate"] : DateTime.MinValue;

                            // CategoryID check
                            obj.CategoryId = DR["CategoryID"] != DBNull.Value ? (int)DR["CategoryID"] : 0;
                        }
                    }
                }
            }
            return obj;
        }
        public void InsertUpdBook(BookDetails model)
        {
            using(SqlConnection con=new SqlConnection(constr))
            {
               using(SqlCommand cmd=new SqlCommand("InsertNewBooks", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("BookID", model.BookId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title",model.Title);
                    cmd.Parameters.AddWithValue("@Price", model.Price);
                    cmd.Parameters.AddWithValue("@CategoryID", model.CategoryId);
                    cmd.Parameters.AddWithValue("@Author", model.Author);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@PublishedDate", model.PublishedDate);
                    cmd.Parameters.AddWithValue("@StockQuantity", model.StockQuality);
                    cmd.Parameters.AddWithValue("@Bookimg", model.ImagePath);
                    cmd.ExecuteNonQuery();
                }
            }

            
        }


    }
}