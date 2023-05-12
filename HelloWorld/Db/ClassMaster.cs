using System;
using System.Collections.Generic;

namespace HelloWorld.Db
{
    public partial class ClassMaster
    {
        public ClassMaster()
        {
            Users = new HashSet<User>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public string ClassDescription { get; set; } = null!;
        public double ClassPrice { get; set; }
        public int ClassSessions { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
