﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;

    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.CTDHs = new HashSet<CTDH>();
        }
    
        public int MaSP { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm!")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được quá 200 kí tự!")]
        public string TenSP { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập dòng sản phẩm!")]
        [StringLength(100, ErrorMessage = "Dòng sản phẩm không được quá 100 kí tự!")]
        public string DongSP { get; set; }
        public int MaHangSP { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập thông tin chi tiết sản phẩm!")]
        public string ThongTinChiTietSP { get; set; }
        public string HinhAnhSP { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập trạng thái sản phẩm!")]
        [StringLength(50, ErrorMessage = "Trạng thái không được quá 100 kí tự!")]
        public string TrangThaiSP { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập số lượng sản phẩm!")]
        public int SL { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập giá gốc sản phẩm!")]
        public double DonGiaGoc { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập giá khuyến mãi sản phẩm!")]
        public double DonGiaKM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDH> CTDHs { get; set; }
        public virtual HangSP HangSP { get; set; }
    }
}
