using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using UmangMicro.Manager;
using UmangMicro.Models;

namespace UmangMicro.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            //if (User.IsInRole("Teacher") || User.IsInRole("Admin"))
            //{
            //    return RedirectToAction("SevenDaysPlan", "Plan");
            //}
            return View();
        }
        public ActionResult GetDashboard(string Sdt, string Edt, string DistrictId, string BlockId)
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                BlockId = BlockId == "0" ? string.Empty : BlockId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                BlockId = (string.IsNullOrWhiteSpace(BlockId)) ? "ALL" : BlockId;
                var items = SP_Model.GetSP_DashboardData(Sdt, Edt, DistrictId.ToUpper(), BlockId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    //var html = ConvertViewToString("_CaseHList", items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllChart(string Sdt, string Edt, string DistrictId, string BlockId)
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                BlockId = BlockId == "0" ? string.Empty : BlockId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                BlockId = (string.IsNullOrWhiteSpace(BlockId)) ? "ALL" : BlockId;
                var items = SP_Model.GetSP_AllChartData(Sdt, Edt, DistrictId.ToUpper(), BlockId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    //var html = ConvertViewToString("_CaseHList", items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTypeQueryChart(string Sdt, string Edt, string DistrictId, string BlockId)
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                BlockId = BlockId == "0" ? string.Empty : BlockId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                BlockId = (string.IsNullOrWhiteSpace(BlockId)) ? "ALL" : BlockId;
                var items = SP_Model.GetSP_ChartDataTypeQuery(Sdt, Edt, DistrictId.ToUpper(), BlockId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    //var html = ConvertViewToString("_CaseHList", items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetModularChart(string Sdt, string Edt, string DistrictId, string BlockId)
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                BlockId = BlockId == "0" ? string.Empty : BlockId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                BlockId = (string.IsNullOrWhiteSpace(BlockId)) ? "ALL" : BlockId;
                var items = SP_Model.GetSP_ModularChart(Sdt, Edt, DistrictId.ToUpper(), BlockId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    //var html = ConvertViewToString("_CaseHList", items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCoursesDetail(string Parame)
        {
            try
            {
                //dt = ds.Tables.Contains("Tables[0]") == true ? dt : ds.Tables[0];//dt1 = ds.Tables.Contains("Tables[1]") == true ? dt1 : ds.Tables[1];
                bool IsCheck = false;
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = CommonModel.GetSPCourselist(Parame);
                if (ds.Tables.Count > 0)
                {
                    IsCheck = true;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        dt1 = ds.Tables[1];
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        dt2 = ds.Tables[2];
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        dt3 = ds.Tables[3];
                    }
                }
                var html_1 = ConvertViewToString("_SkillTrain", dt);
                var html_2 = ConvertViewToString("_Scheme", dt1);
                var html_3 = ConvertViewToString("_Scholarship", dt2);
                var html_4 = ConvertViewToString("_CD", dt3);
                //var html3 = ConvertViewToString("_UserDetailData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, html1 = html_1, html2 = html_2, html3 = html_3, html4 = html_4 }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There was a communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult LastLogin()
        {
            return View();
        }
        public ActionResult GetLastLogin(string Parame)
        {
            try
            {
                //dt = ds.Tables.Contains("Tables[0]") == true ? dt : ds.Tables[0];//dt1 = ds.Tables.Contains("Tables[1]") == true ? dt1 : ds.Tables[1];
                bool IsCheck = false;
                DataTable dt = SP_Model.GetSPLastLogin();
                if (dt.Rows.Count > 0)
                {
                    IsCheck = true;
                    var html_1 = ConvertViewToString("_LastLoginData", dt);
                    var res1 = Json(new { IsSuccess = IsCheck, html1 = html_1 }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                var res = Json(new { IsSuccess = IsCheck, html1 = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There was a communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult Resource()
        {
            return View();
        }
        public ActionResult GetResource(string Sd = "", string Ed = "")
        {
            try
            {
                //dt = ds.Tables.Contains("Tables[0]") == true ? dt : ds.Tables[0];//dt1 = ds.Tables.Contains("Tables[1]") == true ? dt1 : ds.Tables[1];
                bool IsCheck = false;
                DataSet ds = SP_Model.GetSP_Resource();
                DataTable dt = new DataTable();
                DataTable dtDet = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    IsCheck = true;
                    dt = ds.Tables[0]; dtDet = ds.Tables[1];
                    var html_1 = ConvertViewToString("_ResourceData", dtDet);
                    var res1 = Json(new { IsSuccess = IsCheck, html1 = html_1, Data = JsonConvert.SerializeObject(dt) }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                var res = Json(new { IsSuccess = IsCheck, html1 = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There was a communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult RS()
        {
            return View();
        }
        public ActionResult CaseHReportedList()
        {
            return View();
        }
        public ActionResult GetCaseHReportedList(string Para = "", string SearchBy = "", string DOB = "", string Sdt = "", string Edt = "", string DistrictId = "", string BlockId = "")
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                BlockId = BlockId == "0" ? string.Empty : BlockId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                BlockId = (string.IsNullOrWhiteSpace(BlockId)) ? "ALL" : BlockId;
                var items = SP_Model.GetSP_CaseHReportedByList(Para, SearchBy, DOB, Sdt, Edt, DistrictId.ToUpper(), BlockId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_CaseHReportList", items);
                    return Json(new { IsSuccess = true, res = html }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SummaryData()
        {
            return View();
        }
        public ActionResult GetSummaryData(string Para = "", string SearchBy = "", string DOB = "", string Sdt = "", string Edt = "", string DistrictId = "", string BlockId = "")
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                BlockId = BlockId == "0" ? string.Empty : BlockId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                BlockId = (string.IsNullOrWhiteSpace(BlockId)) ? "ALL" : BlockId;
                var items = SP_Model.GetSP_SummaryDistBlockData(Para, SearchBy, DOB, Sdt, Edt, DistrictId.ToUpper(), BlockId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_SummaryData", items);
                    return Json(new { IsSuccess = true, res = html }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        //Modular Session
        public ActionResult FullCalendarReport()
        {
            FilterModel model=new FilterModel();
            return View(model);
        }
        public ActionResult GetCalendarReportData(string Sdt, string Edt, string MonthId, string YearId)
        {
            try
            {
                MonthId = MonthId == "0" ? string.Empty : MonthId;
                YearId = YearId == "0" ? string.Empty : YearId;
                MonthId = (string.IsNullOrWhiteSpace(MonthId)) ? "ALL" : MonthId;
                YearId = (string.IsNullOrWhiteSpace(YearId)) ? "ALL" : YearId;
                var items = SP_Model.GetSPCalendarReport(Sdt, Edt, MonthId.ToUpper(), YearId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    //var html = ConvertViewToString("_CaseHList", items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
       //Group Conselleing
        public ActionResult CalendarGroupCounselling()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetCalendarGroupCounsellingData(string Sdt, string Edt, string MonthId, string YearId)
        {
            try
            {
                MonthId = MonthId == "0" ? string.Empty : MonthId;
                YearId = YearId == "0" ? string.Empty : YearId;
                MonthId = (string.IsNullOrWhiteSpace(MonthId)) ? "ALL" : MonthId;
                YearId = (string.IsNullOrWhiteSpace(YearId)) ? "ALL" : YearId;
                var items = SP_Model.GetSP_CalendarGroupConsellingReport(Sdt, Edt, MonthId.ToUpper(), YearId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    //var html = ConvertViewToString("_CaseHList", items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
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