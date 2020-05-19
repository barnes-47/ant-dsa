using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HackerRank.ProblemsSolved
{
    public static class UsingQueue
    {
        public static void MinimumMoves()
        {
            //var n = Convert.ToInt32(Console.ReadLine());
            //var grid = new string[n];
            //for (int i = 0; i < n; i++)
            //{
            //    grid[i] = Console.ReadLine();
            //}

            //var startXStartY = Console.ReadLine().Split(' ');
            var startX = 2;/*Convert.ToInt32(startXStartY[0]);*/
            var startY = 2;/*Convert.ToInt32(startXStartY[1]);*/
            var goalX = 0;/*Convert.ToInt32(startXStartY[2]);*/
            var goalY = 0;/*Convert.ToInt32(startXStartY[3]);*/

            var grid = new string[3] { ".X.", ".X.", "..." };
            var startingPoint = new Point(startX, startY);
            var endingPoint = new Point(goalX, goalY);
            var move = new SquareBoard(grid, endingPoint, startingPoint);
            var moves = move.MovesTakenToReachFromStartToEnd();
            //var moves = move.MovesTakenToReachFromStartToEnd(startingPoint, endingPoint);
            //move.MinimumMoves(grid, startX, startY, goalX, goalY);
        }
    }
    internal class SquareBoard
    {
        #region Private Variables
        private readonly Queue<Point> _points;
        private readonly Queue<Point> _blockers;
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gives the count of the total squares that exists on the square board.
        /// If a square board object has size = 3, than count = 9 (size * size).
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; }
        public Point End { get; }
        public Point Start { get; }
        /// <summary>
        /// The size of one side of the square.
        /// </summary>
        /// <value>The size.</value>
        public int Size { get; }
        #endregion

        internal SquareBoard(
            string[] array
            , Point end
            , Point start)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            Count = array.Length * array.Length;
            Size = array.Length;
            End = end;
            Start = start;
            _points = new Queue<Point>(array.Length * array.Length);
            _points = new Queue<Point>(array.Length * array.Length);
            for (var i = 0; i < array.Length; i++)
            {
                var item = array[i];
                for (int j = 0; j < item.Length; j++)
                {
                    if (item[i] == 'X')
                    {
                        _blockers.Enqueue(new Point(j, i));
                        continue;
                    }

                    _points.Enqueue(new Point(j, i));
                }
            }
        }

        internal Point MoveToPointWithEqualXcoordsToEnd(Point current)
        {
            if (current.X - End.X < 0)          // Move right across the board.
                while (++current.X != End.X) ;
            else if (current.X - End.X > 0)     // Move left across the board.
                while (--current.X != End.X) ;

            return current;
        }
        internal Point MoveToPointWithEqualYcoordsToEnd(Point current)
        {
            if (current.Y - End.Y < 0)          // Move down the board.
                while (++current.X != End.Y) ;
            else if (current.Y - End.Y > 0)     // Move up the board.
                while (--current.Y != End.Y) ;

            return current;
        }
        internal bool IsMoveValidToPointWithEqualXcoordsToEnd(Point current)
        {
            var reachedEnd = IsCurrentXcoordsEqualToEnd(current);
            var reachedBlocker = false;
            while (!reachedBlocker && !reachedEnd)
            {
                if (_points.Any(_ => _.X == current.X && _.Y == current.Y))
                {
                    if (current.X < End.X)
                        ++current.X;
                    if (current.X > End.X)
                        --current.X;
                    reachedEnd = IsCurrentXcoordsEqualToEnd(current);

                    continue;
                }

                reachedBlocker = true;
            }

            return reachedEnd;
        }
        internal bool IsCurrentXcoordsEqualToEnd(Point current)
            => current.X == End.X;
        internal bool IsCurrentYcoordsEqualToEnd(Point current)
            => current.Y == End.Y;
        internal bool IsOrigin(Point current)
            => current == default;
        internal bool NoBlockerAtXcoordsExists(int x)
            => !_blockers.Any(_ => _.X == x);
        internal bool NoBlockerAtYcoordsExists(int y)
            => !_blockers.Any(_ => _.Y == y);
        internal bool WithinBounds(int x, int y)
            => (0 <= x && x <= Size) && (0 <= y && y <= Size);
        internal int MovesTakenToReachFromStartToEnd()
        {
            var moves = 0;
            var current = Start;
            var goalReached = false;
            while (!goalReached)
            {
                if ((IsCurrentXcoordsEqualToEnd(current) && NoBlockerAtXcoordsExists(current.X))
                    || (IsCurrentYcoordsEqualToEnd(current) && NoBlockerAtYcoordsExists(current.Y)))
                {
                    ++moves;
                    goalReached = true;
                    continue;
                }
                if (IsCurrentXcoordsEqualToEnd(current) && !NoBlockerAtXcoordsExists(current.X))
                {

                }
                if (IsCurrentYcoordsEqualToEnd(current) && !NoBlockerAtYcoordsExists(current.Y))
                {

                    var nextReached = false;
                    while (!nextReached)
                    {
                        if (WithinBounds(current.X, current.Y + 1) && NoBlockerAtYcoordsExists(current.Y + 1))
                        {
                            ++current.Y;
                        }
                        if (WithinBounds(current.X, current.Y - 1))
                        {

                        }
                    }
                }
            }

            return moves;
        }
        internal Point ToLeftEnd(int end, Point point)
        {
            while (point.X < end)
            {
                point.X += 1;
            }

            return point;
        }
        internal Point ToRightEnd(int end, Point point)
        {
            while (point.X > end)
            {
                point.X -= 1;
            }

            return point;
        }
        internal Point ToBottom(int end, Point point)
        {
            while (point.Y < end)
            {
                point.Y += 1;
            }

            return point;
        }
        internal Point ToTop(int end, Point point)
        {
            while (point.Y > end)
            {
                point.Y -= 1;
            }

            return point;
        }
        internal Point ToLeft(Point current)
            => _points.FirstOrDefault(_ => _.X == current.X - 1 && _.Y == current.Y);
        internal Point ToRight(Point current)
            => _points.FirstOrDefault(_ => _.X == current.X + 1 && _.Y == current.Y);
        internal Point ToUp(Point current)
            => _points.FirstOrDefault(_ => _.X == current.X && _.Y == current.Y - 1);
        internal Point ToDown(Point current)
            => _points.FirstOrDefault(_ => _.X == current.X && _.Y == current.Y + 1);
    }
}
