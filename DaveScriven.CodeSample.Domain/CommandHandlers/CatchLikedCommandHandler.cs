using DaveScriven.CodeSample.Domain.Commands;
using DaveScriven.CodeSample.Domain;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace DaveScriven.CodeSample.Domain.CommandHandlers
{
    /// <summary>
    /// Handles the <see cref="CatchLikedCommand"/>.
    /// </summary>
    public class CatchLikedCommandHandler : AggregateRootCommandHandler<CatchLikedCommand, Catch>
    {
        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="catch">The catch.</param>
        public override void Handle(CatchLikedCommand command, Catch @catch)
        {
            @catch.Like();
        }
    }
}