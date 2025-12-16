using UnityEngine;

namespace Script.BattleStyle.DataDefinitions.Data
{
    public class CardZoneCoordinate
    {
        private const int MAXRAW = 2, MAXCOL = 8, MINRAW = 0, MINCOL = 0;

        public int Row { get; private set; }
        public int Col { get; private set; }

        public CardZoneCoordinate(int row, int col)
        {
            Row = row;
            Col = col;
        }


        public CardZoneCoordinate Up()
        {
            if (Row > MINRAW)
            {
                return new CardZoneCoordinate(Row - 1, Col);
            }

            return this;
        }

        public CardZoneCoordinate Down()
        {
            if (Row < MAXRAW)
            {
                return new CardZoneCoordinate(Row + 1, Col);
            }

            return this;
        }

        public CardZoneCoordinate Left()
        {
            if (Col > MINCOL)
            {
                return new CardZoneCoordinate(Row, Col - 1);
            }

            return this;
        }

        public CardZoneCoordinate Right()
        {
            if (Col < MAXCOL)
            {
                return new CardZoneCoordinate(Row, Col + 1);
            }

            return this;
        }

        public override string ToString()
        {
            return Col + ", " + Row;
        }
    }
}