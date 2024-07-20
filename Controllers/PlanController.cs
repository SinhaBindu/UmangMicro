using Newtonsoft.Json;
using SubSonic.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Manager;
using UmangMicro.Models;
using static UmangMicro.Manager.Enums;
using System.Data;

namespace UmangMicro.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        UM_DBEntities db = new UM_DBEntities();
        int results = 0; int results2nd = 0;
        // GET: PlanModular
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetIndex(string Sdt, int Task, string Edt, string DistrictId, string SchoolId)
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                SchoolId = SchoolId == "0" ? string.Empty : SchoolId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                SchoolId = (string.IsNullOrWhiteSpace(SchoolId)) ? "ALL" : SchoolId;
                var items = SP_Model.GetSP_PlanListData(Sdt, Task, Edt, DistrictId.ToUpper(), SchoolId.ToUpper());
                ViewBag.task = Task;
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_PlanSFormData", items);
                    return Json(new { IsSuccess = true, reshtml = html, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PlanSForm(int? Id, int TakId = 2)
        {
            UM_DBEntities db_ = new UM_DBEntities();
            PlanModularModel model = new PlanModularModel();
            if (Id != 0 && TakId == 2 && Id != null)
            {
                var tbl = db_.tbl_Plan.Find(Id);
                var tbldis = db_.SchoolMaster_N.Find(tbl.SchoolId);

                model.Id = tbl.Id;
                model.TaskType = tbl.Task;
                model.DistrictId = Convert.ToInt32(tbldis.DistrictCode);
                model.BlockId = Convert.ToInt32(tbldis.BlockCode);
                model.SchoolId = tbl.SchoolId;
                model.ConductedDate = tbl.ConductedDate;
                model.ClassMLT = tbl.CalssMlt;
                model.NoofStudent = tbl.NoofStudent;
                model.SessionType = tbl.SessionType;
                if (tbl.SessionType == 1 || tbl.SessionType == 2)
                {
                    model.Session = tbl.Session;
                    model.SessionInput = null;
                }
                else if (tbl.SessionType == 3)
                {
                    model.Session = null;
                    model.SessionInput = tbl.SessionInput;
                }

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PlanSForm(PlanModularModel model)
        {
            JsonResponseData response = new JsonResponseData();
            UM_DBEntities db_ = new UM_DBEntities();
            try
            {

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
                    if (model.TaskType == 1 && model.Id == 0)
                    {
                        var MS_model = this.Request.Unvalidated.Form["MS_model"];
                        if (MS_model != null)
                        {
                            var mlist = JsonConvert.DeserializeObject<List<PlanModularModel>>(MS_model);
                            if (mlist.Count() > 0)
                            {
                                tbl_Plan tbl_MS;
                                List<tbl_Plan> tbl_list = new List<tbl_Plan>();
                                foreach (var m in mlist)
                                {
                                    tbl_MS = new tbl_Plan()
                                    {
                                        SchoolId = model.SchoolId,
                                        CalssId = m.CalssId,
                                        // NoofStudent = m.NoofStudent,
                                        Task = model.TaskType,
                                        ConductedDate = m.ConductedDate,
                                        Session = m.Session,
                                        CreatedBy = MvcApplication.CUser.Id,
                                        CreatedOn = DateTime.Now,
                                        IsActive = true
                                    };
                                    tbl_list.Add(tbl_MS);
                                }
                                if (tbl_list.Count > 0)
                                {
                                    db.tbl_Plan.AddRange(tbl_list);
                                    results = db.SaveChanges();
                                    if (results > 0)
                                    {
                                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations,Plan Modular Session Submit successfully ! \r\n <br />", Data = null };
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
                    else if (model.TaskType == 2 && model.Id == 0)
                    {
                        if (model.SessionType != null)
                        {
                            tbl_Plan tbl = new tbl_Plan();
                            tbl.ConductedDate = model.ConductedDate;
                            tbl.SchoolId = model.SchoolId;
                            tbl.CalssMlt = model.ClassMLT;
                            tbl.NoofStudent = model.NoofStudent;
                            tbl.Task = model.TaskType;
                            tbl.SessionType = model.SessionType;
                            if (model.SessionType == 1 || model.SessionType == 2)
                            {
                                tbl.Session = model.Session;
                                tbl.SessionInput = null;
                                //Carre
                                if (model.Session==99 && model.SessionType == 1)
                                {
                                    tbl.SessionInput = model.SessionInput;
                                }
                                //Couser
                                if (model.Session == 99 && model.SessionType == 2)
                                {
                                    tbl.SessionInput = model.SessionInput;
                                }
                            }
                            else if (model.SessionType == 3)
                            {
                                tbl.Session = null;
                                tbl.SessionInput = model.SessionInput;
                            }

                            tbl.IsActive = true;
                            tbl.CreatedBy = MvcApplication.CUser.Id;
                            tbl.CreatedOn = DateTime.Now;
                            db.tbl_Plan.Add(tbl);
                            results = db.SaveChanges();
                            if (results > 0)
                            {
                                response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations,Plan Modular Session Submit successfully ! \r\n <br />", Data = null };
                                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse3.MaxJsonLength = int.MaxValue;
                                return resResponse3;
                            }
                        }
                    }
                    else if (model.TaskType == 2 && model.Id != 0)
                    {
                        var tbl = db_.tbl_Plan.Find(model.Id);
                        if (tbl.ConductedDate > model.AchievementDate)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Please Enter The Plan Date Greater Than Achievement Date .<br /> ", Data = null };
                            var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse1.MaxJsonLength = int.MaxValue;
                            return resResponse1;
                        }

                        if (tbl != null)
                        {
                            var filePath = CommonModel.SaveGroupCounsellingModelSessionFile(model.AchieveImage, tbl.Id.ToString(), "GPCounselling");
                            var physicalFilePath = Server.MapPath(filePath);

                            tbl.AchieveImagepath = filePath;
                            tbl.CounsellingRemarks = model.CounsellingRemarks;
                            tbl.AchievementDate = model.AchievementDate;
                            tbl.AchievedNoofStudent = model.AchievedNoofStudent;


                            int results = db_.SaveChanges();
                            if (results > 0)
                            {
                                response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Congratulations, Group Counselling updated successfully!<br />", Data = null };
                                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse3.MaxJsonLength = int.MaxValue;
                                return resResponse3;
                            }
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
        public ActionResult GetPlanSession()
        {
            PlanModularModel PlanmodularS = new PlanModularModel();
            return View(PlanmodularS);
        }
        [HttpGet]
        public ActionResult PlanLoadForm(int Cohort)
        {
            PlanModularModel PlanmodularS = new PlanModularModel();
            PlanmodularS.Cohort = Cohort;
            return PartialView("_PlanInput", PlanmodularS);
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
        public ActionResult PlanGraph()
        {
            var model = new FilterModel
            {

            };
            return View(model);
        }
        [HttpPost]
        public ActionResult PlanGraph(FilterModel model)
        {
            bool isSuccess = false;
            try
            {
                DataTable dt = SP_Model.Sp_PlanGraph();
                if (dt.Rows.Count > 0)
                {
                    isSuccess = true;
                    var dataList = dt.AsEnumerable().Select(row => new
                    {
                        school_name = row["school_name"].ToString(),
                        NoofData_Modular = Convert.ToInt32(row["NoofData_Modular"]),
                        NoofData_Plan = Convert.ToInt32(row["NoofData_Plan"])
                    }).ToList();

                    return Json(new
                    {
                        IsSuccess = isSuccess,
                        Data = dataList
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    IsSuccess = isSuccess,
                    Data = "No records found."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    IsSuccess = isSuccess,
                    Data = "There was a communication error."
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}



