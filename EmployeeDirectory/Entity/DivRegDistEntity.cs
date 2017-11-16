using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class DivRegDistEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string EEID { get; set; }

        /// <summary>
        /// Gets or sets the row number.
        /// </summary>
        /// <value>
        /// The row number.
        /// </value>
        public int RowNumber { get; set; }

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

        public string Approve { get; set; }

    }
}