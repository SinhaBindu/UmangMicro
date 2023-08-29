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
    }
}
