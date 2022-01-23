using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Scripts.BattleGrid
{
    public enum EnumDirections
    {
        Rest,
        Forward,
        Backward,
        Right,
        Left
    }

    public static class DirectionsUtility
    {
        public static EnumDirections Reverse(EnumDirections direction)
        {
            return direction switch
            {
                EnumDirections.Backward => EnumDirections.Forward,
                EnumDirections.Forward => EnumDirections.Backward,
                EnumDirections.Left => EnumDirections.Right,
                EnumDirections.Right => EnumDirections.Left,
                EnumDirections.Rest => EnumDirections.Rest,
                _ => EnumDirections.Rest
            };
        }

    }
}