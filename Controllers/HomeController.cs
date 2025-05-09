﻿using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Manager;
using UmangMicro.Models;
using static System.Net.WebRequestMethods;
using static UmangMicro.Manager.Enums;

namespace UmangMicro.Controllers
{
    public class HomeController : Controller
    {
        UM_DBEntities db = new UM_DBEntities();
        public ActionResult Index()
        {
            var strurl= CommonModel.GetBaseUrl();
            if (strurl== "https://hbh.pcidigitals.in" || strurl== "https://hbh.pcidigitals.in/")
            {
                return RedirectToAction("Login","Account");
            }
           else if (strurl == "https://hbhtest.pcidigitals.in" || strurl == "https://hbhtest.pcidigitals.in/")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult AnimationFilm()
        {
            return View();
        }

        public ActionResult WomenLeadership()
        {
            return View();
        }

        public ActionResult WhyGoddaAndJamtara()
        {
            return View();
        }

        public ActionResult EvaluationStudy()
        {
            return View();
        }

        public ActionResult CEFMIssue()
        {
            return View();
        }

        public ActionResult StatusOfGirl()
        {
            return View();
        }

        public ActionResult CaseStudyList(string CategId = "")
        {
            CategId = !string.IsNullOrWhiteSpace(CategId) ? CategId : "0";
            ViewBag.CategId = CategId;
            DataSet ds= SP_Model.GetMC_CaseStudyList(CategId);
            if (ds.Tables.Count > 0)
            {
                return View(ds);
            }
            return View(ds);
        }

        public ActionResult KishoriHelpDesk()
        {
            return View();
        }

        public ActionResult StatusOfGirlInJharkhand()
        {
            return View();
        }

        public ActionResult Modules()
        {
            return View();
        }

        public ActionResult Testimonials()
        {
            return View();
        }
        public ActionResult AimObjectives()
        {
            return View();
        }

        public ActionResult Design()
        {
            return View();
        }

        public ActionResult Intervention()
        {
            return View();
        }

        public ActionResult Journeysofar()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #region Home - Resource Module
        [HttpPost]
        public JsonResult Resource(ResourceModel model)
        {
            JsonResponseData response = new JsonResponseData();
            if (!ModelState.IsValid)
            {
                if (model.ResourceDownLoad.Count == 0)
                {
                    ModelState.AddModelError("", "Please select at-least one Resource Module.");
                }
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Please select at-least one Resource Module.", Data = null };
                var resResponse2 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse2.MaxJsonLength = int.MaxValue;
                return resResponse2;
            }
            try
            {
                tbl_Resource tbl = new tbl_Resource();
                tbl.Name = model.Name;
                tbl.Designation = model.Designation;
                tbl.Designation_Other = model.Designation_Other;
                tbl.Organisation = model.Organisation;
                tbl.Email = model.Email;
                tbl.ContactNo = model.ContactNo;
                tbl.Age = model.Age;
                tbl.State = model.State;
                tbl.District = model.District;
                tbl.ResourceDownLoad = string.Join(",", model.ResourceDownLoad);
                tbl.IsActive = true;
                tbl.CreatedOn = DateTime.Now;
                db.tbl_Resource.Add(tbl);
                int res = db.SaveChanges();

                var RD = string.Join(",", model.ResourceDownLoad);
                if (res > 0)
                {
                    ViewBag.RD = model.ResourceDownLoad;
                    ModelState.Clear();
                    model = new ResourceModel();
                    ModelState.AddModelError("", "Record Save Successfully....");
                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Submitted Successfully.", Data = null };
                    var resResponse2 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse2.MaxJsonLength = int.MaxValue;
                    return resResponse2;
                }

                //MultipleFileDownload obj = new MultipleFileDownload();
                //////int CurrentFileID = Convert.ToInt32(FileID);  
                //if (model.ResourceDownLoad.Count != 0 && model.ResourceDownLoad != null)
                //{
                //    List<FileInfo> listFiles = new List<FileInfo>();
                //    foreach (var item in model.ResourceDownLoad)
                //    {
                //        listFiles.Add(new FileInfo()
                //        {
                //            FileName = item.ToString() + ".pdf",
                //        });
                //    }
                //    if (listFiles != null)
                //    {
                //        var filesCol = MultipleFileDownload.GetFile("~/ResourceFile", listFiles).ToList();
                //        using (var memoryStream = new MemoryStream())
                //        {
                //            using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                //            {
                //                for (int i = 0; i < filesCol.Count; i++)
                //                {
                //                    ziparchive.CreateEntryFromFile(filesCol[i].FilePath, filesCol[i].FileName);
                //                }
                //            }
                //            ModelState.Clear();
                //            model = new ResourceModel();
                //            ModelState.AddModelError("", "Resource File Downloaded....");
                //            return File(memoryStream.ToArray(), "application/zip", "ResourceFile.zip");
                //        }
                //    }
                //}
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;

            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Submit Successfully.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
            // return View(model);
        }
        #endregion
        #region Home - Case Studies Module
        //  [HttpGet]
        public ActionResult CaseStudy()
        {
            //DataSet ds = SP_Model.GetSP_MicroCaseList(1);
            //DataTable dt = new DataTable();
            //if (ds.Tables.Count > 0)
            //{
            //    dt = ds.Tables[0];
            //}
            DataTable dt = SP_Model.GetSP_MicroCase_Summary();
            if (dt.Rows.Count > 0)
            {
                return View(dt);
            }
            return View(dt);
        }
        public ActionResult GetCaseList(int CategId)
        {
            JsonResponseData response = new JsonResponseData();
            try
            {
                DataSet ds = SP_Model.GetSP_MicroCaseList(2, CategId);
                DataTable dt = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                    var res = JsonConvert.SerializeObject(dt);
                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "", Data = res };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                }
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "", Data = null };
                var resResponse2 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse2.MaxJsonLength = int.MaxValue;
                return resResponse2;
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
        }
        // [ActionName("{AN}")]
        public ActionResult CaseDetail(int Id = 0, string AN = "")
        {
            ViewBag.Id=Id; ViewBag.AN=AN;   
            return View();
        }
        [HttpPost]
        public ActionResult CaseDetaildata(int Id = 0, string AN = "")
        {
            DataSet ds = SP_Model.GetSP_MicroCaseList(3, Id);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return PartialView("_CaseDetailView", dt);
        }

