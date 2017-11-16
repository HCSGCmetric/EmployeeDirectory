using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class HSGUserEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the div.
        /// </summary>
        /// <value>
        /// The div.
        /// </value>
        public string Div { get; set; }

        /// <summary>
        /// Gets or sets the reg.
        /// </summary>
        /// <value>
        /// The reg.
        /// </value>
        public string Reg { get; set; }

        /// <summary>
        /// Gets or sets the dist.
        /// </summary>
        /// <value>
        /// The dist.
        /// </value>
        public string Dist { get; set; }

        /// <summary>
        /// Gets or sets the eeid.
        /// </summary>
        /// <value>
        /// The eeid.
        /// </value>
        public string EEID { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }

        public string Approve { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the termed date.
        /// </summary>
        /// <value>
        /// The termed date.
        /// </value>
        public string TermedDate { get; set; }

        /// <summary>
        /// Gets or sets the pageindex.
        /// </summary>
        /// <value>
        /// The pageindex.
        /// </value>
        public int Pageindex { get; set; }

        /// <summary>
        /// Gets or sets the total record.
        /// </summary>
        /// <value>
        /// The total record.
        /// </value>
        public int TotalRecord { get; set; }

      
    }
}