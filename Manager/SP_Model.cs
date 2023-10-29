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
        public static DataTable SPCandRegList(string SD, string ED)
        {
            StoredProcedure sp = new StoredProcedure("SP_RegList");
            sp.Command.AddParameter("@FD", SD, DbType.String);
            sp.Command.AddParameter("@TD", ED, DbType.String);
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
        public static DataTable GetSP_StudentList(string Para, string SearchBy)
        {
            StoredProcedure sp = new StoredProcedure("SP_StudentList");
            sp.Command.AddParameter("@Para", Para, DbType.String);
            sp.Command.AddParameter("@SearchBy", SearchBy, DbType.String);
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
        public static DataTable GetSP_RIASECGuidData(string Para)
        {
            StoredProcedure sp = new StoredProcedure("SP_RIASEC_GuidedList");
            sp.Command.AddParameter("@Para", Para, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
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
    }
}
