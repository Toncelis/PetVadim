using System.Collections.Generic;
using System.Linq;
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

            _state = 0;
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
                Debug.Log("no cells under click");
                return null;
            }
            Debug.Log("found cell");
            return hit.collider.gameObject.GetComponentInParent<Cell>();
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
            switch (_state)
            {
                case 0 :
                    WaitingForMapping();
                    return;
                case 1 :
                    WaitingForPathDestination();
                    return;
            }
        }
        
        // ??!? probably state machine to control current input reading
        // !!! temp realisation

        #region Temp realisation of path backtracking (for visual testing)

        [TabGroup("PathBuilderSettings")] [PropertySpace(8)] [SerializeField]
        private GameObject visualArrow;
        
        private int _state;
        private List<CellMapped> _pathingMap;

        private void WaitingForMapping()
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
                    _pathingMap = PathBuilder.MapTheGridToFindAllReachableCells(pickedCell, pathingRule, movementPoints);
                    foreach (var cell in _pathingMap)
                    {
                        _newColoredCells.Add(cell.cell);
                    }
                }
                RecolorCells();

                if (_pathingMap.Count > 1)
                {
                    _state = 1;
                }
            }
        }

        private List<GameObject> _arrows = new List<GameObject>();
        
        private void WaitingForPathDestination()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var cell = TryPickCell();
                if (cell != null)
                {
                    if (_pathingMap[0].cell == cell)
                    {
                        ClearArrows();
                    }
                    else if (_pathingMap.Any(mappedCell => mappedCell.cell == cell))
                    {
                        ClearArrows();
                        VisualisePath(cell);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                ClearArrows();
                RecolorCells();
                _state = 0;
            }
        }

        private Cell _visualisedDestinationCell;
        private void ClearArrows()
        {
            if (_visualisedDestinationCell != null)
            {
                _visualisedDestinationCell.RecolorToInRange();
                _visualisedDestinationCell = null;
            }
            foreach (var arrow in _arrows)
            {
                Destroy(arrow);
            }

            _arrows.Clear();
        }

        private void VisualisePath(Cell cell)
        {
            CellMapped destinationCell = _pathingMap.Find(mappedCell => mappedCell.cell == cell);
            cell.RecolorToChosen();
            _visualisedDestinationCell = cell;
            var previousCell = cell.gridManager.GetCell(destinationCell.cell,
                DirectionsUtility.Reverse(destinationCell.direction));
            while (previousCell != _pathingMap[0].cell)
            {
                Quaternion arrowRotation;
                switch (destinationCell.direction)
                {
                    case EnumDirections.Forward:
                        arrowRotation = Quaternion.identity;
                        break;
                    case EnumDirections.Backward:
                        arrowRotation = Quaternion.Euler(0,180,0);
                        break;
                    case EnumDirections.Left:
                        arrowRotation = Quaternion.Euler(0,-90,0);
                        break;
                    case EnumDirections.Right:
                        arrowRotation = Quaternion.Euler(0,90,0);
                        break;
                    default:
                        arrowRotation = Quaternion.identity;
                        break;
                    
                }

                _arrows.Add(Instantiate(visualArrow,
                    previousCell.gameObject.transform.position + 0.5f * Vector3.up, arrowRotation) as GameObject);
                destinationCell = _pathingMap.Find(mappedCell => mappedCell.cell == previousCell);
                previousCell = cell.gridManager.GetCell(destinationCell.cell,
                    DirectionsUtility.Reverse(destinationCell.direction));
            }
        }
        #endregion

    }
}
