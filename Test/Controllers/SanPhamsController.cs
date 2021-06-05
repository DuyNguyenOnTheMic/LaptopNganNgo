using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Index(int? page)
        {         
            if (page == null) page = 1;
            var links = (from l in db.SanPhams
                         select l).OrderBy(x => x.MaSP);

            int pageSize = 6;
          
            int pageNumber = (page ?? 1);

            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult QT_SanPham(string keyword, int? page)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            }
            db = new CT25Team24Entities();
            var searchSP = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) || keyword == null).ToList().OrderByDescending(x => x.MaSP).ToPagedList(page ?? 1, 4);
            if (searchSP.Count() == 0)
            {
                return RedirectToAction("QT_SPNotFound");
            }
            return View(searchSP);
        }

        public ActionResult QT_SPNotFound()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            }
            return View();
        }

        public ActionResult Search(string keyword, int? page)
        {           
            db = new CT25Team24Entities();
            ViewBag.keyword = keyword;
            bool notfound = db.SanPhams.Any(x => x.TenSP.ToLower().Contains(keyword.ToLower()));
            if (!notfound)
            {
                return View("Search_NotFound");
            }           
            return View(db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) || keyword == null).ToList().ToPagedList(page ?? 1, 6));
        }

        public ActionResult Search_NotFound()
        {
            return View();
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
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            }
            ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang");
            return View();
        }


        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, SanPham sanpham)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;

            string extension = Path.GetExtension(file.FileName);

            string path = Path.Combine(Server.MapPath("~/images/"), _filename);

            sanpham.HinhAnhSP = "~/images/" + _filename;

            if (!ModelState.IsValid)
            {
                ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang");
                return View(sanpham);
            }
            if (ModelState.IsValid)
            {
                ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang");

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (file.ContentLength <= 4000000)
                    {
                        db.SanPhams.Add(sanpham);

                        if (db.SaveChanges() > 0)
                        {
                            file.SaveAs(path);
                            ViewBag.msg = "sanpham Added";
                            ModelState.Clear();
                            return RedirectToAction("QT_SanPham", "SanPhams");
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Hình ảnh phải lớn hơn hoặc bằng 4MB!";
                    }
                }
                else
                {
                    ViewBag.msg = "Định dạng file không hợp lệ!";
                }
            }
            return View(sanpham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            } else
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            Session["imgPath"] = sanPham.HinhAnhSP;
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang", sanPham.MaHangSP);
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, SanPham sanpham)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang");
                return View(sanpham);
            }
            if (ModelState.IsValid)
            {
                ViewBag.MaHangSP = new SelectList(db.HangSPs, "MaHang", "TenHang");
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;

                    string extension = Path.GetExtension(file.FileName);

                    string path = Path.Combine(Server.MapPath("~/images/"), _filename);

                    sanpham.HinhAnhSP = "~/images/" + _filename;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 4000000)
                        {
                            db.Entry(sanpham).State = EntityState.Modified;
                            string oldImgPath = Request.MapPath(Session["imgPath"].ToString());

                            if (db.SaveChanges() > 0)
                            {
                                file.SaveAs(path);
                                if (System.IO.File.Exists(oldImgPath))
                                {
                                    System.IO.File.Delete(oldImgPath);
                                }
                                return RedirectToAction("QT_SanPham");
                            }
                        }
                        else
                        {
                            ViewBag.msg = "Hình ảnh phải lớn hơn hoặc bằng 4MB!";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Định dạng file không hợp lệ!";
                    }
                }
                else
                {
                    sanpham.HinhAnhSP = Session["imgPath"].ToString();
                    db.Entry(sanpham).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        return RedirectToAction("QT_SanPham", "SanPhams");
                    }

                }
            }
            return View(sanpham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            } else
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sanpham = db.SanPhams.Find(id);

            if (sanpham == null)
            {
                return HttpNotFound();
            }
            try
            {
                string currentImg = Request.MapPath(sanpham.HinhAnhSP);
                db.Entry(sanpham).State = EntityState.Deleted;
                if (db.SaveChanges() > 0)
                {
                    if (System.IO.File.Exists(currentImg))
                    {
                        System.IO.File.Delete(currentImg);
                    }
                    return RedirectToAction("QT_SanPham");
                }
            }
            catch (Exception)
            {
                TempData["msg"] = String.Format("Bạn không thể xoá sản phẩm đã có trong đơn hàng!");
                return RedirectToAction("Edit", new { id = sanpham.MaSP });
            }          

            return View();
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("QT_SanPham");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }   
    }
}
