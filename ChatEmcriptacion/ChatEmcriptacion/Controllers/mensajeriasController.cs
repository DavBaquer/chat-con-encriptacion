using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatEmcriptacion;
using ChatEmcriptacion.Models;

namespace ChatEmcriptacion.Controllers
{
    [Authorize]
    public class mensajeriasController : Controller
    {
        private chatEntities db = new chatEntities();

        // GET: mensajerias
        public ActionResult Index(usuario us)
        {
            ViewBag.correo = us.correo;
            return View();
        }


        //get guardar mensajes
        public JsonResult GuardarMensaje(string textmessage) {
            var en =new encriptacion();
            var key = "#$%&/!°678gds*´{ñ+";
            string keyEncript = "";
            var _message = new mensajeria();
            _message.mensaje =en.Encript3DES( textmessage,key);
            usuario usu= (usuario)Session["usuarioL"];

            _message.idemisor = usu.idusuario; 
            _message.fecha = DateTime.Now;
            db.mensajeria.Add(_message);


            db.SaveChanges();

          

            keyEncript = en.EncriptacionRSA(key);

            return Json(keyEncript, JsonRequestBehavior.AllowGet);


        }

        // GET: mensajerias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensajeria mensajeria = db.mensajeria.Find(id);
            if (mensajeria == null)
            {
                return HttpNotFound();
            }
            return View(mensajeria);
        }

        // GET: mensajerias/Create
        public ActionResult Create()
        {
            ViewBag.idemisor = new SelectList(db.usuario, "idusuario", "nombres");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idmensaje,idemisor,mensaje,fecha")] mensajeria mensajeria)
        {
            if (ModelState.IsValid)
            {
                db.mensajeria.Add(mensajeria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idemisor = new SelectList(db.usuario, "idusuario", "nombres", mensajeria.idemisor);
            return View(mensajeria);
        }

        // GET: mensajerias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensajeria mensajeria = db.mensajeria.Find(id);
            if (mensajeria == null)
            {
                return HttpNotFound();
            }
            ViewBag.idemisor = new SelectList(db.usuario, "idusuario", "nombres", mensajeria.idemisor);
            return View(mensajeria);
        }

        // POST: mensajerias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idmensaje,idemisor,mensaje,fecha")] mensajeria mensajeria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensajeria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idemisor = new SelectList(db.usuario, "idusuario", "nombres", mensajeria.idemisor);
            return View(mensajeria);
        }

        // GET: mensajerias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensajeria mensajeria = db.mensajeria.Find(id);
            if (mensajeria == null)
            {
                return HttpNotFound();
            }
            return View(mensajeria);
        }

        // POST: mensajerias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mensajeria mensajeria = db.mensajeria.Find(id);
            db.mensajeria.Remove(mensajeria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
