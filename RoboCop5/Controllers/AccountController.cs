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
    public class AccountController : Controller
    {
        private RoboCopEntities db = new RoboCopEntities();
        private string secretStringForToken = "AVeryBigSecretKey";
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login (string UEMAIL, string UCODE )
        {
            string decoded = CreateMD5Hash(UCODE);
            Users user = db.Users.FirstOrDefault(u => u.UEMAIL == UEMAIL && u.UCODE == decoded);
            if (user != null)
            {
                string token = CipherMaker(user.UID);

                HttpCookie writeCookie = new HttpCookie(UEMAIL,token);
                Response.Cookies.Add(writeCookie);
                HttpCookie getCookie = Request.Cookies[UEMAIL];
                string saveCookie = getCookie != null ? getCookie.Value.Split('=')[0] : "undefined";
                db.SetUserCipher(UEMAIL, saveCookie, new System.Data.Entity.Core.Objects.ObjectParameter("successCode", 0));
                db.SaveChanges();

                Response.Cookies.Set(getCookie);
                TempData["passCookie"] = UEMAIL;

                return RedirectToAction("Riport","CaseRiport");
            }
            else
            {
                //Error
                return RedirectToAction("Login");
            }

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register (string UEMAIL, string UCODE)
        {
            string coded = CreateMD5Hash(UCODE);
            int success = db.RegisterUser(UEMAIL,coded, new System.Data.Entity.Core.Objects.ObjectParameter("successCode", 0));

            if(success <= 0)
            {
                //YouAreLoggedin
                return RedirectToAction("Login");
            }
            if(success == 1)
            {
                //Email_Already_Exists
                return RedirectToAction("Register", "Account");
            }
            if(success == 2)
            {
                //Other Error
                return RedirectToAction("Register", "Account");
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }        
        }


        public ActionResult LogOff ()
        {
            return View();
        }
        public string CipherMaker(int UID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretStringForToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", UID.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
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

        private string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}