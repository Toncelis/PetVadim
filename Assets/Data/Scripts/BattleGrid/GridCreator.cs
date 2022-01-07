using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class GridCreator : OdinEditorWindow
{

    [MenuItem("Game/LevelCreation/Grid Creator")]
    private static void OpenWindow()
    {
        GetWindow<GridCreator>().Show();
    }

    [HorizontalGroup] public int width;

    [HorizontalGroup] public int depth;

    [Button(ButtonSizes.Medium)]
    public void GridCreationButton()
    {
        var grid = new GameObject();
        grid.transform.position = Vector3.zero;
        grid.transform.rotation = Quaternion.identity;
        grid.name = "Grid";
        
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                var gridCell = PrefabUtility.InstantiatePrefab(gridBox, grid.transform) as GameObject;
                gridCell.name = $"cell {x}.{z}";
                gridCell.transform.position = new Vector3(x, 0, z);
            }
        }
    }

    [PropertySpace(20)] public GameObject gridBox;

}
