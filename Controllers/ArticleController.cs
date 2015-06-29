using System;
using System.Collections.Generic;
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
    public class ArticleController : BaseController
    {
        private  ApplicationDbContext db = new ApplicationDbContext();

        // GET: Article
        [AllowAnonymous]
        public ActionResult Index()
        {
          
            return View(db.Articles.ToList());
        }

        // GET: Article/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel article = db.Articles.Find(id);
            List<CommentModel> commentsOfArticle = db.Comments.Where(comment => comment.Article_ArticleID == article.ArticleID).ToList();
            ArticlePageWithCommentsVM articlePage = new ArticlePageWithCommentsVM();
            articlePage.Article = article;
            articlePage.CommentsOfArticle = commentsOfArticle;
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(articlePage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(AddCommentVievModel addCommentVM)
        {
            if (ModelState.IsValid)
            {
                // addCommentVM. articleID???
                addCommentVM.Comment.User_Id = User.Identity.GetUserId();
                addCommentVM.Comment.CommentDateTime = DateTime.Now;
                db.Comments.Add(addCommentVM.Comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Article", articlePage.Comment.Article_ArticleID);
            }

            return View(articlePage);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,ArticleTitle,ArticleText")] ArticleModel articleModel)
        {
            if (ModelState.IsValid)
            {
                articleModel.User_Id = User.Identity.GetUserId();
                articleModel.ArticleDateTime = DateTime.Now;
                db.Articles.Add(articleModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articleModel);
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            EditArticleViewModel editArticleViewModel = new EditArticleViewModel()
            {
                ArticleDate = article.ArticleDateTime,
                ArticleTime = article.ArticleDateTime,
                ArticleText = article.ArticleText,
                ArticleTitle = article.ArticleTitle
            };
            
            return View(editArticleViewModel);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditArticleViewModel editArticleViewModel)
        {
            if (ModelState.IsValid)
            {
                ArticleModel article = db.Articles.Find(editArticleViewModel.Id);

                var newDateTime = new DateTime(
                    editArticleViewModel.ArticleDate.Year,
                    editArticleViewModel.ArticleDate.Month,
                    editArticleViewModel.ArticleDate.Day,
                    editArticleViewModel.ArticleTime.Hour,
                    editArticleViewModel.ArticleTime.Minute,
                    editArticleViewModel.ArticleTime.Second
                    );

                article.ArticleDateTime = newDateTime;
                article.ArticleText = editArticleViewModel.ArticleText;
                article.ArticleTitle = editArticleViewModel.ArticleTitle;

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editArticleViewModel);
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel articleModel = db.Articles.Find(id);
            if (articleModel == null)
            {
                return HttpNotFound();
            }
            return View(articleModel);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleModel articleModel = db.Articles.Find(id);
            db.Articles.Remove(articleModel);
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