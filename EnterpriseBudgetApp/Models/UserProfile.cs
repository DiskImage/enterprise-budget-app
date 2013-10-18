//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnterpriseBudgetApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Budgets = new HashSet<Budget>();
            this.Transactions = new HashSet<Transaction>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int UserBudget { get; set; }
        public string Name { get; set; }
        public Nullable<int> GroupId { get; set; }
    
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
