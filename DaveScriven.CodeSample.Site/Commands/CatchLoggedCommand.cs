using SimpleCqrs.Commanding;

namespace DaveScriven.CodeSample.Site.Commands
{
    /// <summary>
    /// Indicates that a new catch should be logged.
    /// </summary>
    public class CatchLoggedCommand : ICommand
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