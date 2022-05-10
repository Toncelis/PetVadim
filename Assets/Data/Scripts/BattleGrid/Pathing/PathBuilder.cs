using System;
using System.Collections.Generic;
using System.Linq;
using Data.Scripts.BattleGrid.Cells;

namespace Data.Scripts.BattleGrid.Pathing
{
    public static class PathBuilder
    {
        private static readonly CellMappedComparer MappedCellsComparer = new CellMappedComparer();

        private static void AddMappedCellToSortedList(List<CellMapped> sortedCellList, CellMapped cellToAdd)
        {
            int index = sortedCellList.BinarySearch(cellToAdd, MappedCellsComparer);
            if (index >= 0)
            {
                sortedCellList.Insert(index, cellToAdd);
            }

            if (index < 0)
            {
                sortedCellList.Insert(~index, cellToAdd);
            }
        }
        
        public static List<CellMapped> MapTheGridToFindAllReachableCells(
            Cell coreCell, PathingRule pathingRule, int maxMovementPoints)
        {
            var reachableCells = new List<CellMapped>();
            var boundaryCells = new List<CellMapped>();
            GridManager grid = coreCell.gridManager;
            
            boundaryCells.Add(new CellMapped(coreCell, EnumDirections.Rest, 0));
            /*
            foreach (var direction in (EnumDirections[]) Enum.GetValues(typeof(EnumDirections)))
            {
                Cell cell = grid.GetCell(coreCell, direction);
                
                if (cell != null)
                {
                    boundaryCells.Add(new CellMapped(cell, direction, pathingRule.CalculateMovementCost(coreCell, cell)));
                }
            }
            boundaryCells.Sort(MappedCellsComparer);
            */

            int processedCellIndex = 0;
            while (boundaryCells[processedCellIndex].travelPrice <= maxMovementPoints)
            {
                CellMapped processedCell = boundaryCells[processedCellIndex];
                processedCellIndex++;
                
                reachableCells.Add(processedCell);

                foreach (var direction in (EnumDirections[]) Enum.GetValues(typeof(EnumDirections)))
                {
                    Cell possibleBoundaryCell = grid.GetCell(processedCell.cell, direction);
                    if (possibleBoundaryCell == null)
                    {
                        continue;
                    }

                    if (reachableCells.Any(mappedCell => mappedCell.cell == possibleBoundaryCell) || boundaryCells.Any(mappedCell => mappedCell.cell == possibleBoundaryCell))
                    {
                        continue;
                    }

                    int stepPrice = pathingRule.CalculateMovementCost(processedCell.cell, possibleBoundaryCell);
                    if (stepPrice > 0)
                    {
                            AddMappedCellToSortedList(boundaryCells,
                                new CellMapped(possibleBoundaryCell, direction,
                                processedCell.travelPrice + stepPrice));
                    }
                    
                }
                
            }
            
            return reachableCells;
        }
    }
}
