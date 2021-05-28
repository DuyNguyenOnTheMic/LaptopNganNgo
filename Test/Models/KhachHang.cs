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
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            this.DonHangs = new HashSet<DonHang>();
        }

        public int MaKH { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập họ và tên!")]
        [StringLength(100, ErrorMessage = "Họ và tên không được quá 100 kí tự!")]
        [RegularExpression(@"[\p{L} ]+$", ErrorMessage = "Họ và tên chỉ được nhập chữ!")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn giới tính!")]
        public string GioiTinh { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại!")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Số điện thoại không đúng định dạng!")]
        public string DienThoai { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ!")]
        public string DiaChi { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Bạn chưa nhập ngày sinh!")]
        public Nullable<System.DateTime> NgaySinh { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail không đúng định dạng!")]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} kí tự!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string MatKhau { get; set; }
        [NotMapped] // Does not effect with your database
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không trùng!")]
        public string XacNhanMK { get; set; }
        public string VaiTro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
