using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridPointer : MonoBehaviour
{
    private void Start()
    {
        playerCamera = Camera.main;
    }

    private GridBox currentChosenBox;
    private Camera playerCamera;

    private bool TryPickBox()
    {
        if (currentChosenBox != null)
        {
            currentChosenBox.RecolorToStandart();
        }
        RaycastHit hit;
        
        if (!Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hit, LayerMask.GetMask("Grid")))
        {
            return false;
        }

        currentChosenBox = hit.collider.gameObject.GetComponent<GridBox>();
        currentChosenBox.RecolorToChosen();
        return true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryPickBox();
        }
    }
}
