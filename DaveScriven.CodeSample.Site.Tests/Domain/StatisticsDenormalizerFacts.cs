using DaveScriven.CodeSample.Site.Domain.Denormalizers;
using DaveScriven.CodeSample.Site.Domain.Events;
using DaveScriven.CodeSample.Site.ReadModel;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace DaveScriven.CodeSample.Site.Tests.Domain
{
    public class StatisticsDenormalizerFacts
    {
        public class HandleCatchLoggedEventMethod
        {
            [Test]
            public void Adds_a_new_statistics_record_if_none_exists()
            {
                // Arrange
                var statsStub = new FakeDbSet<Stats>();
                var readModelMock = new Mock<IFishLogReadModel>();

                readModelMock.Setup(m => m.Statistics).Returns(statsStub);

                var denormalizer = new StatisticsDenormalizer(readModelMock.Object);

                // Act
                denormalizer.Handle(new CatchLoggedEvent());

                // Assert
                Assert.That(statsStub.Any());
                Assert.That(statsStub.First().TotalCatches, Is.EqualTo(1));
                readModelMock.Verify(m => m.SaveChanges(), "No changes were saved.");
            }

            [Test]
            public void Updates_existing_statistics_record_if_one_exists()
            {
                // Arrange
                var statsStub = new FakeDbSet<Stats>();
                statsStub.Add(new Stats { TotalCatches = 1 });

                var readModelMock = new Mock<IFishLogReadModel>();
                readModelMock.Setup(m => m.Statistics).Returns(statsStub);

                var denormalizer = new StatisticsDenormalizer(readModelMock.Object);

                // Act
                denormalizer.Handle(new CatchLoggedEvent());

                // Assert
                Assert.That(statsStub.First().TotalCatches, Is.EqualTo(2), "The total number of catches was not incremented.");
                readModelMock.Verify(m => m.SaveChanges(), "No changes were saved.");
            }

            #region Support classes

            public class FakeDbSet<T> : IDbSet<T> where T : class
            {
                ObservableCollection<T> _data;
                IQueryable _query;

                public FakeDbSet()
                {
                    _data = new ObservableCollection<T>();
                    _query = _data.AsQueryable();
                }

                public virtual T Find(params object[] keyValues)
                {
                    throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
                }

                public T Add(T item)
                {
                    _data.Add(item);
                    return item;
                }

                public T Remove(T item)
                {
                    _data.Remove(item);
                    return item;
                }

                public T Attach(T item)
                {
                    _data.Add(item);
                    return item;
                }

                public T Detach(T item)
                {
                    _data.Remove(item);
                    return item;
                }

                public T Create()
                {
                    return Activator.CreateInstance<T>();
                }

                public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
                {
                    return Activator.CreateInstance<TDerivedEntity>();
                }

                public ObservableCollection<T> Local
                {
                    get { return _data; }
                }

                Type IQueryable.ElementType
                {
                    get { return _query.ElementType; }
                }

                System.Linq.Expressions.Expression IQueryable.Expression
                {
                    get { return _query.Expression; }
                }

                IQueryProvider IQueryable.Provider
                {
                    get { return _query.Provider; }
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return _data.GetEnumerator();
                }

                IEnumerator<T> IEnumerable<T>.GetEnumerator()
                {
                    return _data.GetEnumerator();
                }
            }

            #endregion
        }
    }
}