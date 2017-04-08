using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RemoteTeamToolbox.Core.Test
{
    public class SessionTest
    {
        [Fact]
        public void GivenAnAdminThatIsNull_WhenConstructingASession_ThenAnArgumentExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Session(null));
            Assert.Equal("Missing admin.", exception.Message);
        }

        [Fact]
        public void GivenAnAdmin_WhenConstructingASession_ThenASessionWithThatAdminIsCreated()
        {
            var admin = new Subscriber("Otis");

            var session = new Session(admin);

            Assert.Equal(admin, session.Admin);
        }

        [Fact]
        public void GivenAnAdmin_WhenConstructingASession_ThenTheAdminIsTheOnlyMemberOfTheSubsribers()
        {
            var admin = new Subscriber("Otis");

            var session = new Session(admin);

            Assert.Single(session.Subscribers);
            Assert.Equal(session.Admin, session.Subscribers.First());
        }

        [Fact]
        public void GivenValidParameters_WhenConstructingSessions_ThenSessionsWithUniqueIdsAreCreated()
        {
            var admin = new Subscriber("Otis");
            var sessionCount = 100;
            var sessions = new List<Session>();
            for (int i = 0; i < sessionCount; i++)
            {
                sessions.Add(new Session(admin));
            }
            
            Assert.Equal(sessionCount, sessions.Select(u => u.Id).Distinct().Count());
        }

        [Fact]
        public void GivenASessionAndASubscriber_WhenSubscribe_ThenTheSubscriberIsAddedToTheSubscribers()
        {
            var admin = new Subscriber("Otis");
            var session = new Session(admin);
            var subscriber = new Subscriber("Linus");

            session.Subscribe(subscriber);

            Assert.Equal(2, session.Subscribers.Count());
            Assert.True(session.Subscribers.All(s => s == admin || s == subscriber));
        }
    }
}
