using HackerRank.ProblemsSolved;
using System.Drawing;

namespace HackerRank
{
    internal static class Extension
    {
        internal static void MinimumMoves(
            this SquareBoard move
            , string[] grid
            , int startRow
            , int startCol
            , int goalRow
            , int goalCol)
        {
            var n = grid.Length;
            for (int y = startCol; y < n; ++y)
            {
                
            }

            var xStart = startCol <= goalCol ? startCol : goalCol;
            var yStart = startRow <= goalRow ? startRow : goalRow;

            var xEnd = xStart == startCol ? goalCol : startCol;
            var yEnd = xEnd == startRow ? goalRow : startRow;

            for (var y = yStart; y < yEnd; ++y)
            {
                for(var x = xStart; x < xEnd; ++x)
                {

                }
            }

            //var directionVert =
            var start = new Point(startCol, startRow);
            var goal = new Point(goalCol, goalRow);
            var current = start;
            var goalReached = false;
            while (!goalReached)
            {
                
            }
        }

        //public static string Direction()
        //{

        //}
    }
}
