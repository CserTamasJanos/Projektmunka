using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoboCop5.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RoboCop5.Controllers
{
    public class NavController : Controller
    {
        private RoboCopEntities db = new RoboCopEntities();
        private string secretStringForToken = "AVeryBigSecretKey";
        // GET: Nav
        public ActionResult Navbar()
        {
            Navbar n = new Navbar();
            //var cookie = HttpContext.Request.Cookies.Get("token").Value;
            //HttpCookie cookie =  ;


            string passCookie = TempData["passCookie"] as string;
            HttpCookie nameCookie = Request.Cookies[passCookie];

            //If Cookie exists fetch its value.
            string name = nameCookie != null ? nameCookie.Value.Split('=')[0] : "undefined";

            //TempData["Message"] = name;

            //return RedirectToAction("Index");

            n.UsersMenus = db.UsersMenus(name).ToList();
            return PartialView(n);
            //RoboCopEntities r = new RoboCopEntities();
            //Navbar n = new Navbar();

            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjIyNiIsIm5iZiI6MTY1MTU5MTg3MCwiZXhwIjoxNjUxNTkxOTMwLCJpYXQiOjE2NTE1OTE4NzB9.z2ixPi97FYmhR4w_eelmQMOLLUDcdEOBT4ITfEnasQs";
            //n.UsersMenus = r.UsersMenus(token).ToList();
            //HttpContext.Request.Cookies.Get(token);
            //return RedirectToAction("Riport", "CaseRiport");
            //ViewData["Menu"] = n;
        }

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretStringForToken);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }


    }
}