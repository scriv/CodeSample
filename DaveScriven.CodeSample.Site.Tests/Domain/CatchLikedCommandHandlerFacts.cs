using DaveScriven.CodeSample.Domain;
using DaveScriven.CodeSample.Domain.CommandHandlers;
using DaveScriven.CodeSample.Domain.Commands;
using Moq;
using NUnit.Framework;
using SimpleCqrs;
using System;

namespace DaveScriven.CodeSample.Site.Tests.Domain
{
    public class CatchLikedCommandHandlerFacts
    {
        public class HandleMethod
        {
            [Test]
            public void Likes_the_matching_catch()
            {
                // Arrange
                ServiceLocator.SetCurrent(new Mock<IServiceLocator>().Object);
                var command = new CatchLikedCommand { AggregateRootId = Guid.NewGuid() };
                var handler = new CatchLikedCommandHandler();
                var catchMock = new Mock<Catch>();

                // Act
                handler.Handle(command, catchMock.Object);

                // Assert
                catchMock.Verify(c => c.Like(), "The catch was not liked.");
            }
        }
    }
}