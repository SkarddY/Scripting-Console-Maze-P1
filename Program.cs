using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime;

namespace ScriptingParcial1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch cTimer = new Stopwatch(); SearchPath Progam = new SearchPath();
            Progam.Maze(); cTimer.Start();
            Progam.BFS(); Progam.CreatePath();
            Progam.ShowPath(); cTimer.Stop();
            TimeSpan time = cTimer.Elapsed;
            string timer = string.Format("Elapsed Time: " + time.ToString("mm\\:ss\\.ff"));
            Console.WriteLine(timer);
        }
    }
}
