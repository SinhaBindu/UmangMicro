using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Models;
using UmangMicro.Manager;
using Newtonsoft.Json;
using static UmangMicro.Manager.Enums;
using System.IO;
using System.Data;
using System;
using System.EnterpriseServices;
using UmangMicro.Controllers;
using System.Web.UI;
using System.Xml.Linq;
using UmangMicro.Helpers;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Collections.Generic;
using FileInfo = UmangMicro.Models.FileInfo;
using IPEL.Manager;


namespace UmangMicro.Controllers
{
    [Authorize]
    public class MasterController : BaseController
    {
        UM_DBEntities db = new UM_DBEntities();
        JsonResponseData response = new JsonResponseData();
        int result = 0; bool CheckStatus = false;
        string MSG = string.Empty;

        #region Course Details Create,List,Update
        public ActionResult CourseDetail()
        {
            CoursesDModel model = new CoursesDModel();
            return View(model);
        }
        public ActionResult GetCourseDetail()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.GetSPCourseEdit();
                if (tbllist != null)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_CourseEdit", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult CourseD(int Id = 0)
        {
            CoursesDModel model = new CoursesDModel();
            if (Id > 0)
            {
                var tbl = db.tbl_CoursesDetail.Find(Id);
                if (tbl != null && Id > 0)
                {
                    model.NameCourseEng = tbl.NameCourseEng;
                    model.NameCourseHindi = tbl.NameCourseHindi;
                    model.CourseTypeEng = tbl.CourseTypeEng;
                    model.CourseTypeHindi = tbl.CourseTypeHindi;
                    model.JobOpportunityEng = tbl.JobOpportunityEng;
                    model.JobOpportunityHindi = tbl.JobOpportunityHindi;
                    model.CourseDurationEng = tbl.CourseDurationEng;
                    model.CourseDurationHindi = tbl.CourseDurationHindi;
                    model.ShortDescriptionCourseEng = tbl.ShortDescriptionCourseEng;
                    model.ShortDescriptionCourseHindi = tbl.ShortDescriptionCourseHindi;
                    model.EligibilityEng = tbl.EligibilityEng;
                    model.EligibilityHindi = tbl.EligibilityHindi;
                    model.MarksCriteriaEng = tbl.MarksCriteriaEng;
                    model.MarksCriteriaHindi = tbl.MarksCriteriaHindi;
                    model.AdmissionProcessEng = tbl.AdmissionProcessEng;
                    model.AdmissionProcessHindi = tbl.AdmissionProcessHindi;
                    model.MediumInstructionEng = tbl.MediumInstructionEng;
                    model.MediumInstructionHindi = tbl.MediumInstructionHindi;
                    model.HostelAvailabilityEng = tbl.HostelAvailabilityEng;
                    model.HostelAvailabilityHindi = tbl.HostelAvailabilityHindi;
                    model.AvailableScholarshipEng = tbl.AvailableScholarshipEng;
                    model.AvailableScholarshipHindi = tbl.AvailableScholarshipHindi;
                    model.CategoryEng = tbl.CategoryEng;
                    model.CategoryHindi = tbl.CategoryHindi;
                    model.CategoryOtherEng = tbl.CategoryOtherEng;
                    model.CategoryOtherHindi = tbl.CategoryOtherHindi;
                    model.College_Unvty_InstEng = tbl.College_Unvty_InstEng;
                    model.College_Unvty_InstHindi = tbl.College_Unvty_InstHindi;
                    model.FeeStructureEng = tbl.FeeStructureEng;
                    model.FeeStructureHindi = tbl.FeeStructureHindi;
                    model.StatusInstitutionEng = tbl.StatusInstitutionEng;
                    model.StatusInstitutionHindi = tbl.StatusInstitutionHindi;
                    model.DistrictEng = tbl.DistrictEng;
                    model.DistrictHindi = tbl.DistrictHindi;
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CourseD(CoursesDModel model)
        {
            if (!ModelState.IsValid)
            {
                Danger("Required!", true);
                return View(model);
            }
            var tbl = model.ID != 0 ? db.tbl_CoursesDetail.Find(model.ID) : new tbl_CoursesDetail();
            if (tbl != null)
            {
                tbl.NameCourseEng = model.NameCourseEng;
                tbl.NameCourseHindi = model.NameCourseHindi;
                tbl.CourseTypeEng = model.CourseTypeEng;
                tbl.CourseTypeHindi = model.CourseTypeHindi;
                tbl.JobOpportunityEng = model.JobOpportunityEng;
                tbl.JobOpportunityHindi = model.JobOpportunityHindi;
                tbl.CourseDurationEng = model.CourseDurationEng;
                tbl.CourseDurationHindi = model.CourseDurationHindi;
                tbl.ShortDescriptionCourseEng = model.ShortDescriptionCourseEng;
                tbl.ShortDescriptionCourseHindi = model.ShortDescriptionCourseHindi;
                tbl.EligibilityEng = model.EligibilityEng;
                tbl.EligibilityHindi = model.EligibilityHindi;
                tbl.MarksCriteriaEng = model.MarksCriteriaEng;
                tbl.MarksCriteriaHindi = model.MarksCriteriaHindi;
                tbl.AdmissionProcessEng = model.AdmissionProcessEng;
                tbl.AdmissionProcessHindi = model.AdmissionProcessHindi;
                tbl.MediumInstructionEng = model.MediumInstructionEng;
                tbl.MediumInstructionHindi = model.MediumInstructionHindi;
                tbl.HostelAvailabilityEng = model.HostelAvailabilityEng;
                tbl.HostelAvailabilityHindi = model.HostelAvailabilityHindi;
                tbl.AvailableScholarshipEng = model.AvailableScholarshipEng;
                tbl.AvailableScholarshipHindi = model.AvailableScholarshipHindi;
                tbl.CategoryEng = model.CategoryEng;
                tbl.CategoryHindi = model.CategoryHindi;
                tbl.CategoryOtherEng = model.CategoryOtherEng;
                tbl.CategoryOtherHindi = model.CategoryOtherHindi;
                tbl.College_Unvty_InstEng = model.College_Unvty_InstEng;
                tbl.College_Unvty_InstHindi = model.College_Unvty_InstHindi;
                tbl.FeeStructureEng = model.FeeStructureEng;
                tbl.FeeStructureHindi = model.FeeStructureHindi;
                tbl.StatusInstitutionEng = model.StatusInstitutionEng;
                tbl.StatusInstitutionHindi = model.StatusInstitutionHindi;
                tbl.DistrictEng = model.DistrictEng;
                tbl.DistrictHindi = model.DistrictHindi;
                tbl.IsActive = true;
                if (model.ID == 0)
                {
                    tbl.CreatedBy = User.Identity.Name;
                    tbl.CreatedDt = DateTime.Now;
                    db.tbl_CoursesDetail.Add(tbl);
                }
                else
                {
                    tbl.UpdatedBy = User.Identity.Name;
                    tbl.UpdatedDt = DateTime.Now;
                }
                var res = db.SaveChanges();
                if (res > 0)
                {
                    Success("Added Successfully !", true);
                    return RedirectToAction("CourseD", new { id = tbl.ID });
                }
            }
            return View(model);
        }
        #endregion

        #region
        public ActionResult GetStateList()
        {
            try
            {
                var items = CommonModel.GetState();
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpGet]
        public ActionResult GetDistrictList(int StateId = 20)
        {
            try
            {
                var items = CommonModel.GetDistrict(false, StateId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetBlockList(int DistrictId)
        {
            try
            {
                var items = CommonModel.GetBlock(false, DistrictId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetClusterList(int BlockId)
        {
            try
            {
                var items = CommonModel.GetCluster(false, BlockId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Master
        public ActionResult UserDetaillist()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }
        public ActionResult GetUserDetailData()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = CommonModel.GetSPCutUserlist();
                if (tbllist != null)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_UserDetailData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        //public JsonResult GetTypeParticipantForWeeklyCollation(string PerformID, string PlanID, string ActivityID, string TypeofActivityID)
        //{
        //    try
        //    {
        //        //var data = CommonModel.GetParticipant(TypeofActivityID);
        //        DataTable dt = CommonModel.SPGetParticipantTypeForWeeklyCollation(PerformID, PlanID, ActivityID, TypeofActivityID);
        //        if (dt.Rows.Count > 0)
        //        {
        //            var data = JsonConvert.SerializeObject(dt);
        //            return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);  
        //        }
        //        return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet); 
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

        #endregion

        //#region Home - Resource
        //[AllowAnonymous]
        //public ActionResult Resource()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult Resource(ResourceModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        if (model.ResourceDownLoad.Count == 0)
        //        {
        //            ModelState.AddModelError("", "Please select at-least one Resource Module.");
        //        }
        //        return Json(new { IsSuccess = false, Data = "", Msg = "Please select at-least one Resource Module." }, JsonRequestBehavior.AllowGet);
        //    }
        //    try
        //    {
        //        tbl_Resource tbl = new tbl_Resource();
        //        tbl.Name = model.Name;
        //        tbl.Designation = model.Designation;
        //        tbl.Designation_Other = model.Designation_Other;
        //        tbl.Organisation = model.Organisation;
        //        tbl.Email = model.Email;
        //        tbl.ContactNo = model.ContactNo;
        //        tbl.Age = model.Age;
        //        tbl.State = model.State;
        //        tbl.District = model.District;
        //        tbl.ResourceDownLoad = string.Join(",", model.ResourceDownLoad);
        //        tbl.IsActive = true;
        //        tbl.CreatedOn = DateTime.Now;
        //        db.tbl_Resource.Add(tbl);
        //        int res = db.SaveChanges();

        //        var RD = string.Join(",", model.ResourceDownLoad);
        //        if (res > 0)
        //        {
        //            ViewBag.RD = model.ResourceDownLoad;
        //            ModelState.Clear();
        //            model = new ResourceModel();
        //            ModelState.AddModelError("", "Record Save Successfully....");
        //            return Json(new { IsSuccess = true, Data = "",Msg= "Record Save Successfully...." }, JsonRequestBehavior.AllowGet);
        //        }

        //        //MultipleFileDownload obj = new MultipleFileDownload();
        //        //////int CurrentFileID = Convert.ToInt32(FileID);  
        //        //if (model.ResourceDownLoad.Count != 0 && model.ResourceDownLoad != null)
        //        //{
        //        //    List<FileInfo> listFiles = new List<FileInfo>();
        //        //    foreach (var item in model.ResourceDownLoad)
        //        //    {
        //        //        listFiles.Add(new FileInfo()
        //        //        {
        //        //            FileName = item.ToString() + ".pdf",
        //        //        });
        //        //    }
        //        //    if (listFiles != null)
        //        //    {
        //        //        var filesCol = MultipleFileDownload.GetFile("~/ResourceFile", listFiles).ToList();
        //        //        using (var memoryStream = new MemoryStream())
        //        //        {
        //        //            using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        //        //            {
        //        //                for (int i = 0; i < filesCol.Count; i++)
        //        //                {
        //        //                    ziparchive.CreateEntryFromFile(filesCol[i].FilePath, filesCol[i].FileName);
        //        //                }
        //        //            }
        //        //            ModelState.Clear();
        //        //            model = new ResourceModel();
        //        //            ModelState.AddModelError("", "Resource File Downloaded....");
        //        //            return File(memoryStream.ToArray(), "application/zip", "ResourceFile.zip");
        //        //        }
        //        //    }
        //        //}
        //        return Json(new { IsSuccess = false, Data = "", Msg = "There was a communication error." }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { IsSuccess = false, Data = "", Msg = "There was a communication error." }, JsonRequestBehavior.AllowGet);
        //    }
        //   // return View(model);
        //}
        //#endregion

        #region
        public ActionResult GetMicroCase()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = db.tbl_MicroCase.ToList(); //CommonModel.GetSPCutUserlist();
                if (tbllist != null)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_MicroCaseData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult MicroCase(int Id = 0)
        {
            MicroCaseModel model = new MicroCaseModel();
            if (Id > 0)
            {
                var tbl = db.tbl_MicroCase.Find(Id);
                if (tbl != null)
                {
                    model.ID = tbl.ID;
                    model.Date = tbl.Date.Value;
                    model.Subject = tbl.Subject;
                    model.Category = tbl.Category;
                    model.HtmlEditor = tbl.HtmlEditor;
                    model.PhotoPath = tbl.PhotoPath;
                    model.BannerPath = tbl.BannerPath;
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult MicroCase(MicroCaseModel model)
        {
            JsonResponseData response = new JsonResponseData();
            result = 0;
            try
            {
                var tbl = model.ID != 0 ? db.tbl_MicroCase.Find(model.ID) : new tbl_MicroCase();
                if (tbl != null)
                {
                    tbl.Date = model.Date.Value;  
                    tbl.Subject = !(string.IsNullOrWhiteSpace(model.Subject)) ? model.Subject.Trim() : null;
                    tbl.Category = !(string.IsNullOrWhiteSpace(model.Category)) ? model.Category.Trim() : null;
                    tbl.HtmlEditor = !string.IsNullOrEmpty(model.HtmlEditor) ? model.HtmlEditor.Trim() : null;
                    tbl.IsActive = true;
                    if (model.ID == 0)
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            tbl.CreatedBy = MvcApplication.CUser.Id;
                            tbl.CreatedOn = DateTime.Now;
                        }
                        db.tbl_MicroCase.Add(tbl);
                    }
                    else
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            tbl.UpdatedOn = DateTime.Now;
                            tbl.UpdatedBy = MvcApplication.CUser.Id;
                        }
                    }
                    result = db.SaveChanges();
                    if (Request.Files != null && Request.Files.Count > 0)
                    {
                        var multiLinkPic = string.Empty;
                        foreach (var item in Request.Files.AllKeys)
                        {
                            var postedFile = Request.Files[item];
                            string extension = Path.GetExtension(postedFile.FileName);
                            if (extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".JPG"
                                        || extension.ToUpper() == ".PNG" || extension.ToUpper() == "GIF")
                            {
                                if (item== "Banner")
                                {
                                    tbl.BannerPath = !string.IsNullOrWhiteSpace(postedFile.FileName) ? CommonModel.SaveSingleFile(postedFile, "CaseStudy", tbl.ID.ToString()) : null;
                                }
                                if (item == "Photo")
                                {
                                    tbl.PhotoPath = !string.IsNullOrWhiteSpace(postedFile.FileName) ? CommonModel.SaveSingleFile(postedFile, "CaseStudy", tbl.ID.ToString()) : null;
                                }
                            }
                        }
                        result = db.SaveChanges();
                    }
                }
                if (result > 0)
                {
                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Data Submitted " + " Successfully.....", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                }
                //}
                //else
                //{
                //    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !!", Data = null };
                //    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                //    resResponse1.MaxJsonLength = int.MaxValue;
                //    return resResponse1;
                //};
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
            return View();
        }
        #endregion


    }
}