using Data.Scripts.BattleGrid.Cells;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Data.Scripts.BattleGrid
{
    public class GridCreator : OdinEditorWindow
    {

        [MenuItem("Game/LevelCreation/Grid Creator")]
        private static void OpenWindow()
        {
            GetWindow<GridCreator>().Show();
        }

        public int width;
        public int depth;
        public int maxHeight;

        [Button(ButtonSizes.Medium)]
        public void GridCreationButton()
        {
            var grid = new GameObject();
        
            grid.transform.position = Vector3.zero;
            grid.transform.rotation = Quaternion.identity;
            grid.name = "Grid";

            GridManager gridManager = grid.AddComponent<GridManager>();
            gridManager.Setup(width, depth);
        
            for (int z = 1; z <= depth; z++)
            {
                for (int x = 1; x <= width; x++)
                {
                    var gridCell = PrefabUtility.InstantiatePrefab(gridBox, grid.transform) as GameObject;
                    gridCell.name = $"cell {x}.{z}";
                    int randomHeight = UnityEngine.Random.Range(0, maxHeight);
                    gridCell.transform.position = new Vector3(x, (float)randomHeight/2, z);
                    gridCell.transform.localScale = new Vector3 (0.99f, 1+randomHeight, 0.99f);

                    Cell cell = gridCell.AddComponent<Cell>();
                    cell.Setup(x,randomHeight,z,gridManager);
                    gridManager.AddCell(cell);

                }
            }
        
            gridManager.RebuildCellConnections();
        }

        [PropertySpace(20)] public GameObject gridBox;

    }
}
