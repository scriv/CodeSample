using DaveScriven.CodeSample.Site.Domain.Denormalizers;
using DaveScriven.CodeSample.Site.Domain.Events;
using DaveScriven.CodeSample.Site.ReadModel;
using Moq;
using NUnit.Framework;
using SimpleCqrs.Domain;
using System;
using System.Data.Entity;

namespace DaveScriven.CodeSample.Site.Tests.Domain
{
    public class CatchesReadModelDenormalizerFacts
    {
        public class HandleCatchLoggedEventMethod
        {
            [Test]
            public void Adds_a_new_catch_to_the_read_model()
            {
                // Arrange
                var readModelMock = new Mock<IFishLogReadModel>();
                var dbSetMock = new Mock<IDbSet<Catch>>();
                var domainRepositoryDummy = new Mock<IDomainRepository>().Object;

                readModelMock.Setup(m => m.Catches).Returns(dbSetMock.Object);

                var catchLoggedEvent = new CatchLoggedEvent { Species = "Test", Depth = 10, Length = 100, Latitude = 10, Longitude = 10 };
                var denormalizer = new CatchesReadModelDenormalizer(readModelMock.Object, domainRepositoryDummy);

                // Act
                denormalizer.Handle(catchLoggedEvent);

                // Assert
                dbSetMock.Verify(s => s.Add(It.Is<Catch>(c => 
                    c.Species == catchLoggedEvent.Species && 
                    c.Length == catchLoggedEvent.Length && 
                    c.Depth == catchLoggedEvent.Depth &&
                    c.Latitude == catchLoggedEvent.Latitude &&
                    c.Longitude == catchLoggedEvent.Longitude)), "The saved catch does not match the catch from the event.");

                readModelMock.Verify(m => m.SaveChanges(), "The changes were not saved to the read model.");
            }
        }

        public class HandleCatchLikedEventMethod
        {
            [Test]
            public void Sets_the_catch_read_model_likes_value_to_that_of_the_domain_model()
            {
                // Arrange
                var catchLikedEvent = new CatchLikedEvent { AggregateRootId = Guid.NewGuid() };
                var associatedCatch = new DaveScriven.CodeSample.Site.Domain.Catch { Likes = 10 };
                var catchReadModel = new Catch { CatchId = catchLikedEvent.AggregateRootId, Likes = 9 };

                var readModelMock = new Mock<IFishLogReadModel>();
                var dbSetMock = new Mock<IDbSet<Catch>>();
                var domainRepositoryStub = new Mock<IDomainRepository>();

                readModelMock.Setup(m => m.Catches).Returns(dbSetMock.Object);

                domainRepositoryStub.Setup(r => r.GetById<DaveScriven.CodeSample.Site.Domain.Catch>(catchLikedEvent.AggregateRootId)).Returns(associatedCatch);
                dbSetMock.Setup(s => s.Find(catchLikedEvent.AggregateRootId)).Returns(catchReadModel);

                var denormalizer = new CatchesReadModelDenormalizer(readModelMock.Object, domainRepositoryStub.Object);

                // Act
                denormalizer.Handle(catchLikedEvent);

                // Assert
                Assert.That(catchReadModel.Likes, Is.EqualTo(associatedCatch.Likes));
                readModelMock.Verify(m => m.SaveChanges(), "The changes were not saved to the read model.");
            }
        }
    }
}