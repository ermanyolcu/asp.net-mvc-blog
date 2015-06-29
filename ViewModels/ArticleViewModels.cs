using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string ArticleTitle { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string ArticleText { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ArticleDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Time Created")]
        public DateTime ArticleTime { get; set; }
    }

    public class ArticlePageWithCommentsVM
    {
        public ArticleModel Article { get; set; }

        //  public CommentModel Comment { get; set; }

        public List<CommentModel> CommentsOfArticle { get; set; }
    }
}