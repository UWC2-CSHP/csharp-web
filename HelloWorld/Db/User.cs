using System;
using System.Collections.Generic;

namespace HelloWorld.Db
{
    public partial class User
    {
        public User()
        {
            Classes = new HashSet<ClassMaster>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;

        public virtual ICollection<ClassMaster> Classes { get; set; }
    }
}
