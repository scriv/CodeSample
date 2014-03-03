using System.Data.Entity;

namespace DaveScriven.CodeSample.Site.ReadModel
{
    /// <summary>
    /// Provides read-model querying and updating support.
    /// </summary>
    public interface IFishLogReadModel
    {
        /// <summary>
        /// Gets the catches.
        /// </summary>
        IDbSet<Catch> Catches { get; }

        /// <summary>
        /// Saves any changes.
        /// </summary>
        int SaveChanges();
    }
}