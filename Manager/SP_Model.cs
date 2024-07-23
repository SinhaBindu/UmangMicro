using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmangMicro.Models;

namespace UmangMicro.Manager
{
    public class SP_Model
    {
        public static DataTable SPCandRegList(string SD, string ED,string DistrictId)
        {
            StoredProcedure sp = new StoredProcedure("SP_RegList");
            sp.Command.AddParameter("@FD", SD, DbType.String);
            sp.Command.AddParameter("@TD", ED, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRoleLogin(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSPCourseEdit()
        {
            StoredProcedure sp = new StoredProcedure("SP_CourseEdit");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSPLastLogin()
        {
            StoredProcedure sp = new StoredProcedure("GetSPLastLogin");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #region
        public static DataSet GetSP_Resource()
        {
            StoredProcedure sp = new StoredProcedure("SP_ResourceData");
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_MicroCaseList(int Para, int CategId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetMicroCaseList");
            sp.Command.AddParameter("@Para", Para, DbType.Int32);
            sp.Command.AddParameter("@CategId", CategId, DbType.Int32);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable GetSP_MicroCase_Summary()
        {
            StoredProcedure sp = new StoredProcedure("SP_MicroCase_Summary");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet GetMC_CaseStudyList(string CategId)
        {
            StoredProcedure sp = new StoredProcedure("SP_MicroC_CaseStudyList");
            sp.Command.AddParameter("@CategId", CategId, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        #endregion
        public static DataTable GetSP_DistrictList()
        {
            StoredProcedure sp = new StoredProcedure("SP_District");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_BlockList(string DistrictCode)
        {
            StoredProcedure sp = new StoredProcedure("SP_Block");
            sp.Command.AddParameter("@DistrictCode", DistrictCode, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_PanchayatList(string DistrictCode, string BlockCode)
        {
            StoredProcedure sp = new StoredProcedure("SP_Panchayat");
            sp.Command.AddParameter("@DistrictCode", DistrictCode, DbType.String);
            sp.Command.AddParameter("@BlockCode", BlockCode, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_VillageList(string DistrictCode, string BlockCode, string PanchayatCode)
        {
            StoredProcedure sp = new StoredProcedure("SP_Village");
            sp.Command.AddParameter("@DistrictCode", DistrictCode, DbType.String);
            sp.Command.AddParameter("@BlockCode", BlockCode, DbType.String);
            sp.Command.AddParameter("@PanchayatCode", PanchayatCode, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_SchoolList(string DistrictCode, string BlockCode)
        {
            StoredProcedure sp = new StoredProcedure("SP_School");
            sp.Command.AddParameter("@DistrictCode", DistrictCode, DbType.String);
            sp.Command.AddParameter("@BlockCode", BlockCode, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_StudentList(string Para, string SearchBy, string DOB)
        {
            StoredProcedure sp = new StoredProcedure("SP_StudentList");
            sp.Command.AddParameter("@Para", Para, DbType.String);
            sp.Command.AddParameter("@SearchBy", SearchBy, DbType.String);
            sp.Command.AddParameter("@DOB", DOB, DbType.String);
            sp.Command.AddParameter("@DistrictId", MvcApplication.CUser.DistrictId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_RIASECList(string CaseID)
        {
            StoredProcedure sp = new StoredProcedure("SP_RIASECResult");
            sp.Command.AddParameter("@CaseID", CaseID, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet GetSP_RIASECGuidData(string Para)
        {
            StoredProcedure sp = new StoredProcedure("SP_RIASEC_GuidedList");
            sp.Command.AddParameter("@Para", Para, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_SearchBYCD()
        {
            StoredProcedure sp = new StoredProcedure("SP_SearchBYCD");
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetComdSearchlist(SearchModel DList)
        {
            StoredProcedure sp = new StoredProcedure("SP_ComdSearchlist");
            sp.Command.AddParameter("@Para1", DList.Para1, DbType.String);
            sp.Command.AddParameter("@Para2", DList.Para2, DbType.String);
            sp.Command.AddParameter("@Para3", DList.Para3, DbType.String);
            sp.Command.AddParameter("@Para4", DList.Para4, DbType.String);
            sp.Command.AddParameter("@Para5", DList.Para5, DbType.String);
            sp.Command.AddParameter("@Para6", DList.Para6, DbType.String);
            sp.Command.AddParameter("@Para7", DList.Para7, DbType.String);
            sp.Command.AddParameter("@Para8", DList.Para8, DbType.String);
            sp.Command.AddParameter("@Paratbl", DList.Paratbl, DbType.String);
            sp.Command.AddParameter("@ParaAll", DList.ParaAll, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            if (ds.Tables.Count > 0)
            {
                return ds;
            }
            return ds;
        }
        public static DataSet GetSP_CaseHistoryDetails(string RowId, string CaseID)
        {
            StoredProcedure sp = new StoredProcedure("SP_CaseHistoryDetails");
            sp.Command.AddParameter("@RowId", RowId, DbType.String);
            sp.Command.AddParameter("@CaseID", CaseID, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            if (ds.Tables.Count > 0)
            {
                return ds;
            }
            return ds;
        }
        public static DataTable GetSP_CaseHistoryCount(string RowId, string CaseID)
        {
            StoredProcedure sp = new StoredProcedure("SP_CaseHistoryCount");
            sp.Command.AddParameter("@RowId", RowId, DbType.String);
            sp.Command.AddParameter("@CaseID", CaseID, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_RCTestRes_List(string Para, string SearchBy,string DOB, string Sdt, string Edt, string SchoolId, string DistrictId)
        {
            StoredProcedure sp = new StoredProcedure("SP_RToCTestRes_List");
            //sp.Command.AddParameter("@Para", Para, DbType.String);
            //sp.Command.AddParameter("@CaseIDNameDOB", SearchBy, DbType.String);
            //sp.Command.AddParameter("@DOB", DOB, DbType.String);
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@SchoolId", SchoolId, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRoleLogin(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet GetSP_RCTestRes_Detail(string RowId, string CaseID)
        {
            StoredProcedure sp = new StoredProcedure("SP_RToCTestResult_Details");
            sp.Command.AddParameter("@RowId", RowId, DbType.String);
            sp.Command.AddParameter("@CaseID", CaseID, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable GetSP_CaseHList(string Para, string SearchBy, string DOB, string Sdt, string Edt, string DistrictId, string BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_CaseHList");
            //sp.Command.AddParameter("@Para", Para, DbType.String);
            //sp.Command.AddParameter("@CaseIDNameDOB", SearchBy, DbType.String);
            //sp.Command.AddParameter("@DOB", DOB, DbType.String);
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_CaseHReportedByList(string Para, string SearchBy, string DOB, string Sdt, string Edt, string DistrictId, string BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_CaseHReportedByList");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet GetSP_DashboardData(string Sdt, string Edt, string DistrictId, string BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_DashboardData");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_AllChartData(string Sdt, string Edt, string DistrictId, string BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ChartData6To12th");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSPCalendarReport(string Sdt, string Edt, string MonthId, string YearId)
        {
            StoredProcedure sp = new StoredProcedure("SP_CalendarModularSessionReport");
            //sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            //sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@MonthId", MonthId, DbType.String);
            sp.Command.AddParameter("@YearId", YearId, DbType.String);
            //sp.Command.AddParameter("@DistrictId", MonthId, DbType.String);
            //sp.Command.AddParameter("@BlockId", YearId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_CalendarGroupConsellingReport(string Sdt, string Edt, string MonthId, string YearId)
        {
            StoredProcedure sp = new StoredProcedure("SP_CalendarGroupConsellingReport");
            sp.Command.AddParameter("@MonthId", MonthId, DbType.String);
            sp.Command.AddParameter("@YearId", YearId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_ChartDataTypeQuery(string Sdt, string Edt, string DistrictId, string BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ChartDataTypeQuery");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable GetSP_SummaryDistBlockData(string Para, string SearchBy, string DOB, string Sdt, string Edt, string DistrictId, string BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_SummaryDistBlockData");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_ModularListData(string Sdt, string Edt, string DistrictId, string SchoolId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ModularList");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@SchoolId", SchoolId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_ModularSummary(string Sdt, string Edt, string DistrictId, string SchoolId,string ClassId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ModularSummary");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@SchoolId", SchoolId, DbType.String);
            sp.Command.AddParameter("@ClassId",ClassId , DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_PlanListData(string Sdt, int Task,  string Edt, string DistrictId, string SchoolId)
        {
            StoredProcedure sp = new StoredProcedure("SP_PlanList");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Task", Task, DbType.Int32);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@SchoolId", SchoolId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_PlanSevenDaysListData(string Sdt, int Task, string Edt, string DistrictId, string SchoolId)
        {
            StoredProcedure sp = new StoredProcedure("SP_SevendaysPlanList");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Task", Task, DbType.Int32);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@SchoolId", SchoolId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_TrainingListData(string Sdt, int Trainingtype, string Edt, string DistrictId, string SchoolId)
        {
            StoredProcedure sp = new StoredProcedure("SP_TrainingList");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Trainingtype", Trainingtype, DbType.Int32);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@SchoolId", SchoolId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetSP_TeachersData(string DistrictIds = "", string SchoolIds = "")
        {
            StoredProcedure sp = new StoredProcedure("SP_GetTeacher");
            sp.Command.AddParameter("@DistrictIds", DistrictIds, DbType.String);
            sp.Command.AddParameter("@SchoolIds", SchoolIds, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable Sp_PlanGraph()
        {
            StoredProcedure sp = new StoredProcedure("Sp_PlanGraph");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet GetSP_ModularChart(string Sdt, string Edt, string DistrictId, string BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ChartModularSession");
            sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            sp.Command.AddParameter("@Edt", Edt, DbType.String);
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.String);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.String);
            sp.Command.AddParameter("@Role", CommonModel.GetUserRole(), DbType.String);
            sp.Command.AddParameter("@ParaUser", MvcApplication.CUser.Id, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GtSPPlanCalendar()
        {
            StoredProcedure sp = new StoredProcedure("SP_PlanCalendar");
            //sp.Command.AddParameter("@Sdt", Sdt, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
    }
}
