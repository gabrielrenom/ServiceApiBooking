using ACP.Business;
using ACP.Business.Exceptions;
using ACP.Business.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceAPI.Controllers.Administration
{    
    public class UserAdminController : Controller
    {
        private UserController _userController;

        public UserAdminController(UserController userController)
        {
            _userController = userController;
        }
            // GET: UserAdmin
        public async Task<ActionResult> Index()
        {
            List<UserModel> users = new List<UserModel>();

            try
            {
                _userController.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _userController.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();

                var result = await _userController.GetAll();

                result.TryGetContentValue(out users);
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return View(users);
        }

        // GET: UserAdmin/Details/5
        public async Task<ActionResult> Details(int id)
        {
            UserModel user = new UserModel();

            try
            {
                _userController.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _userController.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _userController.GetById(id);

                result.TryGetContentValue(out user);

                // await FillDropBoxes();

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return View(user);


        }

        // GET: UserAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userController.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _userController.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();

                    var result = await _userController.Add(user);

                    result.TryGetContentValue(out user);

                    if (user != null) return RedirectToAction("Index");
                    else
                        return View();
                }
                // TODO: Add insert logic here

                    
            }
            catch
            {
                return View();
            }

            return View();
        }

        // GET: UserAdmin/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
           UserModel user = new UserModel();

            try
            {
                _userController.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _userController.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _userController.GetById(id);

                result.TryGetContentValue(out user);

               // await FillDropBoxes();

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return View(user);

        }

        // POST: UserAdmin/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(UserModel model)
        {
            UserModel user = new UserModel();
            bool updateresult = false;

            try
            {
                _userController.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _userController.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _userController.GetById(model.Id);

                result.TryGetContentValue(out user);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Modified = DateTime.Now;
                user.Created = DateTime.Now;
                user.ModifiedBy = model.Email;
                user.DOB = model.DOB;
                user.Email = model.Email;
                user.Gender = model.Gender;
                user.Password = model.Password;
                user.PhoneNumber = model.PhoneNumber;
                user.Address.Modified = DateTime.Now;
                user.Address.Created = DateTime.Now;
                user.Address.ModifiedBy = model.Email;
                user.Address.Number = model.Address.Number;
                user.Address.Postcode = model.Address.Postcode;
                user.Address.Address1 = model.Address.Address1;
                user.Address.Address2 = model.Address.Address2;
                user.Address.City = model.Address.City;
                user.Address.Country = model.Address.Country;
                user.Address.County = model.Address.County;

                var resultupdated = await _userController.Update(user);

                resultupdated.TryGetContentValue(out updateresult);

                if (updateresult)
                {
                    return RedirectToAction("Index");
                }
                // await FillDropBoxes();

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return View();
        }

        // GET: UserAdmin/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            UserModel user = new UserModel();

            try
            {
                _userController.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _userController.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _userController.GetById(id);

                result.TryGetContentValue(out user);                

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return View(user);


        }

        // POST: UserAdmin/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            bool resutdelete = false;
            try
            {
                _userController.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _userController.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _userController.Delete(id);

                result.TryGetContentValue(out resutdelete);

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            if (resutdelete) return RedirectToAction("Index");
            else return View();

        }
    }
}
