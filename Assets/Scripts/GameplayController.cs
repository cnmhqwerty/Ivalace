using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static string mode = "plant";
    public static float tickSpeed = 10;
    public GameObject count;
    public Slider sl;
    private static bool vToggle = false;

    public static bool curVine()
    {
        return vToggle;
    }

    public void Vine()
    {
        vToggle = !vToggle;
    }

    public void Plant()
    {
        mode = "plant";
    }
    
    public void Trim()
    {
        mode = "trim";
    }
    
    public void Speed()
    {
        tickSpeed = sl.value;
    }

    void Update()
    {
        //count.GetComponent<Text>().text = TileMap.GetSeedCount().ToString();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Plant();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Trim();
        }
    }

    public void ResetTiles()
    {
        var objects = GameObject.FindGameObjectsWithTag("TileMap");
        foreach (var obj in objects)
        {
            obj.GetComponent<TileMap>().reset();
        }
    }
}
