using System;
using System.ComponentModel.DataAnnotations;

namespace DaveScriven.CodeSample.Site.ReadModel
{
    /// <summary>
    /// Represents a logged catch.
    /// </summary>
    public class Catch
    {
        /// <summary>
        /// Gets or sets the catch ID.
        /// </summary>
        public Guid CatchId { get; set; }

        /// <summary>
        /// Gets or sets the species.
        /// </summary>
        [Required]
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [Required]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [Required]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        [Required]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        [Required]
        public double Depth { get; set; }

        /// <summary>
        /// Gets or sets the likes.
        /// </summary>
        public int Likes { get; set; }
    }
}