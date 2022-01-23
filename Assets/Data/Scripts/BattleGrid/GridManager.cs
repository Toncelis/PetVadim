using System.Collections.Generic;
using Data.Scripts.BattleGrid.Cells;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scripts.BattleGrid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] [ReadOnly] private int width;
        [SerializeField] [ReadOnly] private int depth;
        [SerializeField] [ReadOnly] List<Cell> cells;

        public void Setup(int width, int depth)
        {
            this.width = width;
            this.depth = depth;

            cells = new List<Cell>();
        }

        public void AddCell(Cell cell)
        {
            cells.Add(cell);
        }

        public Cell GetCell(int x, int z)
        {
            if ((x < 1) || (z < 1) || (x > width) || (z > depth) || (cells.Count < width * (z-1) + x))
            {
                return null;
            }

            return cells[width * (z - 1) + x - 1];
        }

        public Cell GetCell(Cell cellToMoveFrom, EnumDirections movingDirection)
        {
            return movingDirection switch
            {
                EnumDirections.Backward => cellToMoveFrom.z == 1 
                    ? null 
                    : GetCell(cellToMoveFrom.x, cellToMoveFrom.z - 1),
            
                EnumDirections.Forward => cellToMoveFrom.z == depth
                    ? null
                    : GetCell(cellToMoveFrom.x, cellToMoveFrom.z + 1),
            
                EnumDirections.Left => cellToMoveFrom.x == 1 
                    ? null 
                    : GetCell(cellToMoveFrom.x - 1, cellToMoveFrom.z),
            
                EnumDirections.Right => cellToMoveFrom.x == width 
                    ? null 
                    : GetCell(cellToMoveFrom.x + 1, cellToMoveFrom.z),
                
                _ => null
            };
        }

        public void RebuildCellConnections()
        {
            foreach (var cell in cells)
            {
                cell.UpdateAdjacentCells();
            }
        }
    }
}
