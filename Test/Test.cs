using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void OkTest()
        {
            Assert.Pass();
        }

        [Test]
        public void CorrectDisplay()
        {
            //Arrange
            #region toCompare =
            string toCompare = @"xxxxxxxxxxxxxxxxxxxx
xoxxxxxxxxxxxxxxxxxx
xxoxxxxxxxxxxxxxxxxx
xxxoooxxxxxxxxxxxxxx
xxxoxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
";
            #endregion  
            //Act
            Library.Board board = new Library.Board();

            board.SetAlive(1, 1);
            board.SetAlive(2, 2);
            board.SetAlive(3, 3);
            board.SetAlive(3, 4);
            board.SetAlive(4, 3);
            board.SetAlive(5, 3);
            string output = board.Display();
            //Assert
            Assert.AreEqual(toCompare, output);
        }
        [Test]
        public void CorrectDisplay2()
        {
            //Arrange
            #region toCompare =
            string toCompare = @"xxxxxxxxxxxxxxxxxxxx
xoxxxxxxxxxxxxxxxxxx
xxoxxxxxxxxxxxxxxxxx
xxxoooxxxxxxxxxxxxxx
xxxoxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxx
oooooxxxxxxxxxxxxxxx
";
            #endregion  
            //Act
            Library.Board board = new Library.Board();

            board.SetAlive(1, 1); board.SetAlive(0, 19);
            board.SetAlive(2, 2); board.SetAlive(1, 19);
            board.SetAlive(3, 3); board.SetAlive(2, 19);
            board.SetAlive(3, 4); board.SetAlive(3, 19);
            board.SetAlive(4, 3); board.SetAlive(4, 19);
            board.SetAlive(5, 3); 
            string output = board.Display();
            //Assert
            Assert.AreEqual(toCompare, output);
        }
        [Test]
        public void AliveNeighborsMiddle()
        {
            Library.Board board = new Library.Board();
            foreach (int x in Enumerable.Range(1, 3))
            {
                foreach (int y in Enumerable.Range(1, 3))
                {
                    board.SetAlive(x, y);
                }
            }
            board.SetDead(1, 1);
            int output = board.GetNeighbours(2, 2);
            Assert.AreEqual(7, output);
        }
        [Test]
        public void AliveNeighborsCorner()
        {
            Library.Board board = new Library.Board();
            board.SetAlive(1, 0);
            board.SetAlive(0, 1);
            int output = board.GetNeighbours(0, 0);
            Assert.AreEqual(2, output);

            board.SetAlive(0, 0);
            board.SetAlive(1, 1);
            output = board.GetNeighbours(0, 0);
            Assert.AreEqual(3, output);

            output = board.GetNeighbours(0, 1);
            Assert.AreEqual(3, output);
        }

        //Any live cell with fewer than two live neighbours dies, as if by underpopulation.
        [Test]
        public void LessThanTwoNeighboursDie()
        {
            Library.Board board = new Library.Board();
            board.SetAlive(1, 0);
            board.SetAlive(1, 1);
            board.SetAlive(13, 13);

            board.NextState();

            Assert.AreEqual(false, board.IsAlive(1, 0));
            Assert.AreEqual(false, board.IsAlive(1, 1));
            Assert.AreEqual(false, board.IsAlive(13, 13));
        }
        //Any live cell with two or three live neighbours lives on to the next generation.
        [Test]
        public void TwoOrThreeNeighboursAlive()
        {
            Library.Board board = new Library.Board();
            board.SetAlive(4, 4);
            board.SetAlive(4, 5);
            board.SetAlive(5, 4);

            board.NextState();

            Assert.AreEqual(true, board.IsAlive(4, 4));
            Assert.AreEqual(true, board.IsAlive(4, 5));
            Assert.AreEqual(true, board.IsAlive(5, 4));
        }
        
        //Any live cell with more than three live neighbours dies, as if by overpopulation.
        [Test]
        public void MoreThanThreeNeighbours()
        {
            Library.Board board = new Library.Board();
            board.SetAlive(2, 2);
            board.SetAlive(2, 3);
            board.SetAlive(2, 1);
            board.SetAlive(3, 2);
            board.SetAlive(1, 2);
            board.NextState();

            Assert.AreEqual(false, board.IsAlive(2, 2));
        }
        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        [Test]
        public void ExactlyThreeNeighboursDeadCell()
        {
            Library.Board board = new Library.Board();
            board.SetAlive(2, 3);
            board.SetAlive(2, 1);
            board.SetAlive(3, 2);
            board.SetAlive(1, 2);

            board.SetAlive(5, 6);
            board.SetAlive(5, 5);
            board.SetAlive(6, 5);

            board.NextState();

            Assert.AreEqual(false, board.IsAlive(2, 2));
            Assert.AreEqual(true, board.IsAlive(6, 6));
        }

    }
}

