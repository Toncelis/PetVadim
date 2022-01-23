using System.Collections.Generic;
using Data.Scripts.BattleGrid.Cells;
using Data.Scripts.BattleGrid.Pathing;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Data.Scripts.BattleGrid
{
    public class GridPointer : MonoBehaviour
    {
        [TabGroup("Pointer settings")] [SerializeField]
        private float raycastDistance;

        [TabGroup("PathBuilderSettings")] [SerializeField]
        private PathingRule pathingRule;
        [TabGroup("PathBuilderSettings")] [SerializeField]
        private int movementPoints;
        
        
        private void Start()
        {
            _playerCamera = Camera.main;
            _coloredCells = new List<Cell>();
            _newColoredCells = new List<Cell>();
        }

        private Camera _playerCamera;

        private List<Cell> _coloredCells;
        private List<Cell> _newColoredCells;
        
        private Cell TryPickCell()
        {
            if (!Physics.Raycast(_playerCamera.ScreenPointToRay(Input.mousePosition),
                    out var hit, raycastDistance,
                    LayerMask.GetMask("Grid")))
            {
                return null;
            }
            return hit.collider.gameObject.GetComponent<Cell>();
        }

        private void RecolorCells()
        {
            if (_coloredCells.Count == _newColoredCells.Count && _coloredCells.Count != 0 && _coloredCells[0] == _newColoredCells[0])
            {
                Debug.Log($"Keeping {_coloredCells.Count} boxes colored with core : {_coloredCells[0]}");
                _newColoredCells.Clear();
            }
            else
            {
                Debug.Log($"Coloring {_coloredCells.Count} boxes to STANDARD");
                foreach (var cell in _coloredCells)
                {
                    cell.RecolorToStandard();
                }

                _coloredCells.Clear();
                _coloredCells = new List <Cell> (_newColoredCells);
                _newColoredCells.Clear();
                
                if (_coloredCells.Count > 0)
                {
                    Debug.Log($"Recoloring {_coloredCells.Count} boxes with core : {_coloredCells[0]}");
                    _coloredCells[0].RecolorToChosen();
                    for (int i = 1; i < _coloredCells.Count; i++)
                    {
                        _coloredCells[i].RecolorToInRange();
                    }
                }
            }
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var cell = TryPickCell();
                if (cell != null)
                {
                    _newColoredCells.Add(cell);
                }

                RecolorCells();
            }

            if (Input.GetMouseButtonDown(1))
            {
                var pickedCell = TryPickCell();
                if (pickedCell != null)
                {
                    pickedCell.RecolorToChosen();
                    foreach (var cell in PathBuilder.MapTheGridToFindAllReachableCells(pickedCell, pathingRule,
                                 movementPoints))
                    {
                        _newColoredCells.Add(cell.cell);
                    }
                }
                RecolorCells();
            }
        }
    }
}