        #endregion

        [AllowAnonymous]
        public ActionResult ResourcesFile(string dt, resourcefilter rf)
        {
            UM_DBEntities _db = new UM_DBEntities();
            ViewBag.doctype = rf.doctype;

            if (!string.IsNullOrWhiteSpace(rf.doctype) && (string.IsNullOrWhiteSpace(rf.Subject) && rf.IssueDate==null))
            {
                var x = _db.Tbl_FileResource.Where(m => m.DocumentType.Contains(rf.doctype)).ToList();
                return View(x);
            }
            if (!string.IsNullOrWhiteSpace(rf.Subject) && (string.IsNullOrWhiteSpace(rf.doctype) && rf.IssueDate == null))
            {
                var x = _db.Tbl_FileResource.Where(m => m.Subject.Contains(rf.Subject)).ToList();
                return View(x);
            }
            if (rf.IssueDate!=null && (string.IsNullOrWhiteSpace(rf.doctype) && string.IsNullOrWhiteSpace(rf.Subject)))
            {
                var x = _db.Tbl_FileResource.Where(m => m.DateofIssue == rf.IssueDate ).ToList();
                return View(x);
            }
            else if (!string.IsNullOrWhiteSpace(rf.doctype) && !string.IsNullOrWhiteSpace(rf.Subject) && rf.IssueDate == null)
            {
                var x = _db.Tbl_FileResource.Where(m => m.DocumentType == rf.doctype && m.Subject == rf.Subject).ToList();
                return View(x);
            }
            else if (!string.IsNullOrWhiteSpace(rf.doctype) && string.IsNullOrWhiteSpace(rf.Subject) && rf.IssueDate != null)
            {
                var x = _db.Tbl_FileResource.Where(m => m.DocumentType == rf.doctype && rf.IssueDate != null).ToList();
                return View(x);
            }
            else if (string.IsNullOrWhiteSpace(rf.doctype) && string.IsNullOrWhiteSpace(rf.Subject) && rf.IssueDate != null)
            {
                var x = _db.Tbl_FileResource.Where(m => m.Subject == rf.Subject && rf.IssueDate != null).ToList();
                return View(x);
            }
            else if (!string.IsNullOrWhiteSpace(rf.doctype) && !string.IsNullOrWhiteSpace(rf.Subject) && rf.IssueDate != null)
            {
                var x = _db.Tbl_FileResource.Where(m => m.DocumentType == rf.doctype && m.DateofIssue == rf.IssueDate && m.Subject == rf.Subject).ToList();
                return View(x);
            }
            else
            {
                var x = _db.Tbl_FileResource.ToList();
                return View(x);
            }
        }
        public ActionResult ResourcesDownload()
        {
            var x = db.Tbl_FileResource.ToList();
            return View(x);
        }

    }
}