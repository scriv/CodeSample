using SimpleCqrs.Commanding;
using System;

namespace DaveScriven.CodeSample.Domain.Commands
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