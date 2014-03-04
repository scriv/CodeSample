using DaveScriven.CodeSample.Site.ReadModel;
using System.Linq;
using System.Web.Mvc;

namespace DaveScriven.CodeSample.Site.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IFishLogReadModel readModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="readModel">The read model.</param>
        public DashboardController(IFishLogReadModel readModel)
        {
            this.readModel = readModel;
        }

        public ActionResult Index()
        {
            var stats = this.readModel.Statistics.FirstOrDefault();
            int totalCatches = stats != null ? stats.TotalCatches : 0;

            ViewBag.TotalCatches = totalCatches;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}