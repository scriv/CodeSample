using AutoMapper;
using DaveScriven.CodeSample.Site.Commands;
using DaveScriven.CodeSample.Site.ReadModel;
using SimpleCqrs.Commanding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaveScriven.CodeSample.Site.Api
{
    /// <summary>
    /// Provides API functionality for catches.
    /// </summary>
    public class CatchesController : ApiController
    {
        private readonly ICommandBus commandBus;
        private readonly IFishLogReadModel readModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatchesController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="readModel">The read model.</param>
        public CatchesController(ICommandBus commandBus, IFishLogReadModel readModel)
        {
            this.commandBus = commandBus;
            this.readModel = readModel;
        }

        /// <summary>
        /// Gets all catches.
        /// </summary>
        public IEnumerable<Catch> GetAllCatches()
        {
            return this.readModel.Catches;
        }

        /// <summary>
        /// Likes a catch.
        /// </summary>
        /// <param name="id">The catch ID.</param>
        [HttpPost]
        [ActionName("like")]
        public HttpResponseMessage LikeCatch(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            this.commandBus.Execute(new CatchLikedCommand { AggregateRootId = id });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Creates a new catch.
        /// </summary>
        /// <param name="catch">The catch to be created.</param>
        public HttpResponseMessage Post(Catch @catch)
        {
            if (!ModelState.IsValid)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            this.commandBus.Execute(Mapper.Map<CatchLoggedCommand>(@catch));

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}