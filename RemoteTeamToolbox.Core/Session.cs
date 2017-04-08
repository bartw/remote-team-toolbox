using System;
using System.Collections.Generic;

namespace RemoteTeamToolbox.Core
{
    public class Session
    {
        private List<Subscriber> _subscribers;
        public Guid Id { get; private set; }
        public Subscriber Admin { get; private set; }
        public IEnumerable<Subscriber> Subscribers
        {
            get
            {
                return _subscribers;
            }
        }

        public Session(Subscriber admin)
        {
            if (admin == null)
            {
                throw new ArgumentException("Missing admin.");
            }

            Id = Guid.NewGuid();
            Admin = admin;
            _subscribers = new List<Subscriber> { Admin };
        }

        public void Subscribe(Subscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }
    }
}
