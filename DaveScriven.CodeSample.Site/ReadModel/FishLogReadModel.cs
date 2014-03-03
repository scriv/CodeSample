using System.Data.Entity;

namespace DaveScriven.CodeSample.Site.ReadModel
{
    /// <summary>
    /// Represents the Fishlog read model.
    /// </summary>
    public class FishLogReadModel : DbContext, IFishLogReadModel
    {
        /// <summary>
        /// Gets or sets the catches.
        /// </summary>
        public IDbSet<Catch> Catches { get; set; }
    }
}