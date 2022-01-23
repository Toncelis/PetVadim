using System.Collections;
using System.Collections.Generic;
using Data.Scripts.BattleGrid.Cells;
using UnityEngine;

namespace Data.Scripts.BattleGrid.Pathing
{
    public abstract class PathingRule : ScriptableObject
    {
        public abstract int CalculateMovementCost(Cell cellFrom, Cell cellTo, Border border = null);
    }
}
