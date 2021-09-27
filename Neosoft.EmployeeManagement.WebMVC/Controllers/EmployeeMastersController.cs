using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Neosoft.EmployeeManagement.BAL.Employee;
using Neosoft.EmployeeManagement.DAL.DAL;
using Neosoft.EmployeeManagement.DAL.EFDbFirst;
using System.Linq.Dynamic;
using Neosoft.EmployeeManagement.WebMVC.Models;
using System.IO;

namespace Neosoft.EmployeeManagement.WebMVC.Controllers
{
    public class EmployeeMastersController : Controller
    {
        private IEmployeeMasterBAL employeeMasterBAL = new EmployeeMasterBAL(new EmployeeMasterDAL());

        // GET: EmployeeMasters
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadData()
        {
            var employeeMasters = employeeMasterBAL.GetEmployeeMasters();

            var dataView = employeeMasters.Select(t => new EmployeeMasterView
            {
                Row_Id = t.Row_Id,
                CountryName = t.Country.CountryName,
                StateName = t.State.StateName,
                CityName = t.City.CityName,
                MobileNumber = t.MobileNumber,
                PanNumber = t.PanNumber,
                PassportNumber = t.PassportNumber,
                EmailAddress = t.EmailAddress,
                ProfileImage = t.ProfileImage,
                Gender = t.Gender == 1 ? "Male" : "Female",
                IsActive = t.IsActive == true ? "Yes" : "No",
            });

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var v = (from a in dataView select a);

            //SORT
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                v = v.OrderBy(sortColumn + " " + sortColumnDir);
            }

            recordsTotal = v.Count();
            var data = v.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEditEmployee(int EmployeeId)
        {
            EmployeeMaster emp;
            if (EmployeeId > 0)
            {
                // Edit Case
                emp = employeeMasterBAL.GetEmployeeMasterById(EmployeeId);
            }
            else
            {
                // Create case
                emp = new EmployeeMaster();

                DateTime todayDate = DateTime.Now;
                DateTime lastDateFor18 = DateTime.Now.AddYears(-18);

                emp.DateOfBirth = lastDateFor18;
                emp.DateOfJoinee = todayDate;
            }

            ViewBag.CountryId = GetCountries(emp.CountryId);
            ViewBag.CityId = GetCities(emp.CityId);
            ViewBag.StateId = GetStates(emp.StateId);

            return PartialView("CreateEdit", emp);
        }

        private List<SelectListItem> GetCountries(int countryId)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<Country> dbCountries = employeeMasterBAL.GetCountries();

            if (countryId == 0)
            {
                selectListItems.Add(new SelectListItem { Text = "Please select a country", Value = "", Selected = true });
            }

            dbCountries.ForEach(s =>
            {
                bool selected = false;
                if (s.Row_Id.Equals(countryId))
                {
                    selected = true;
                }
                selectListItems.Add(new SelectListItem { Text = s.CountryName, Value = s.Row_Id.ToString(), Selected = selected });
            });

            return selectListItems;
        }
        private List<SelectListItem> GetStates(int stateId)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<State> dbStates = employeeMasterBAL.GetStates();

            if (stateId == 0)
            {
                selectListItems.Add(new SelectListItem { Text = "Please select a state", Value = "", Selected = true });
            }

            dbStates.ForEach(s =>
            {
                bool selected = false;
                if (s.Row_Id.Equals(stateId))
                {
                    selected = true;
                }
                selectListItems.Add(new SelectListItem { Text = s.StateName, Value = s.Row_Id.ToString(), Selected = selected });
            });

