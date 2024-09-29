using Microsoft.Owin.Security.Twitter.Messages;
using Newtonsoft.Json;
using SubSonic.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UmangMicro.Manager;
using UmangMicro.Models;
using static UmangMicro.Manager.Enums;

namespace UmangMicro.Controllers
{
    [Authorize]
    public class ModularController : Controller
    {
        UM_DBEntities db = new UM_DBEntities();
        int results = 0; int results2nd = 0;
        // GET: Modular
        public ActionResult Index(int? DId, int? SId)
        {
            QesRes QesRes = new QesRes();
            QesRes.DistrictId = DId != null ? DId.Value : 0;
            QesRes.SchoolId = SId != null ? SId.Value : 0;
            return View(QesRes);
        }
        public ActionResult GetIndex(string Sdt, string Edt, string DistrictId, string SchoolId)
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                SchoolId = SchoolId == "0" ? string.Empty : SchoolId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                SchoolId = (string.IsNullOrWhiteSpace(SchoolId)) ? "ALL" : SchoolId;
                var items = SP_Model.GetSP_ModularListData(Sdt, Edt, DistrictId.ToUpper(), SchoolId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_ModularData", items);
                    var datahtml = JsonConvert.SerializeObject(new { IsSuccess = true, reshtml = html });
                    return Content(datahtml, "application/json");
                }
                return Json(new { IsSuccess = false, reshtml = "Record Not Found." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;    
                return Json(new { IsSuccess = false, reshtml = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ModularSForm()
        {
            ModularSModel model = new ModularSModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ModularSForm(ModularSModel model)
        {
            JsonResponseData response = new JsonResponseData();
            UM_DBEntities db_ = new UM_DBEntities();
            try
            {
                var MS_model = this.Request.Unvalidated.Form["MS_model"];
                var cdate = string.Format("{0:M/dd/yyyy}",
                                  DateTime.Now.Date);
                //var getid =
                //     //(from dt in
                //     (from ac in db_.tbl_Modular.ToList()
                //      from mc in db_.tbl_ModularChild
                //      where ac.Id == ac.Id && ac.Id == model.Id
                //      select new
                //      {
                //          CreatedOndmy = string.Format("{0:M/dd/yyyy}",
                //           ac.CreatedOn),
                //          ac.SchoolId,
                //          ac.CalssId,
                //      }).ToList();

                if (model.SchoolId == 0)
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Select School Name.<br /> ", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                }
                if (model != null)
                {
                    if (MS_model != null)
                    {
                        var mlist = JsonConvert.DeserializeObject<List<ModularSModel>>(MS_model);
                        if (mlist.Count() > 0)
                        {
                            tbl_Modular tbl_MS;
                            List<tbl_Modular> tbl_list = new List<tbl_Modular>();
                            var maxid = db.tbl_Modular.Max(x => x.Id);
                            foreach (var m in mlist)
                            {
                                string filePath = "";
                                var file = Request.Files.AllKeys;
                                for (int i = 0; i < Request.Files.Count; i++)
                                {
                                    if (Request.Files.GetKey(i) == ("AchieveImage_" + m.CalssId))
                                    {
                                        HttpPostedFileBase fileUpload = Request.Files.Get(i);
                                        filePath = CommonModel.SaveGroupCounsellingModelSessionFile(fileUpload, maxid.ToString()+"_Clsid"+m.CalssId, "ModularAchievementImage");
                                    }
                                }

                                maxid = maxid + 1;

                                tbl_MS = new tbl_Modular()
                                {
                                    SchoolId = model.SchoolId,
                                    CalssId = m.CalssId,
                                    NoofStudent = m.NoofStudent,
                                    ConductedDate = m.ConductedDate,
                                    Session = m.Session,
                                    AchieveImage = filePath,
                                    CreatedBy = MvcApplication.CUser.Id,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true,

                                };



                                tbl_list.Add(tbl_MS);
                            }
                            if (tbl_list.Count > 0)
                            {
                                db.tbl_Modular.AddRange(tbl_list);
                                results = db.SaveChanges();
                                if (results > 0)
                                {
                                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, Modular Session Submit successfully ! \r\n <br />", Data = null };
                                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                                    resResponse3.MaxJsonLength = int.MaxValue;
                                    return resResponse3;
                                }
                            }
                        }
                        else
                        {
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = " Congratulations,Select Session,\r\n <br /> Enter the No. of students attended session \r\n <br />Enter the Conducted date. ! \r\n <br /> ", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                    }
                }
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


        public ActionResult GetModularSession()
        {
            ModularSModel modularS = new ModularSModel();
            return View(modularS);
        }

        [HttpGet]
        public ActionResult LoadFormModular(int Cohort)
        {
            ModularSModel modularS = new ModularSModel();
            modularS.Cohort = Cohort;
            return PartialView("_ModularInput", modularS);
        }
        public ActionResult ModularSummary()
        {
            return View();
        }
        public ActionResult GetModularSummary(string Sdt, string Edt, string DistrictId, string SchoolId, string ClassId)
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                SchoolId = SchoolId == "0" ? string.Empty : SchoolId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                SchoolId = (string.IsNullOrWhiteSpace(SchoolId)) ? "ALL" : SchoolId;
                var items = SP_Model.GetSP_ModularSummary(Sdt, Edt, DistrictId.ToUpper(), SchoolId.ToUpper(), ClassId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_ModularSummaryData", items);
                    return Json(new { IsSuccess = true, reshtml = html, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
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

    }
}