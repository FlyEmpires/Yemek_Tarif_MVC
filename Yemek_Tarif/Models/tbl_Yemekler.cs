//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yemek_Tarif.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Yemekler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Yemekler()
        {
            this.tbl_Yorumlar = new HashSet<tbl_Yorumlar>();
        }
    
        public short Yemekid { get; set; }
        public string YemekAd { get; set; }
        public string YemekMalzeme { get; set; }
        public string YemekTarif { get; set; }
        public string YemekResim { get; set; }
        public Nullable<System.DateTime> YemekTarih { get; set; }
        public Nullable<byte> YemekPuan { get; set; }
        public Nullable<short> Kategoriid { get; set; }
        public string YemekIcerik { get; set; }
        public Nullable<bool> YemekDurum { get; set; }
        public string KullanıcıAdı { get; set; }
    
        public virtual tbl_Kategoriler tbl_Kategoriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Yorumlar> tbl_Yorumlar { get; set; }
    }
}
