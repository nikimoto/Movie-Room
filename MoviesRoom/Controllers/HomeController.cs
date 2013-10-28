using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MoviesRoom.Data;
using MoviesRoom.ViewModels.Film;
using MoviesRoom.Models;
using System.ComponentModel;
using MoviesRoom.ViewModels.Comment;
using System.IO;
using System.Net;

namespace MoviesRoom.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
            : base(new UowData())
        {
        }

        public HomeController(IUowData data)
            : base(data)
        {
        }

        public ViewResultBase Index(string query)
        {
            if (Request.IsAjaxRequest())
            {
                var results = this.Data.Films.All()
                                  .Include(c => c.Category)
                                  .Where(f => f.Title.ToLower().Contains(query.ToLower()) && f.StartDate > DateTime.Now)
                                  .Select(FilmViewModel.FromFilm)
                                  .ToList();

                return PartialView("_FilmPartial", results);
            }

            var films = this.Data.Films.All()
                            .Include(c => c.Category)
                            .Where(f => f.StartDate > DateTime.Now)
                            .Select(FilmViewModel.FromFilm)
                            .OrderByDescending(f => f.StartDate).ToList();
            return View(films);
        }

        public ActionResult SingleFilm(int id)
        {
            var result = this.Data.Films.All().Include(c => c.Category)
                             .Where(film => film.Id == id)
                             .Select(FullFilmViewModel.FromFilm)
                             .FirstOrDefault();

            result.Comments = result.Comments.OrderByDescending(c => c.Id).ToList();

            return View(result);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("CustomError",
                    "Comments is Required!");
            }
            else
            {
                var entity = comment.CreateComment();
                entity.UserId = this.Data.Users.All()
                                    .Where(u => u.UserName == comment.Author)
                                    .Select(u => u.Id)
                                    .FirstOrDefault();

                this.Data.Comments.Add(entity);
                this.Data.SaveChanges();
            }

            var comments = this.Data.Comments.All()
                               .Where(c => c.FilmId == comment.FilmId)
                               .Include(u => u.User)
                               .OrderByDescending(c => c.Id)
                               .Select(CommentViewModel.FromComment)
                               .ToList();

            return PartialView("_CommentsPartial", comments);
        }

        [Authorize(Roles = "admin")]
        public ActionResult UploadPicture(int id, IEnumerable<HttpPostedFileBase> UploadPicture)
        {
            if (UploadPicture != null)
            {
                foreach (var file in UploadPicture)
                {
                    if (IsImage(file.ContentType))
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                        byte[] image = ReadStream(file.InputStream);
                        var film = this.Data.Films.GetById(id);
                        film.Image = image;
                        this.Data.Films.Update(film);
                        this.Data.SaveChanges();
                        return Content("");
                    }
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid type");
        }

        private bool IsImage(string contentType)
        {
            if (contentType == "image/jpeg" || contentType == "image/pjpeg" ||
                contentType == "image/png" || contentType == "image/bmp"
                || contentType == "image/x-windows-bmp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}