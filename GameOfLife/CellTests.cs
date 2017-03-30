using System.Runtime;
using NUnit.Framework;

namespace GameOfLife
{
    /*
Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
Any live cell with two or three live neighbours lives on to the next generation.
Any live cell with more than three live neighbours dies, as if by overpopulation.
Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
    */
    [TestFixture]
    public class CellTests
    {
        [Test]
        public void CellShouldBeAliveGivenAlive()
        {
            Cell cell = new PopulationControl().SpawnLivingCell();
            bool isAlive = cell.IsAlive();
            Assert.True(isAlive);
        }

        [Test]
        public void CellShouldBeDeadGivenDead()
        {
            Cell cell = new PopulationControl().SpawnDeadCell();
            bool isAlive = cell.IsAlive();
            Assert.False(isAlive);
        }

        [Test]
        public void PopulationControlShouldSpawnLivingCell()
        {
            Cell cell = SpawnLivingCell();
            bool isAlive = cell.IsAlive();
            Assert.True(isAlive);
        }

        [Test]
        public void PopulationControlShouldSpawnDeadCell()
        {
            Cell cell = SpawnDeadCell();
            bool isAlive = cell.IsAlive();
            Assert.False(isAlive);
        }

        [Test]
        public void PopulationControlShouldKillLivingCellGivenUnderpopulated()
        {
            PopulationControl populationControl = new PopulationControl();
            Cell cell = SpawnLivingCell();
            bool shouldKill = populationControl.ShouldKill(cell, 0) &&
                              populationControl.ShouldKill(cell, 1);
            Assert.True(shouldKill);
        }

        [Test]
        public void PopulationControlShouldNotKillLivingCellGivenNotUnderpopulated()
        {
            PopulationControl populationControl = new PopulationControl();
            Cell cell = SpawnLivingCell();
            bool shouldKill = populationControl.ShouldKill(cell, 2);
            Assert.False(shouldKill);
        }
        [Test]
        public void PopulationControlShouldNotKillLivingCellGivenNotOverpopulated()
        {
            PopulationControl populationControl = new PopulationControl();
            Cell cell = SpawnLivingCell();
            bool shouldKill = populationControl.ShouldKill(cell, 4);
            Assert.True(shouldKill);
        }

        private static Cell SpawnLivingCell()
        {
            return new PopulationControl().SpawnLivingCell();
        }

        private static Cell SpawnDeadCell()
        {
            return new PopulationControl().SpawnDeadCell();
        }
    }

    public class PopulationControl
    {
        public Cell SpawnLivingCell()
        {
            return new Cell(true);
        }

        public Cell SpawnDeadCell()
        {
            return new Cell(false);
        }

        public bool ShouldKill(Cell cell, int neighbors)
        {
            return Underpopulated(neighbors) || Overpopulated(neighbors);
        }

        private static bool Overpopulated(int neighbors)
        {
            return neighbors > 3;
        }

        private static bool Underpopulated(int neighbors)
        {
            return neighbors < 2;
        }
    }

    public class Cell
    {
        private readonly bool _isAlive;

        public Cell(bool isAlive)
        {
            _isAlive = isAlive;
        }

        public bool IsAlive()
        {
            return _isAlive;
        }
    }
}