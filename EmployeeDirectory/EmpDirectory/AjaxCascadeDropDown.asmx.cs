using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace EmployeeDirectory.EmpDirectory
{
    /// <summary>
    /// Summary description for AjaxCascadeDropDown
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AjaxCascadeDropDown : System.Web.Services.WebService
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HSGOPSConnectionString"].ToString());

        //public AjaxCascadingDropDown()
        //{

        //    //Uncomment the following line if using designed components
        //    //InitializeComponent();
        //}


        [WebMethod]
        public CascadingDropDownNameValue[] FetchDivision(string knownCategoryValues, string category)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select distinct div as division from HSG_ARCUST_VW where div is not null order by div", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            con.Close();

            List<CascadingDropDownNameValue> divisions = new List<CascadingDropDownNameValue>();
            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                string DivisionID = dtRow["division"].ToString();
                string DivisionName = dtRow["division"].ToString();
                divisions.Add(new CascadingDropDownNameValue(DivisionName, DivisionID));
            }
            return divisions.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] FetchRegion(string knownCategoryValues, string category)
        {
            string division;
            StringDictionary strDivisions = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            division = strDivisions["division"];
            con.Open();
            SqlCommand cmd = new SqlCommand("select distinct reg as region from HSG_ARCUST_VW where reg is not null and div = @division order by reg", con);
            cmd.Parameters.AddWithValue("@division", division);
            cmd.ExecuteNonQuery();
            SqlDataAdapter dastate = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dastate.Fill(ds);
            con.Close();
            List<CascadingDropDownNameValue> regions = new List<CascadingDropDownNameValue>();
            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                string RegionID = dtRow["region"].ToString();
                string RegionName = dtRow["region"].ToString();
                regions.Add(new CascadingDropDownNameValue(RegionName, RegionID));
            }
            return regions.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] FetchDistrict(string knownCategoryValues, string category)
        {
            string region;
            StringDictionary strRegions = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            region = strRegions["region"];
            con.Open();
            SqlCommand cmd = new SqlCommand("select distinct dist as district from HSG_ARCUST_VW where dist is not null and reg = @region order by dist", con);
            cmd.Parameters.AddWithValue("@region", region);
            cmd.ExecuteNonQuery();
            SqlDataAdapter daregion = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            daregion.Fill(ds);
            con.Close();
            List<CascadingDropDownNameValue> districts = new List<CascadingDropDownNameValue>();
            foreach (DataRow dtRow in ds.Tables[0].Rows)
            {
                string DistrictID = dtRow["district"].ToString();
                string DistrictName = dtRow["district"].ToString();
                districts.Add(new CascadingDropDownNameValue(DistrictName, DistrictID));
            }
            return districts.ToArray();
        }

    }
}
