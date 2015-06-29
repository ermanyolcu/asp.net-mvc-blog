using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class EditCommentVievModel
    {
        public int Id { get; set; }
        
        public string CommentText { get; set; }
    }
}
