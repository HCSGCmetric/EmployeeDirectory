using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    /// <summary>
    /// Class Arcust View Entity
    /// </summary>
    public class ArcustViewEntity
    {
        public string CO1 { get; set; }

        public string CO2 { get; set; }

        public string CO3 { get; set; }

        public string DIV { get; set; }

        public string REG { get; set; }

        public string DIST { get; set; }

        public string SUBREG { get; set; }

        public string SUBREG2 { get; set; }

        public string customer_code { get; set; }

        public string Department { get; set; }

        public string FacilityEmail { get; set; }

        public string START_DT { get; set; }

        public string TERM_DT { get; set; }

        public string customer_name { get; set; }

        public string AFFILIATED_CUST_CODE { get; set; }

        public string Management_Name { get; set; }

        public string addr1 { get; set; }

        public string addr2 { get; set; }

        public string addr3 { get; set; }

        public string addr4 { get; set; }

        public string addr5 { get; set; }

        public string addr6 { get; set; }

        public string addr_sort1 { get; set; }

        public string addr_sort2 { get; set; }

        public string addr_sort3 { get; set; }

        public int status_type { get; set; }

        public string attention_name { get; set; }

        public string attention_phone { get; set; }

        public string contact_name { get; set; }

        public string contact_phone { get; set; }

        public string tlx_twx { get; set; }

        public string phone_1 { get; set; }

        public string phone_2 { get; set; }

        public string ship_to_code { get; set; }

        public string tax_code { get; set; }

        public string terms_code { get; set; }

        public string fob_code { get; set; }

        public string territory_code { get; set; }

        public string COLLECTOR { get; set; }

        public string SALESPERSON { get; set; }

        public string MESSAGE_CODE { get; set; }

        public string dunn_message_code { get; set; }

        public string note { get; set; }

        public string db_num { get; set; }

        public int db_date { get; set; }

        public string db_credit_rating { get; set; }

        public int address_type { get; set; }

        public int late_chg_type { get; set; }

        public int valid_shipto_flag { get; set; }

        public int date_opened { get; set; }

        public string rate_type_home { get; set; }

        public string rate_type_oper { get; set; }

        public int limit_by_home { get; set; }

        public string nat_cur_code { get; set; }

        public int one_cur_cust { get; set; }

        public string added_by_user_name { get; set; }

        public DateTime? added_by_date { get; set; }

        public string modified_by_user_name { get; set; }

        public DateTime? modified_by_date { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string postal_code { get; set; }

        public string country { get; set; }

        public string remit_code { get; set; }

        public string forwarder_code { get; set; }

        public string freight_to_code { get; set; }

        public string route_code { get; set; }

        public int route_no { get; set; }

        public string url { get; set; }

        public string special_instr { get; set; }

        public string guid { get; set; }

        public string price_level { get; set; }

        public string ship_via_code { get; set; }

        public string dest_zone_code { get; set; }

        public string Hospital { get; set; }

        public string payment_code { get; set; }

        public string UnionBuilding { get; set; }

        public int Pageindex { get; set; }

        public int TotalRecord { get; set; }



        

        public List<ArcustViewSearchEntity> lstArcustViewSearchEntity { get; set; }
        public List<EmployeeDirectoryEntity> lstEmployeeEntity { get; set; }

    }


    public class ArcustViewSearchEntity 
    {
        public string FacilityCode { get; set; }

        public string FacilityName { get; set; }

        public string ManagementCode { get; set; }

        public string ManagementName { get; set; }

        public string TerritoryCode { get; set; }

        public string CollectorCode { get; set; }

        public string PayrollCode { get; set; }

        public string DepartmentCode { get; set; }

        public string City { get; set; }

        public string UserName { get; set; }

        public bool isFieldUser { get; set; }

        public string State { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? TermStartDate { get; set; }

        public DateTime? TermEndDate { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string Div { get; set; }

        public string Reg { get; set; }

        public string Dist { get; set; }

        public string Status { get; set; }
        
        public int RowStart { get; set; }

        public int RowEnd { get; set; }

        public string Sort { get; set; }

        public string SortDirection { get; set; }

        public int Pageindex { get; set; }

        public int PageSize { get; set; }

        public string DistinctManagerName { get; set; }

        public string RegionManagerName { get; set; }

        public string DivisionalManagerName { get; set; }

        public string AccountManagerName { get; set; }
    }
}