using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    [Table("Articles")]
    public class ArticleModel
    {
        [Key]
        public int ArticleID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Created")]
        public DateTime ArticleDateTime { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string ArticleTitle { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string ArticleText { get; set; }
        
        
        //NAVIGATION PROPERTIES
        
        public string User_Id { get; set; }

        [ForeignKey("User_Id")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CommentModel> Comments { get; set; }

    }
}
