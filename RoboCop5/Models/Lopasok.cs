//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoboCop5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lopasok
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lopasok()
        {
            this.Targyak = new HashSet<Targyak>();
        }
    
        public int LID { get; set; }
        public string LIKTATO { get; set; }
        public string LCIM { get; set; }
        public System.DateTime LIDO { get; set; }
        public int LIRSZ { get; set; }
        public int LMID { get; set; }
        public int LVAROS { get; set; }
        public string LKOZNEV { get; set; }
        public int LKOZTIP { get; set; }
        public int LHSZ { get; set; }
        public string LESZKOZ { get; set; }
        public string LTENYALL { get; set; }
        public Nullable<int> LUID { get; set; }
        public Nullable<System.DateTime> LROGZ { get; set; }
        public Nullable<int> LBESZID { get; set; }
    
        public virtual KozteruletTipusok KozteruletTipusok { get; set; }
        public virtual Szemelyek Szemelyek { get; set; }
        public virtual Megyek Megyek { get; set; }
        public virtual Users Users { get; set; }
        public virtual Varosok Varosok { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Targyak> Targyak { get; set; }
    }
}