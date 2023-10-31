using ShipmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShipmentManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {

            using (CourierEntities data = new CourierEntities())
            {
                var obj = data.Users.Where(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password)).FirstOrDefault();
                if (obj != null)
                {

                    Session["UserID"] = obj.UserId.ToString();
                    Session["Username"] = obj.UserName.ToString();
                    //Session["UserType"] = obj.UserType.ToString();
                    if (obj.UserType.ToString() == "Admin")

                        return RedirectToAction("Index", "ShipmentInfoes");

                    else if (obj.UserType.ToString() == "Customer")

                        return RedirectToAction("Index", "Customer");



                }
                else
                {
                    ViewBag.Error = "Invalid User Name or Password";

                    // return View();
                }

                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}