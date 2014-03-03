using DaveScriven.CodeSample.Site.Domain.Events;
using DaveScriven.CodeSample.Site.ReadModel;
using SimpleCqrs.Domain;
using SimpleCqrs.Eventing;

namespace DaveScriven.CodeSample.Site.Domain.Denormalizers
{
    /// <summary>
    /// Handles catch related events to ensure that the catch read model is up to date.
    /// </summary>
    public class CatchesReadModelDenormalizer : IHandleDomainEvents<CatchLoggedEvent>, IHandleDomainEvents<CatchLikedEvent>
    {
        private readonly IFishLogReadModel readModel;
        private readonly IDomainRepository domainRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatchesReadModelDenormalizer" /> class.
        /// </summary>
        /// <param name="readModel">The read model.</param>
        /// <param name="domainRepository">The domain repository.</param>
        public CatchesReadModelDenormalizer(IFishLogReadModel readModel, IDomainRepository domainRepository)
        {
            this.readModel = readModel;
            this.domainRepository = domainRepository;
        }

        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        public void Handle(CatchLoggedEvent domainEvent)
        {
            this.readModel.Catches.Add(new ReadModel.Catch
                {
                    CatchId = domainEvent.AggregateRootId,
                    Species = domainEvent.Species,
                    Depth = domainEvent.Depth,
                    Latitude = domainEvent.Latitude,
                    Longitude = domainEvent.Longitude,
                    Length = domainEvent.Length
                });

            this.readModel.SaveChanges();
        }

        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        public void Handle(CatchLikedEvent domainEvent)
        {
            ReadModel.Catch @catch = this.readModel.Catches.Find(domainEvent.AggregateRootId);

            @catch.Likes = domainRepository.GetById<Catch>(domainEvent.AggregateRootId).Likes;

            this.readModel.SaveChanges();
        }
    }
}