using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class KeyHighlight : MonoBehaviour
{

   

    private Material originalMat;

    [SerializeField]
    private Material highlightMat;

    MeshRenderer meshRenderer;

    /// <summary>
    /// gets the mesh renderer Component from Inspektor
    /// the default material for color set on original white
    /// </summary>
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMat = meshRenderer.material; 
    }

    /// <summary>
    /// highlights the key gameobjects with prefab colour metallic brown
    /// </summary>
    public void HighlightKey()
    {
        Debug.Log(" key highlighted...");
        meshRenderer.material = highlightMat;
    }

    /// <summary>
    /// unhighlights the key gameobjects back to original color white
    /// </summary>
    public void UnhighlightKey()
    {
        Debug.Log("key unhighlighted...");
        meshRenderer.material = originalMat;
    }
}
