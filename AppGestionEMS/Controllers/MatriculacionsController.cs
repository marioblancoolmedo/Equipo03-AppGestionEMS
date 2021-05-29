using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppGestionEMS.Models;

namespace AppGestionEMS.Controllers
{
    [Authorize(Roles = "P")]
    public class MatriculacionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Matriculacions
        public ActionResult Index()
        {
            var matriculacions = db.Matriculacions.Include(m => m.Curso).Include(m => m.Grupo).Include(m => m.User);
            return View(matriculacions.ToList());
        }

        // GET: Matriculacions/Create
        public ActionResult Create()
        {
            var alumnos = from user in db.Users
                             from u_r in user.Roles
                             join rol in db.Roles on u_r.RoleId equals rol.Id
                             where rol.Name == "A"
                             select user.UserName;
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Year");
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users.Where(u => alumnos.Contains(u.UserName)), "Id", "Name");
            return View();
        }

        // POST: Matriculacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,CursoId,GrupoId,Date")] Matriculacion matriculacion)
        {
            if (ModelState.IsValid)
            {
                db.Matriculacions.Add(matriculacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Year", matriculacion.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Name", matriculacion.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", matriculacion.UserId);
            return View(matriculacion);
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
