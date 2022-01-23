using System.Collections;
using System.Collections.Generic;
using Data.Scripts.BattleGrid.Cells;
using Data.Scripts.BattleGrid.Pathing;
using UnityEngine;

namespace Data.Scripts.BattleGrid.Pathing.Rules
{
    [CreateAssetMenu(order = 1, fileName = "ClimberPathingRule", menuName = "CustomSystems/Pathing/ClimberRule")]
    public class ClimberRule : PathingRule
    {
        [SerializeField] private float climbMultiplier; 
        
        public override int CalculateMovementCost(Cell cellFrom, Cell cellTo, Border border = null)
        {
            return 1 + Mathf.FloorToInt(climbMultiplier * Mathf.Abs(cellFrom.y - cellTo.y));
        }
    }
}
