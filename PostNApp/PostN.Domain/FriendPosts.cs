using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostN.Domain
{
    public class FriendPosts
    {
        public FriendPosts() { }
        public FriendPosts(List<Post> posts) {
            Posts = posts;
        }
        public List<Post> Posts { get; set; }
    }
}
