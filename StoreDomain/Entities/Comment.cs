using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.StoreDomain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual Game Game { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int LikeCount { get; set; }

        //if comment edited by Admin

        public virtual Admin Admin { get; set; }
        public string NewText { get; set; }
        public DateTime NewTime { get; set; }

        public void Edit(Admin admin, string text, DateTime time)
        {
            Admin = admin;
            NewText = text;
            NewTime = time;
        }
    }
}
