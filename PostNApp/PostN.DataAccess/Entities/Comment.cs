using System;
using System.Collections.Generic;

#nullable disable

namespace PostN.DataAccess.Entities
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime Created { get; set; }
        public string CommentBody { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
