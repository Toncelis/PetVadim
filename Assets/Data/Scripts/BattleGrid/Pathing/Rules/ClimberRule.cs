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
        [SerializeField] private float ascendingMultiplier;
        [SerializeField] private float descendingMultiplier;
        
        public override int CalculateMovementCost(Cell cellFrom, Cell cellTo, Border border = null)
        {
            if (cellFrom.y > cellTo.y)
            {
                return 1 + Mathf.FloorToInt(descendingMultiplier * (cellFrom.y - cellTo.y));
            }
            
            return 1 + Mathf.FloorToInt(ascendingMultiplier * (cellTo.y - cellFrom.y));
            
        }
    }
}
