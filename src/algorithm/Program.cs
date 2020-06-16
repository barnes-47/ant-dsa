using System;
using System.Collections.Generic;
using static System.Console;

namespace Algo
{
    class Program
    {
        #region Private Constants
        private const ConsoleColor DARKGREEN = ConsoleColor.DarkGreen;
        private const ConsoleColor DARKYELLOW = ConsoleColor.DarkYellow;
        private const ConsoleColor DEFAULT = ConsoleColor.Gray;
        private const ConsoleColor GREEN = ConsoleColor.Green;
        private const ConsoleColor RED = ConsoleColor.Red;
        private const ConsoleColor WHITE = ConsoleColor.White;
        private const ConsoleColor YELLOW = ConsoleColor.Yellow;
        #endregion

        static void Main(string[] args)
        {
            string[] columnHeadings = { "Binary", "Interpolation", "Jump", "Linear" };

            SetTextColorTo(WHITE);
            WriteHeading("--- SEARCH ALGORITHMS ---");
            WriteColumnHeadings(columnHeadings);


            ReadKey();
        }

        #region Private Methods
        private static void InsertSpaces()
            => InsertSpaces(2);
        private static void InsertSpaces(int count)
        {
            while (0 < count--) { Write(" "); }
        }
        private static void InsertTabs()
            => InsertTabs(1);
        private static void InsertTabs(int count)
        {
            while (0 < count--) { Write("\t"); }
        }
        private static void SetTextColorTo(ConsoleColor color)
            => ForegroundColor = color;
        private static void WriteColumnHeadings(string[] headings)
        {
            InsertTabs();
            var et = headings.GetEnumerator();
            while (et.MoveNext())
            {
                Write(et.Current);
                InsertSpaces();
            }
            WriteLine();
        }
        private static void WriteHeading(string heading)
        {
            InsertTabs(2);
            Write($"{heading}");
            WriteLine();
            WriteLine();
        }

        #endregion
    }
}
