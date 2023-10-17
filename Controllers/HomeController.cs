using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Manager;
using UmangMicro.Models;
using static UmangMicro.Manager.Enums;

namespace UmangMicro.Controllers
{
    public class HomeController : Controller
    {
        UM_DBEntities db = new UM_DBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnimationFilm()
        {
            return View();
        }

        public ActionResult CaseStudyList()
        {
            return View();
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
            DataSet ds = SP_Model.GetSP_MicroCaseList(1);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
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
        public ActionResult CaseDetail(int Id=0,string AN="")
        {
            DataSet ds = SP_Model.GetSP_MicroCaseList(3, Id);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return PartialView("_CaseDetailView",dt);
        }

        #endregion

    }
}