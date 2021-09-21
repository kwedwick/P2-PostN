using System;
using System.Collections.Generic;

#nullable disable

namespace PostN.DataAccess.Entities
{
    public partial class Follower
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserId2 { get; set; }
        public int FriendRequest { get; set; }

        public virtual User User { get; set; }
        public virtual User UserId2Navigation { get; set; }
    }
}
