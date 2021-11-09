using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SingleResponsibility_Principle
{
    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("SOLID is an acronym.");
            j.AddEntry("We are creating sample code for Single Responsibility Principle.");
            Console.WriteLine(j);

            var p = new Persistence();
            //var filename = @"D:\temp\journal.txt";
            var filename = string.Format(@"{0}\journal.txt", Environment.CurrentDirectory);
            p.SaveToFile(j, filename);

            //Process.Start(filename);  -- wont work in .Net core because UseShellExecute is false by default.

            ProcessStartInfo startInfo = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }
    }

    // just stores a couple of journal entries and ways of
    // working with them
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add(($"{++count}: {text}"));
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        // breaks single responsibility principle
        public void Save(string filename, bool overwrite = false)
        {
            File.WriteAllText(filename, ToString());
        }

        public void Load(string filename)
        {

        }

        public void Load(Uri uri)
        {

        }
    }

    // handles the responsibility of persisting objects
    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, journal.ToString());
        }
    }

}
