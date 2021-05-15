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
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.HangSP);
            return View(sanPhams.ToList());
        }

        public ActionResult Search()
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


        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, SanPham sanpham)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;

            string extension = Path.GetExtension(file.FileName);

            string path = Path.Combine(Server.MapPath("~/images/"), _filename);

            sanpham.HinhAnhSP = "~/images/" + _filename;


            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (file.ContentLength <= 10000000)
                {
                    db.SanPhams.Add(sanpham);

                    if (db.SaveChanges() > 0)
                    {
                        file.SaveAs(path);
                        ViewBag.msg = "Employee Added";
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.msg = "File Size must be Equal or less than 10mb";
                }
            }
            else
            {
                ViewBag.msg = "Inavlid File Type";
            }
            return RedirectToAction("Index","QT_TrangChu");
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
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
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;

                    string extension = Path.GetExtension(file.FileName);

                    string path = Path.Combine(Server.MapPath("~/images/"), _filename);

                    sanpham.HinhAnhSP = "~/images/" + _filename;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 10000000)
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
                                TempData["msg"] = "Data Updated";
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            ViewBag.msg = "File Size must be Equal or less than 10mb";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Inavlid File Type";
                    }
                }
                else
                {
                    sanpham.HinhAnhSP = Session["imgPath"].ToString();
                    db.Entry(sanpham).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        TempData["msg"] = "Data Updated";
                        return RedirectToAction("Index", "QT_TrangChu");
                    }

                }
            }
            return View();
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = db.SanPhams.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            string currentImg = Request.MapPath(employee.HinhAnhSP);
            db.Entry(employee).State = EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(currentImg))
                {
                    System.IO.File.Delete(currentImg);
                }
                TempData["msg"] = "Data Deleted";
                return RedirectToAction("Index");
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
    }
}
