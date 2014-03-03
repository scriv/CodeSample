using DaveScriven.CodeSample.Site.Domain.Events;
using SimpleCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaveScriven.CodeSample.Site.Domain
{
    /// <summary>
    /// Represents a fishing catch.
    /// </summary>
    public class Catch : AggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Catch"/> class.
        /// </summary>
        public Catch()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Catch"/> class.
        /// </summary>
        /// <param name="species">The species.</param>
        /// <param name="length">The length.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public Catch(string species, int length, double depth, double latitude, double longitude)
        {
            if (string.IsNullOrEmpty(species))
            {
                throw new ArgumentNullException("species");
            }

            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            if (depth <= 0)
            {
                throw new ArgumentOutOfRangeException("depth");
            }

            this.Apply(new CatchLoggedEvent { AggregateRootId = Guid.NewGuid(), Species = species, Length = length, Depth = depth, Latitude = latitude, Longitude = longitude });
        }

        /// <summary>
        /// Gets or sets the number of likes.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Gets or sets the species.
        /// </summary>
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        public double Depth { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Likes the catch.
        /// </summary>
        public void Like()
        {
            this.Apply(new CatchLikedEvent { AggregateRootId = this.Id });
        }

        /// <summary>
        /// Handles the <see cref="CatchLoggedEvent"/>.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        public void OnCatchLogged(CatchLoggedEvent domainEvent)
        {
            this.Id = domainEvent.AggregateRootId;
            this.Species = domainEvent.Species;
            this.Length = domainEvent.Length;
            this.Depth = domainEvent.Depth;
            this.Longitude = domainEvent.Longitude;
            this.Latitude = domainEvent.Latitude;
        }

        /// <summary>
        /// Handles the <see cref="CatchLikedEvent"/>.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        public void OnCatchLiked(CatchLikedEvent domainEvent)
        {
            this.Likes += 1;
        }
    }
}