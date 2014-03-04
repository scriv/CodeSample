using DaveScriven.CodeSample.Domain.CommandHandlers;
using DaveScriven.CodeSample.Domain.Commands;
using DaveScriven.CodeSample.Domain;
using Moq;
using NUnit.Framework;
using SimpleCqrs.Domain;

namespace DaveScriven.CodeSample.Site.Tests.Domain
{
    public class CatchLoggedCommandHandlerFacts
    {
        public class HandleMethod
        {
            [Test]
            public void Saves_a_complete_new_catch()
            {
                // Arrange
                var domainRepositoryMock = new Mock<IDomainRepository>();
                var handler = new CatchLoggedCommandHandler(domainRepositoryMock.Object);
                var command = new CatchLoggedCommand { Species = "Snapper", Depth = 17, Length = 98, Latitude = -38.011989, Longitude = 145.034723 };

                // Act
                handler.Handle(command);

                // Assert
                domainRepositoryMock.Verify(r => r.Save(It.Is<Catch>(c => 
                    c.Species == command.Species && 
                    c.Depth == command.Depth && 
                    c.Length == command.Length &&
                    c.Latitude == command.Latitude &&
                    c.Longitude == command.Longitude)), "A domain catch correctly representing the command was not saved.");
            }
        }
    }
}