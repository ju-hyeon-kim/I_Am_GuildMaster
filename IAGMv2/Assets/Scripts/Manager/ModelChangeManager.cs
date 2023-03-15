using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ModelChangeManager : MonoBehaviour
{
    public Material[] ColorPack;

    void Awake()
    {
        ColorPack = Resources.LoadAll<Material>("Material/NpcMaterials");
    }
    //public int MatNum;

    // Start is called before the first frame update
    void Start()
    {
        int ClothNum = Random.Range(0, 5);

        Renderer Rd = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mat = Rd.sharedMaterials;

        mat[0] = ColorPack[ClothNum];
        
        Rd.materials = mat;
    }
}
