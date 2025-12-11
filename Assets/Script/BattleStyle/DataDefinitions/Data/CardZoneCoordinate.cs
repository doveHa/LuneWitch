using UnityEngine;

namespace Script.BattleStyle.DataDefinitions.Data
{
    public class CardZoneCoordinate
    {
        private const int MAXRAW = 2, MAXCOL = 8, MINRAW = 0, MINCOL = 0;

        public int Raw { get; private set; }
        public int Col { get; private set; }

        public CardZoneCoordinate(int raw, int col)
        {
            Raw = raw;
            Col = col;
        }


        public CardZoneCoordinate Up()
        {
            if (Raw > MINRAW)
            {
                return new CardZoneCoordinate(Raw - 1, Col);
            }

            return this;
        }

        public CardZoneCoordinate Down()
        {
            if (Raw < MAXRAW)
            {
                return new CardZoneCoordinate(Raw + 1, Col);
            }

            return this;
        }

        public CardZoneCoordinate Left()
        {
            if (Col > MINCOL)
            {
                return new CardZoneCoordinate(Raw, Col - 1);
            }

            return this;
        }

        public CardZoneCoordinate Right()
        {
            if (Col < MAXCOL)
            {
                return new CardZoneCoordinate(Raw, Col + 1);
            }

            return this;
        }
    }
}