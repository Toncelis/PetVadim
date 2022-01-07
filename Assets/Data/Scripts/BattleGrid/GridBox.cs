using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox : MonoBehaviour
{
    [SerializeField] private Material standartMaterial;
    [SerializeField] private Material chosenMaterial;

    private MeshRenderer _renderer;
    
    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void RecolorToChosen()
    {
        _renderer.material = chosenMaterial;
    }

    public void RecolorToStandart()
    {
        _renderer.material = standartMaterial;
    }
    
}
