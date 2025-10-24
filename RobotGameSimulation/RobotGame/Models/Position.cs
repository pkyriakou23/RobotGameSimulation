using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotGameSimulation.RobotGame.Models
{
    public class Position : IEquatable<Position>
    {
        public int Row { get; }
        public int Col { get; }
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public bool Equals(Position? other)
        {
            if (other is null) return false;
            return Row == other.Row && Col == other.Col;
        }
        public override bool Equals(object? obj) => Equals(obj as Position);
        public override int GetHashCode() => HashCode.Combine(Row, Col);

        public override string ToString() => $"{Row},{Col}";
    }
}
