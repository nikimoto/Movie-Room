using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoviesRoom.Models;
using MoviesRoom.Data;

namespace MoviesRoom.Controllers
{
    [Authorize(Roles = "admin")]
    public class CommentController : BaseController
    {
        public CommentController()
            : base(new UowData())
        {
        }

        public CommentController(IUowData data)
            : base(data)
        {
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = this.Data.Comments.GetById(id.GetValueOrDefault());

            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                this.Data.Comments.Update(comment);
                this.Data.SaveChanges();
                return RedirectToAction("SingleFilm", "Home", new { id = comment.FilmId });
            }

            return View(comment);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = this.Data.Comments.GetById(id.GetValueOrDefault());

            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = this.Data.Comments.GetById(id);

            this.Data.Comments.Delete(comment);
            this.Data.SaveChanges();

            return RedirectToAction("SingleFilm", "Home", new { id = comment.FilmId });
        }
    }
}
