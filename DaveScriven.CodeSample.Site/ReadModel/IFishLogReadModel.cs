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
        /// Gets the application's statistics.
        /// </summary>
        IDbSet<Stats> Statistics { get; }

        /// <summary>
        /// Saves any changes.
        /// </summary>
        int SaveChanges();
    }
}