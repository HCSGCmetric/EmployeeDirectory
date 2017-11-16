using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Helper
{
    public static class ConfigurationProvider
    {
        /// <summary>
        /// Gets the pageindex.
        /// </summary>
        /// <value>
        /// The pageindex.
        /// </value>
        public static int Pageindex
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the size of the weg grid page.
        /// </summary>
        /// <value>
        /// The size of the weg grid page.
        /// </value>
        public static int WegGridPageSize
        {
            get
            {
                //int pageSize = 0;
                //SytemConfigurationMasterModel sytemConfigurationMasterModel = CacheHelper.Get<SytemConfigurationMasterModel>(CacheKeys.SystemConfiguration);
                //if (sytemConfigurationMasterModel == null)
                //{
                //    return 10;
                //}

                //pageSize = Convert.ToInt32(sytemConfigurationMasterModel.SystemConfig.PageSize);
                //return pageSize <= 0 ? 10 : pageSize;
                //return 10;
                return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            }
        
        }

        public static int InvalidAttamptCounter
        {
            get
            {
                int InvalidAttamptCounter = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["InvalidAttamptCounter"]);
                return InvalidAttamptCounter > 0 ? InvalidAttamptCounter : 5;
            }
        }
    }
}