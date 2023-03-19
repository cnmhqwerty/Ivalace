using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public bool growth = false;
    public bool available = true;
    public Sprite wall;
    private Sprite[] vines;
    public Sprite[] vinesF, vinesL;
    private int rand;
    private string mode;

    private void Start()
    {
        rand = Random.Range(0, 4);
    }

    void Update()
    {
        vines = GameplayController.curVine() ? vinesF : vinesL;
        GetComponent<SpriteRenderer>().sprite = growth? vines[rand] : wall;
    }

    private void OnMouseDown()
    {
        mode = GameplayController.mode;
        if(growth && mode == "trim")
        {
            growth = false;
            TileMap.SetSeedCount(TileMap.GetSeedCount()+1);
        }
        else if (TileMap.GetSeedCount() > 0 && !growth && mode == "plant")
        {
            growth = true;
            TileMap.SetSeedCount(TileMap.GetSeedCount()-1);
        }
    }
}
