
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Business
{
    public class DocumentDisplayBusiness
    {

        public DocumentDisplayBusiness()
        {
           DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
           //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }


        public List<DocumentDisplayEntity> GetAllDocumentDisplayList(string Type)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@Type", DbType.String, Type);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllDocumentDisplayList", parameter);

            List<DocumentDisplayEntity> listDocumentDisplayEntity = new List<DocumentDisplayEntity>();
            if (dt.Rows.Count > 0)
            {
                listDocumentDisplayEntity = dt.DataTableToList<DocumentDisplayEntity>();
            }
            return listDocumentDisplayEntity;

        }




    }
}