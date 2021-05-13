using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers.Website_QuanTri
{
    public class SanPhamsController : Controller
    {
        private CT25Team24Entities db = new CT25Team24Entities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.HangSP);
            return View(sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,DongSP,MaHangSP,ThongTinChiTietSP,HinhAnhSP,TrangThaiSP,SL,DonGiaGoc,DonGiaKM")] SanPham sanPham, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength>0) 
            {
                sanPham.HinhAnhSP = new byte[image.ContentLength];
                image.InputStream.Read(sanPham.HinhAnhSP, 0, image.ContentLength);
                string filename = System.IO.Path.GetFileName(image.FileName);
                string urlImage = Server.MapPath("~/Images/" + filename);
                image.SaveAs(urlImage);
                sanPham.URLHinhAnh = "~/Images/" + filename;

            }
                if (ModelState.IsValid)
                {
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang", sanPham.MaHangSP);
                return View(sanPham);
            }
        
        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang", sanPham.MaHangSP);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,DongSP,MaHangSP,ThongTinChiTietSP,HinhAnhSP,TrangThaiSP,SL,DonGiaGoc,DonGiaKM")] SanPham sanPham, HttpPostedFileBase editImage)

        {
            if (ModelState.IsValid)
            {
                SanPham modifyProduct = db.SanPhams.Find(sanPham.MaSP);
                if (modifyProduct != null)
                {
                    if (editImage != null && editImage.ContentLength > 0)
                    {
                        modifyProduct.HinhAnhSP = new byte[editImage.ContentLength];
                        editImage.InputStream.Read(modifyProduct.HinhAnhSP, 0, editImage.ContentLength);
                        string filename = System.IO.Path.GetFileName(editImage.FileName);
                        string urlImage = Server.MapPath("~/Images/" + filename);
                        editImage.SaveAs(urlImage);
                        modifyProduct.URLHinhAnh = "~/Images/" + filename;
                    }
                    }
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            
            ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang", sanPham.MaHangSP);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult AddImage()
        //{
          //  SanPham sanpham = new SanPham();
            //return View(sanpham);
        //}

        [HttpPost]
        //public ActionResult AddImage(SanPham model, HttpPostedFileBase image1)
        //{
          //  var db = new CT25Team24Entities();
            //if (image1!=null)
            //{
              //  model.HinhAnhSP = new byte[image1.ContentLength];
                //image1.InputStream.Read(model.HinhAnhSP, 0, image1.ContentLength);
            //}
            //db.SanPhams.Add(model);
          //db.SaveChanges();
            //return RedirectToAction("Index", "SanPhams");
        //}

        public ActionResult Index1()
        {
            CT25Team24Entities db = new CT25Team24Entities();
            var item = (from d in db.SanPhams
                        select d).ToList();
            return View(item);
        }
    }
}
