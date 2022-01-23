using System.Collections.Generic;
using Data.Scripts.BattleGrid.Cells;

namespace Data.Scripts.BattleGrid.Pathing
{
    public class CellMapped
    {
        public Cell cell {private set; get; }
        public EnumDirections direction {private set; get; }
        public int travelPrice { private set; get; }

        public CellMapped(Cell mappedCell, EnumDirections incomingDirection, int priceToReachCellFromCoreCellOfTheMap)
        {
            cell = mappedCell;
            direction = incomingDirection;
            travelPrice = priceToReachCellFromCoreCellOfTheMap;
        }
    }

    public class CellMappedComparer : IComparer<CellMapped>
    {
        public int Compare(CellMapped x, CellMapped y)
        {
            if (x == null)
            {
                return y == null ? 0 : 1;
            }

            if (y == null)
            {
                return -1;
            }
        
            return (x.travelPrice - y.travelPrice);
        }
    }
}
