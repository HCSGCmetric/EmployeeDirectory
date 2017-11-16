using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    /// <summary>
    /// Emtity Class EmployeeDetails
    /// </summary>
    public class EmployeeDetails
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user list.
        /// </summary>
        /// <value>
        /// The user list.
        /// </value>
        public List<EmployeeDetailsList> UserList { get; set; }
    }

    public class EmployeeDetailsList 
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

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
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is add require.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is add require; otherwise, <c>false</c>.
        /// </value>
        public bool IsAddRequire { get; set; }

        /// <summary>
        /// Gets or sets the eeid.
        /// </summary>
        /// <value>
        /// The eeid.
        /// </value>
        public string EEID { get; set; }

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