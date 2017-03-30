using System;
using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class CellTests
    {
        [Test]
        public void CellShouldBeAliveGivenAlive()
        {
            Cell cell = new Cell(true);
            bool isAlive = cell.IsAlive();
            Assert.True(isAlive);
        }

        [Test]
        public void CellShouldBeDeadGivenDead()
        {
            Cell cell = new Cell(false);
            bool isAlive = cell.IsAlive();
            Assert.False(isAlive);
        }

        [Test]
        public void ShouldSpawnLivingCell()
        {
            Cell cell = new PopulationControl().SpawnLivingCell();
            bool isAlive = cell.IsAlive();
            Assert.True(isAlive);
        }

        [Test]
        public void ShouldSpawnDeadCell()
        {
            Cell cell = new PopulationControl().SpawnDeadCell();
            bool isAlive = cell.IsAlive();
            Assert.False(isAlive);
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