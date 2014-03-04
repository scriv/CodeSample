using DaveScriven.CodeSample.Domain.Events;
using DaveScriven.CodeSample.Data;
using SimpleCqrs.Eventing;
using System.Linq;

namespace DaveScriven.CodeSample.Domain.Denormalizers
{
    /// <summary>
    /// Updates the statistics read model.
    /// </summary>
    public class StatisticsDenormalizer : IHandleDomainEvents<CatchLoggedEvent>
    {
        private readonly IFishLogReadModel readModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsDenormalizer"/> class.
        /// </summary>
        /// <param name="readModel">The read model.</param>
        public StatisticsDenormalizer(IFishLogReadModel readModel)
        {
            this.readModel = readModel;
        }

        /// <summary>
        /// Handles the specified <see cref="CatchLoggedEvent"/> domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        public void Handle(CatchLoggedEvent domainEvent)
        {
            if (!this.readModel.Statistics.Any())
            {
                this.readModel.Statistics.Add(new Stats { TotalCatches = 1 });
            }
            else
            {
                var existing = this.readModel.Statistics.First();
                existing.TotalCatches = existing.TotalCatches + 1;
            }

            this.readModel.SaveChanges();
        }
    }
}