            return selectListItems;
        }

        private List<SelectListItem> GetCities(int cityId)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<City> dbCity = employeeMasterBAL.GetCities();

            if (cityId == 0)
            {
                selectListItems.Add(new SelectListItem { Text = "Please select a state", Value = "", Selected = true });
            }

            dbCity.ForEach(s =>
            {
                bool selected = false;
                if (s.Row_Id.Equals(cityId))
                {
                    selected = true;
                }
                selectListItems.Add(new SelectListItem { Text = s.CityName, Value = s.Row_Id.ToString(), Selected = selected });
            });

            return selectListItems;
        }

        public JsonResult GetStates(string id)
        {
            List<SelectListItem> states = new List<SelectListItem>();
            int.TryParse(id, out int countryId);
            List<State> dbStates = employeeMasterBAL.GetStates().Where(t => t.CountryId.Equals(countryId)).ToList();
            states.Add(new SelectListItem { Text = "Please select a state", Value = "", Selected = true });

            dbStates.ForEach(s =>
            {
                states.Add(new SelectListItem { Text = s.StateName, Value = s.Row_Id.ToString() });
            });

            return Json(new SelectList(states, "Value", "Text"));
        }

        public JsonResult GetCities(string id)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            int.TryParse(id, out int stateId);
            List<City> dbCities = employeeMasterBAL.GetCities().Where(t => t.StateId.Equals(stateId)).ToList();
            cities.Add(new SelectListItem { Text = "Please select a city", Value = "", Selected = true });
            dbCities.ForEach(s =>
            {
                cities.Add(new SelectListItem { Text = s.CityName, Value = s.Row_Id.ToString() });
            });

            return Json(new SelectList(cities, "Value", "Text"));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeMaster employeeMaster = employeeMasterBAL.GetEmployeeMasterById(id);
            employeeMasterBAL.DeleteEmployeeMaster(employeeMaster);
            return RedirectToAction("Index");
        }

        // GET: EmployeeMasters/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EmployeeMaster employeeMaster = db.EmployeeMasters.Find(id);
        //    if (employeeMaster == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employeeMaster);
        //}

        //// GET: EmployeeMasters/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CityId = new SelectList(db.Cities, "Row_Id", "CityName");
        //    ViewBag.CountryId = new SelectList(db.Countries, "Row_Id", "CountryName");
        //    ViewBag.StateId = new SelectList(db.States, "Row_Id", "StateName");
        //    return View();
        //}

        //// POST: EmployeeMasters/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Row_Id,EmployeeCode,FirstName,LastName,CountryId,StateId,CityId,EmailAddress," +
            "MobileNumber,PanNumber,PassportNumber,ProfileImage,Gender,IsActive,DateOfBirth,DateOfJoinee,CreatedDate," +
            "UpdatedDate,IsDeleted,DeletedDate")] EmployeeMaster employeeMaster)
        {
            if (ModelState.IsValid)
            {
                //if (file != null)
                //{
                //    string path = Path.Combine(Server.MapPath("~UploadedFiles"), Path.GetFileName(file.FileName));
                //    file.SaveAs(path);
                //    employeeMaster.ProfileImage = "/UploadedFiles/" + file.FileName;
                //}

                employeeMasterBAL.InsertEmployeeMaster(employeeMaster);
                return RedirectToAction("Index");
            }
            else
            {

                var errors = new List<string>();
                foreach (var modelStateVal in ModelState.Values)
                {
                    errors.AddRange(modelStateVal.Errors.Select(error => error.ErrorMessage));
                }
                return Json(new { status = "validationerror", errors = errors });
            }

            // ViewBag.CountryId = GetCountries(employeeMaster.CountryId);
            // ViewBag.CityId = GetCities(employeeMaster.CityId);
            // ViewBag.StateId = GetStates(employeeMaster.StateId);

            //return PartialView("CreateEdit", employeeMaster);

        }

        [HttpGet]
        public JsonResult IsEmailExits(string EmailAddress, int? id)
        {
            bool isEmailExists = false;
            if (id == null)
            {
                isEmailExists = employeeMasterBAL.GetEmployeeMasters().Any(s => s.EmailAddress == EmailAddress);
            }
            else
            {
                isEmailExists = employeeMasterBAL.GetEmployeeMasters().Any(s => s.Row_Id== id.GetValueOrDefault() && s.EmailAddress == EmailAddress);
            }

            return Json(!isEmailExists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult IsPanNumberExits(string PanNumber, int? id)
        {
            bool isPanNumberExists = false;
            if (id == null)
            {
                isPanNumberExists = employeeMasterBAL.GetEmployeeMasters().Any(s => s.PanNumber== PanNumber);
            }
            else
            {
                isPanNumberExists = employeeMasterBAL.GetEmployeeMasters().Any(s => s.Row_Id == id.GetValueOrDefault() &&
                s.PanNumber== PanNumber);
            }

            return Json(!isPanNumberExists, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsPassportNumberExits(string PassportNumber ,int? id)
        {
            bool isPassportNumberExists = false;
            if (id == null)
            {
                isPassportNumberExists = employeeMasterBAL.GetEmployeeMasters().Any(s => s.PassportNumber == PassportNumber);
            }
            else
            {
                isPassportNumberExists = employeeMasterBAL.GetEmployeeMasters().Any(s => s.Row_Id == id.GetValueOrDefault() &&
                s.PassportNumber == PassportNumber);
            }

            return Json(!isPassportNumberExists, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsMobileNumberExits(string MobileNumber, int? id)
        {
            bool isMobileNumberExists = false;
            if (id == null)
            {
                isMobileNumberExists = employeeMasterBAL.GetEmployeeMasters().Any(s => s.MobileNumber == MobileNumber);
            }
            else
            {
                isMobileNumberExists = employeeMasterBAL.GetEmployeeMasters().Any(s => s.Row_Id == id.GetValueOrDefault() 
                && s.MobileNumber == MobileNumber);
            }

            return Json(!isMobileNumberExists, JsonRequestBehavior.AllowGet);
        }
        //// GET: EmployeeMasters/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EmployeeMaster employeeMaster = db.EmployeeMasters.Find(id);
        //    if (employeeMaster == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CityId = new SelectList(db.Cities, "Row_Id", "CityName", employeeMaster.CityId);
        //    ViewBag.CountryId = new SelectList(db.Countries, "Row_Id", "CountryName", employeeMaster.CountryId);
        //    ViewBag.StateId = new SelectList(db.States, "Row_Id", "StateName", employeeMaster.StateId);
        //    return View(employeeMaster);
        //}

        //// POST: EmployeeMasters/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Row_Id,EmployeeCode,FirstName,LastName,CountryId,StateId,CityId,EmailAddress,MobileNumber,PanNumber,PassportNumber,ProfileImage,Gender,IsActive,DateOfBirth,DateOfJoinee,CreatedDate,UpdatedDate,IsDeleted,DeletedDate")] EmployeeMaster employeeMaster)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(employeeMaster).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CityId = new SelectList(db.Cities, "Row_Id", "CityName", employeeMaster.CityId);
        //    ViewBag.CountryId = new SelectList(db.Countries, "Row_Id", "CountryName", employeeMaster.CountryId);
        //    ViewBag.StateId = new SelectList(db.States, "Row_Id", "StateName", employeeMaster.StateId);
        //    return View(employeeMaster);
        //}

        //// GET: EmployeeMasters/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EmployeeMaster employeeMaster = db.EmployeeMasters.Find(id);
        //    if (employeeMaster == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employeeMaster);
        //}

        //// POST: EmployeeMasters/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    EmployeeMaster employeeMaster = db.EmployeeMasters.Find(id);
        //    db.EmployeeMasters.Remove(employeeMaster);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
