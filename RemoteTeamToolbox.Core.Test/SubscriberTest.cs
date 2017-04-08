using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RemoteTeamToolbox.Core.Test
{
    public class UserTest
    {
        [Fact]
        public void GivenNameThatIsNull_WhenConstructingAUser_ThenAnArgumentExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Subscriber(null));
            Assert.Equal("Missing or empty name argument.", exception.Message);
        }

        [Fact]
        public void GivenNameThatIsEmpty_WhenConstructingAUser_ThenAnArgumentExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Subscriber(""));
            Assert.Equal("Missing or empty name argument.", exception.Message);
        }

        [Fact]
        public void GivenAValidName_WhenConstructingAUser_ThenAUserWithThatNameIsCreated()
        {
            var name = "Otis";

            var user = new Subscriber(name);

            Assert.Equal(name, user.Name);
        }

        [Fact]
        public void GivenValidParameters_WhenConstructingUsers_ThenUsersWithUniqueIdsAreCreated()
        {
            var userCount = 100;
            var users = new List<Subscriber>();
            for (int i = 0; i < userCount; i++)
            {
                users.Add(new Subscriber("a name"));
            }
            
            Assert.Equal(userCount, users.Select(u => u.Id).Distinct().Count());
        }
    }
}
