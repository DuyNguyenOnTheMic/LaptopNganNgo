//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.CTDHs = new HashSet<CTDH>();
        }
    
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string DongSP { get; set; }
        public int MaHangSP { get; set; }
        public string ThongTinChiTietSP { get; set; }
        public byte[] HinhAnhSP { get; set; }
        public string TrangThaiSP { get; set; }
        public int SL { get; set; }
        public double DonGiaGoc { get; set; }
        public double DonGiaKM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDH> CTDHs { get; set; }
        public virtual HangSP HangSP { get; set; }
    }
}
