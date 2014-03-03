using SimpleCqrs.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaveScriven.CodeSample.Site.Domain.Events
{
    /// <summary>
    /// Indicates that a new catch has been logged.
    /// </summary>
    public class CatchLoggedEvent : DomainEvent
    {
        /// <summary>
        /// Gets or sets the species caught.
        /// </summary>
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets the length of the creature caught.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the depth that the creature was caught in.
        /// </summary>
        public double Depth { get; set; }

        /// <summary>
        /// Gets or sets the latitude that the creature was caught at.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude that the creature was caught at.
        /// </summary>
        public double Longitude { get; set; }
    }
}