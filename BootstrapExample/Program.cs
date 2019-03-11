using System;
using Starcounter;

namespace BootstrapExample
{
    class Program
    {
        static void Main()
        {
            CustomAppShellBootstrapper.Start(new Startup());
        }
    }
}