﻿using PagedList;
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

        public ActionResult QT_SanPham(string keyword, int? page)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/QT_DangNhap/Index");
            }
            db = new CT25Team24Entities();
            List<SanPham> listSP = db.SanPhams.ToList();
            return View(db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) || keyword==null).ToList().ToPagedList(page ?? 1, 4));
        }

        public ActionResult Search(string keyword, int? page)
        {           
            db = new CT25Team24Entities();
            List<SanPham> listSP = db.SanPhams.ToList();          
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


            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (file.ContentLength <= 10000000)
                {
                    db.SanPhams.Add(sanpham);

                    if (db.SaveChanges() > 0)
                    {
                        file.SaveAs(path);
                        ViewBag.msg = "sanpham Added";
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
            return RedirectToAction("QT_SanPham","SanPhams");
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
                        return RedirectToAction("QT_SanPham", "SanPhams");
                    }

                }
            }
            return View();
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
            string currentImg = Request.MapPath(sanpham.HinhAnhSP);
            db.Entry(sanpham).State = EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(currentImg))
                {
                    System.IO.File.Delete(currentImg);
                }
                TempData["msg"] = "Data Deleted";
                return RedirectToAction("QT_SanPham");
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
