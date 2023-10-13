using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static DataSet GetSP_MicroCaseList(int Para,int CategId=0)
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
    }
}
