using System.Collections.Generic;

namespace Wonders.IntegrationTests
{
    public class InMemoryLogStore
    {
        private static InMemoryLogStore _instance;
        private static readonly object _lock = new object();
        private InMemoryLogStore()
        {
            Logs = new List<string>();
        }

        public List<string> Logs { get; }

        public void AddEntry(string message)
        {
            Logs.Add(message);
        }

        public static InMemoryLogStore Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (_lock)
                {
                    _instance = new InMemoryLogStore();
                }
                return _instance;
            }
        }
    }
}