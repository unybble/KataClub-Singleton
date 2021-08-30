using System;
namespace Singleton
{
    // Bad code! Do not use!
    public sealed class Singleton1
    {
        private static Singleton1 instance = null;

        private Singleton1()
        {
        }

        public static Singleton1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton1();
                }
                return instance;
            }
        }
    }

    public sealed class Singleton2
    {
        private static Singleton2 instance = null;
        private static readonly object padlock = new object();

        Singleton2()
        {
        }

        public static Singleton2 Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton2();
                    }
                    return instance;
                }
            }
        }
    }
    public sealed class Singleton3
    {
        private static readonly Singleton3 instance = new Singleton3();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Singleton3()
        {
        }

        private Singleton3()
        {
        }

        public static Singleton3 Instance
        {
            get
            {
                return instance;
            }
        }
    }

    public sealed class Singleton
    {
        private Singleton()
        {
        }

        public static Singleton Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Singleton instance = new Singleton();
        }
    }
}
