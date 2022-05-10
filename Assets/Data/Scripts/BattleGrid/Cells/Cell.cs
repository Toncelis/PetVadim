using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Data.Scripts.BattleGrid.Cells
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] [ReadOnly] private int _x;
        [SerializeField] [ReadOnly] private int _y;
        [SerializeField] [ReadOnly] private int _z;
        public int x => _x;
        public int y => _y;
        public int z => _z;

        [SerializeField] [Sirenix.OdinInspector.ReadOnly] private GridBox box;

        [SerializeField] [ReadOnly] private GridManager _gridManager;
        public GridManager gridManager => _gridManager;

        public Dictionary<EnumDirections, Cell> adjacentCells;
    
        [SerializeField] [ReadOnly] private Cell forwardCell;
        [SerializeField] [ReadOnly] private Cell backwardCell;
        [SerializeField] [ReadOnly] private Cell rightCell;
        [SerializeField] [ReadOnly] private Cell leftCell;
    
    
        public void Setup(
            int newX, int newY, int newZ, 
            GridManager newGridManager
        )
        {
            _x = newX;
            _y = newY;
            _z = newZ;

            _gridManager = newGridManager;
            box = gameObject.GetComponentInChildren<GridBox>();
            UpdateVisualPosition();
        }

        public void OnEnable()
        {
            InitializeAdjacencyDictionary();
        }

        private void InitializeAdjacencyDictionary()
        {
            adjacentCells = new Dictionary<EnumDirections, Cell>
            {
                {EnumDirections.Forward, forwardCell},
                {EnumDirections.Backward, backwardCell},
                {EnumDirections.Right, rightCell},
                {EnumDirections.Left, leftCell}
            };
        }
        
        public void UpdateAdjacentCells()
        {
            forwardCell = gridManager.GetCell(x, z + 1);
            backwardCell = gridManager.GetCell(x, z - 1);
            leftCell = gridManager.GetCell(x - 1, z);
            rightCell = gridManager.GetCell(x + 1, z);
        }

        public void RecolorToStandard()
        {
            box.RecolorToStandard();
        }
        public void RecolorToChosen()
        {
            box.RecolorToChosen();
        }
        public void RecolorToInRange()
        {
            box.RecolorToInRange();
        }

        private void UpdateVisualPosition()
        {
            transform.position = new Vector3(x, y, z);
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        }
        
        [Button("Update button position")]
        private void UpdateCellSizeBasedOnTransformPosition()
        {
            Debug.Log($"validating box {x} {z}");
            _y = Mathf.RoundToInt(transform.localPosition.y);
            UpdateVisualPosition();
        }
        
        
    }
}