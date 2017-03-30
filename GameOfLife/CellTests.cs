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
        private const int MaxStaticNeighbors = 3;
        private const int OverpopulationNeighbors = 4;
        private const int RegenerationNeighbors = 3;
        private const int MinStaticNeighbors = 2;
        private const int UnderpopulationNeighbors = 0;

        [Test]
        public void CellShouldBeAliveGivenAlive()
        {
            Cell cell = new Cell(PopulationControl.Alive);
            bool isAlive = cell.IsAlive();
            Assert.True(isAlive);
        }

        [Test]
        public void CellShouldBeDeadGivenDead()
        {
            Cell cell = new Cell(PopulationControl.Dead);
            bool isAlive = cell.IsAlive();
            Assert.False(isAlive);
        }

        [Test]
        public void IsAliveShouldBeFalseAfterAliveCellUpdateGivenUnderpopulationNeighbors()
        {
            Cell cell = AliveCell();
            new PopulationControl().UpdateState(cell, UnderpopulationNeighbors);
            cell.Update();
            Assert.False(cell.IsAlive());
        }

        [Test]
        public void IsAliveShouldBeTrueAfterAliveCellUpdateGivenMinStaticNeighbors()
        {
            Cell cell = AliveCell();
            new PopulationControl().UpdateState(cell, MinStaticNeighbors);
            cell.Update();
            Assert.True(cell.IsAlive());
        }

        [Test]
        public void IsAliveShouldBeFalseAfterDeadCellUpdateGivenMinStaticNeighbors()
        {
            Cell cell = DeadCell();
            new PopulationControl().UpdateState(cell, MinStaticNeighbors);
            cell.Update();
            Assert.False(cell.IsAlive());
        }

        [Test]
        public void IsAliveShouldBeTrueAfterDeadCellUpdateGivenRegenerationNeighbors()
        {
            Cell cell = DeadCell();
            new PopulationControl().UpdateState(cell, RegenerationNeighbors);
            cell.Update();
            Assert.True(cell.IsAlive());
        }

        [Test]
        public void IsAliveShouldBeFalseAfterAliveCellUpdateGivenOverpopulationNeighbors()
        {
            Cell cell = AliveCell();
            new PopulationControl().UpdateState(cell, OverpopulationNeighbors);
            cell.Update();
            Assert.False(cell.IsAlive());
        }

        [Test]
        public void IsAliveShouldBeTrueAfterAliveCellUpdateGivenMaxStaticNeighbors()
        {
            Cell cell = AliveCell();
            new PopulationControl().UpdateState(cell, MaxStaticNeighbors);
            cell.Update();
            Assert.True(cell.IsAlive());
        }

        private static Cell AliveCell()
        {
            return new Cell(PopulationControl.Alive);
        }

        private static Cell DeadCell()
        {
            return new Cell(PopulationControl.Dead);
        }
    }

    public class PopulationControl
    {
        public static readonly object Alive = new object();
        public static readonly object Dead = new object();

        private delegate object CellDeath(int neighbors);

        private static readonly CellDeath LivingCell =
            neighbors => neighbors < 2 || neighbors > 3 ? Dead : Alive;

        private static readonly CellDeath DeadCell =
            neighbors => neighbors == 3 ? Alive : Dead;

        public void UpdateState(Cell cell, int neighbors)
        {
            object futureState = cell.IsAlive() ? LivingCell(neighbors) : DeadCell(neighbors);
            cell.SetUpdateState(futureState);
        }
    }
    public class Cell
    {
        private object _state;
        private object _updateState;

        public Cell(object state)
        {
            _state = state;
        }

        public bool IsAlive()
        {
            return _state == PopulationControl.Alive;
        }

        public void SetUpdateState(object futureState)
        {
            _updateState = futureState;
        }

        public void Update()
        {
            _state = _updateState;
        }
    }
}