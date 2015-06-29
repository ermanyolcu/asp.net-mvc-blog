using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comment
        [AllowAnonymous]
        public ActionResult Index()
        {
            var commentModels = db.Comments.Include(c => c.Article).Include(c => c.User);
            return View(commentModels.ToList());
        }

        // GET: Comment/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentModel = db.Comments.Find(id);
            if (commentModel == null)
            {
                return HttpNotFound();
            }
            return View(commentModel);
        }

        // GET: Comment/Create
        public ActionResult Create(int articleID)
        {
            var newComment = new CommentModel();
            newComment.Article_ArticleID = articleID;
            //ViewBag.Article_ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleTitle");
            //ViewBag.User_Id = new SelectList(db.Users, "Id", "Email");
            return View(newComment);
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,Article_ArticleID,CommentText,User_Id")] CommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                commentModel.User_Id = User.Identity.GetUserId();
                commentModel.CommentDateTime = DateTime.Now;
                db.Comments.Add(commentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Article_ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleTitle", commentModel.Article_ArticleID);
            //ViewBag.User_Id = new SelectList(db.Users, "Id", "Email", commentModel.User_Id);
            return View(commentModel);
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel comment = db.Comments.Find(id);
          
            if (comment == null)
            {
                return HttpNotFound();
            }

            EditCommentVievModel viewModel = new EditCommentVievModel()
            {
                CommentText = comment.CommentText,
                Id = comment.CommentID
            };
            //ViewBag.Article_ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleTitle", commentModel.Article_ArticleID);
            //ViewBag.User_Id = new SelectList(db.Users, "Id", "Email", commentModel.User_Id);
            return View(viewModel);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCommentVievModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                CommentModel comment = db.Comments.Find(viewModel.Id);
                
                comment.CommentText = viewModel.CommentText;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Article_ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleTitle", commentModel.Article_ArticleID);
            //ViewBag.User_Id = new SelectList(db.Users, "Id", "Email", commentModel.User_Id);
            return View(viewModel);
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentModel = db.Comments.Find(id);
            if (commentModel == null)
            {
                return HttpNotFound();
            }
            return View(commentModel);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentModel = db.Comments.Find(id);
            db.Comments.Remove(commentModel);
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
