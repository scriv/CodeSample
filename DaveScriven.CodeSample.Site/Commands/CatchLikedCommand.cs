using SimpleCqrs.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaveScriven.CodeSample.Site.Commands
{
    /// <summary>
    /// Indicates that a catch should be liked.
    /// </summary>
    public class CatchLikedCommand : ICommandWithAggregateRootId
    {
        /// <summary>
        /// Gets or sets the catch identifier.
        /// </summary>
        public Guid AggregateRootId { get; set; }
    }
}