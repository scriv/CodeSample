using DaveScriven.CodeSample.Site.Commands;
using DaveScriven.CodeSample.Site.Domain;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace DaveScriven.CodeSample.Site.Domain.CommandHandlers
{
    /// <summary>
    /// Handles <see cref="CatchLoggedCommand"/>s.
    /// </summary>
    public class CatchLoggedCommandHandler : CommandHandler<CatchLoggedCommand>
    {
        private readonly IDomainRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatchLoggedCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">The domain repository.</param>
        public CatchLoggedCommandHandler(IDomainRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public override void Handle(CatchLoggedCommand command)
        {
            var @catch = new Catch(command.Species, command.Length, command.Depth, command.Latitude, command.Longitude);

            repository.Save(@catch);
        }
    }
}