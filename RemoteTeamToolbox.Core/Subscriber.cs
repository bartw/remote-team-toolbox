using System;

namespace RemoteTeamToolbox.Core
{
    public class Subscriber
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Subscriber(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Missing or empty name argument.");
            }

            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
