using System;
using System.Collections.Generic;

namespace HelloWorld.Db
{
    public partial class View1
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public string ClassDescription { get; set; } = null!;
        public double ClassPrice { get; set; }
        public int ClassSessions { get; set; }
        public int UserId { get; set; }
        public int Expr1 { get; set; }
        public int Expr2 { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
    }
}
