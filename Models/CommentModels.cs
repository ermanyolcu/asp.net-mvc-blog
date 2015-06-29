using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    [Table("Comments")]
    public class CommentModel
    {
        [Key]
        public int CommentID { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date Created")]
        public DateTime CommentDateTime { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment")]
        public string CommentText { get; set; }

        public CommentModel CommentParent { get; set; }



        //NAVIGATION PROPERTIES

        [ForeignKey("Article_ArticleID")]
        public virtual ArticleModel Article { get; set; }
        public int Article_ArticleID { get; set; }

        [ForeignKey("User_Id")]
        public virtual ApplicationUser User { get; set; }
        public string User_Id { get; set; }

    }
}
