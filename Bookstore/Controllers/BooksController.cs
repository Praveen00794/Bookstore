﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookstore.Models;
using Bookstore.BussinessLogic;
namespace Bookstore.Controllers
{[Authorize]
    public class BooksController : Controller
    {
         BookstoreDAL ObjDAL = new BookstoreDAL();
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookPage()
        {
            IList<BookDetails> objbookdetails = new List<BookDetails>();
            objbookdetails = ObjDAL.GetBookdetails();
            
            return View(objbookdetails);
        }

        public ActionResult Insertbook()
        {

            BookDetails obj = new BookDetails();
            if (TempData["bookemp"] != null)
            {
                obj = (BookDetails)TempData["bookemp"];
            }
            obj.Countriesmstlst = ObjDAL.GetCountryDropdownlsts();
            obj.Statemstlst = ObjDAL.GetStateDropdownlsts(obj.countryid);
            obj.Dropdowncategories = ObjDAL.GetBookCategory();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Insertbook(BookDetails model)
        {
            BookDetails obj = new BookDetails();
            
            if (ModelState.IsValid)
            {
                string strfilename = Convert.ToString(model.bookupldimg);
                if (model.bookupldimg!=null && model.bookupldimg.ContentLength>0)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(model.bookupldimg.FileName);
                    string fileextension = System.IO.Path.GetExtension(model.bookupldimg.FileName);
                    filename = model.Title + fileextension;
                    string uploadpath = Server.MapPath("../booksuploadfiles/");
                    model.ImagePath = filename;
                    model.bookupldimg.SaveAs(uploadpath + model.ImagePath);
                }
                else
                {
                    model.ImagePath = "nill";
                }
               
                ObjDAL.InsertUpdBook(model);
                obj.Dropdowncategories = ObjDAL.GetBookCategory();
                return RedirectToAction("BookPage");
                
            }
            
                obj.Dropdowncategories = ObjDAL.GetBookCategory();
                obj.Countriesmstlst = ObjDAL.GetCountryDropdownlsts();
                obj.Statemstlst = ObjDAL.GetStateDropdownlsts(obj.countryid);
            return View(obj);
            
            
        }
        public ActionResult Editbook(int id)
        {
            BookDetails obj = new BookDetails();
            obj = ObjDAL.Editbookload(id);
            TempData["bookemp"] = obj;
            return RedirectToAction("Insertbook");
        }
        public JsonResult PopulateStatecmb(int countryid)
        {
            IList<StateDropdownlst> lst = ObjDAL.GetStateDropdownlsts(countryid);
            return Json(lst);
        }
        public ActionResult Deletebook(int id)
        {
            ObjDAL.DeleteEmp(id);
            return RedirectToAction("BookPage");
        }


        public ActionResult BookImport()
        {
            return View();
        }


    }
}