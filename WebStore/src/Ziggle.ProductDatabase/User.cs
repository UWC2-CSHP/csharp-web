using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; } = null!;
        public string UserHashedPassword { get; set; } = null!;
        public bool UserFailedPasswordAttempts { get; set; }
        public bool UserLockedOut { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
