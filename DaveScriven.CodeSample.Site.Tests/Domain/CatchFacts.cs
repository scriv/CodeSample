using DaveScriven.CodeSample.Site.Domain;
using DaveScriven.CodeSample.Site.Domain.Events;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaveScriven.CodeSample.Site.Tests.Domain
{
    public class CatchFacts
    {
        public class OnCatchLikedMethod
        {
            [Test]
            public void Increments_the_number_of_likes_by_one()
            {
                // Arrange
                var @catch = new Catch();
                int likesCount = @catch.Likes;

                // Act
                @catch.OnCatchLiked(new CatchLikedEvent());

                // Assert
                Assert.That(@catch.Likes, Is.EqualTo(likesCount + 1));
            }
        }

        public class Constructor
        {
            [Test]
            public void Throws_if_no_species_is_set()
            {
                // Act/Assert
                Assert.Throws<ArgumentNullException>(() => new Catch(string.Empty, 10, 10, 10, 10));
            }

            [Test]
            public void Throws_if_no_length_is_set()
            {
                // Act/Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => new Catch("Test", 0, 10, 10, 10));
            }

            [Test]
            public void Throws_if_no_depth_is_set()
            {
                // Act/Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => new Catch("Test", 10, 0, 10, 10));
            }
        }
    }
}