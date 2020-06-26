using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace ChatEmcriptacion.Controllers
{
    public class HomeController : Controller
    {
        private Models.encriptacion en = new Models.encriptacion();

        private chatEntities db = new chatEntities();
        public ActionResult Login(string message="")
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {

               password= en.Encrypt(password, "|#$¿¡+´{}¿¿owsoxk0%&/d<<");
                var user = db.usuario.FirstOrDefault(e => email.Equals(e.correo) && password.Equals(e.contrasenia));

             



                if (user != null)
                {
                    Session["usuarioL"] = user;
                    FormsAuthentication.SetAuthCookie(user.correo, true);
                    return RedirectToAction("Index", "mensajerias",user);

                }
                else
                {
                    return Login("el usuario y contraseña no son correctos");


                }


            }
            else {
                return Login("Llene los campos");
            }
           
            
        }

        [Authorize]
        public ActionResult Logout() {

            FormsAuthentication.SignOut();
            return Login("Sesion finalizada");
        }



       
    }
}