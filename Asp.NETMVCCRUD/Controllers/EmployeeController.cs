﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;

namespace Asp.NETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using(DBModelEntities db = new DBModelEntities())
            {
                List<Employee> empList = db.Employees.ToList<Employee>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }    
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
                return View(new Employee());
            else
            {
                using (DBModelEntities db = new DBModelEntities())
                {
                    return View(db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>());
                }    
            }    
        }
        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            using(DBModelEntities db = new DBModelEntities())
            {
                if(emp.EmployeeID == 0)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Save Successfully" }, JsonRequestBehavior.AllowGet);
                }    
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }    
                
            }    
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using(DBModelEntities db = new DBModelEntities())
            {
                Employee emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }    
        }
    }
}