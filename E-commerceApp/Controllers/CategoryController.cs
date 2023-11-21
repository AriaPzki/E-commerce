using E_commerceApp.Data;
using E_commerceApp.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using System;

namespace E_commerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        // inside the parameter telling what object or implementation we need.
        public CategoryController(ApplicationDbContext db)
        {
            _db= db;
        }

        public IActionResult Index()
        {
            // No need to write SQL Queries, using entity framework core instead.
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            // if you want you can pass new category here, but that is not required.
            // If nothing is passed here and if we are defining the category,
            // it will create a new object for that.
                return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // it will see all validations in Category Model
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order must not be the same");
            }

            if (ModelState.IsValid)
             {
                    _db.Categories.Add(obj);
                    _db.SaveChanges();
                // If you assign a value in temp data, it is only available for the next render.
                //Temp data has been created in the.Net framework with that sole purpose that if you have to display a notification on the next page, you add some value in temp data.
                TempData["success"] =  "Category Created Successfully";
                    // to reload the category list , we jump to Index action not any other views
                    return RedirectToAction("Index");
             }
            return View();
         }

        // To Edit Category
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
           // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
           // Category? categoryFromDb2 = _db.Categories.Where(u => u.Id== id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            //if you ever try to update a record and it is creating a new record, make sure the ID 
            //is populated here. If that does not work, then you have to add the hidden property
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //In that case, we cannot use the same name for the action method because then the parameters are also same for get and post.
        //Because in the form when we are posting it will look for the same delete action method.
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}

