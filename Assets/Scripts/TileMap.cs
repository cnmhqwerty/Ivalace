using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public int width = 12;
    public int height = 8;
    public string axis,type;
    public Tile sprite;
    public Camera mainCam;
    public static int seedCount = 5;
    private float nextTick = 0;

    Tile[,] map;

    // Start is called before the first frame update
    void Start()
    {
        map = new Tile[width,height];
        switch (axis)
        {
            case "z":
                switch (type)
                {
                    case "rect":
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                map[x, y] = Instantiate(sprite,
                                    new Vector3(x + transform.position.x, y + transform.position.y,
                                        transform.position.z),
                                    transform.rotation, transform);
                            }
                        }

                        break;
                    case "triL":
                        int wl = width;
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < wl; x++)
                            {
                                map[x, y] = Instantiate(sprite,
                                    new Vector3(x + transform.position.x, y + transform.position.y,
                                        transform.position.z),
                                    transform.rotation, transform);
                            }
                            wl--;
                        }

                        break;
                    case "triR":
                        int wr = width;
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < wr; x++)
                            {
                                map[x, y] = Instantiate(sprite,
                                    new Vector3(x + transform.position.x, y + transform.position.y,
                                        transform.position.z),
                                    transform.rotation, transform);
                            }
                            wr--;
                        }

                        transform.Rotate(0,180,0);
                        break;
                }

                break;

            case "x":
                for (int z = 0; z < width; z++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        map[z, y] = Instantiate(sprite,
                            new Vector3(transform.position.x, y + transform.position.y,
                                z + transform.position.z),
                            transform.rotation, transform);
                    }
                }

                break;
        }
    }

    void Update()
    {
        if (GameplayController.tickSpeed < 15)
        {
            switch (type)
            {
                case "triL":

                    if (Time.time > nextTick)
                    {
                        nextTick += GameplayController.tickSpeed;
                        int wl = width;
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < wl; x++)
                            {
                                if (map[x, y].growth)
                                {
                                    Grow(x, y);
                                }
                            }

                            wl--;
                        }
                    }

                    break;

                case "triR":

                    if (Time.time > nextTick)
                    {
                        nextTick += GameplayController.tickSpeed;
                        int wr = width;
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < wr; x++)
                            {
                                if (map[x, y].growth)
                                {
                                    Grow(x, y);
                                }
                            }

                            wr--;
                        }
                    }

                    break;

                case "rect":

                    if (Time.time > nextTick)
                    {
                        nextTick += GameplayController.tickSpeed;
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                if (map[x, y].growth)
                                {
                                    Grow(x, y);
                                }
                            }
                        }
                    }

                    break;
            }
        }
    }

    void Grow(int x, int y)
    {
        List<Tile> possible = new List<Tile>();

        if (x - 1 >=0){
            if (map[x - 1, y] != null) //left tile
            {
                possible.Add(map[x - 1, y]);
            }
        }

        if (x + 1 < width)
        {
            if (map[x + 1, y] != null) //right tile
            {
                possible.Add(map[x + 1, y]);
            }
        }

        if (y - 1 >= 0)
        {
            if (map[x, y - 1] != null) //down tile
            {
                possible.Add(map[x, y - 1]);
            }
        }

        if (y + 1 < height)
        {
            if (map[x, y + 1] != null) //up tile
            {
                possible.Add(map[x, y + 1]);
            }
        }

        if (possible.Count > 0)
        {
            possible[Random.Range(0, possible.Count)].growth = true;
        }

    }

    public void reset()
    {
        switch (axis)
        {
            case "z":
                switch (type)
                {
                    case "rect":
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                map[x, y].growth = false;
                            }
                        }

                        break;
                    case "triL":
                        int wl = width;
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < wl; x++)
                            {
                                map[x, y].growth = false;
                            }
                            wl--;
                        }

                        break;
                    case "triR":
                        int wr = width;
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < wr; x++)
                            {
                                map[x, y].growth = false;
                            }
                            wr--;
                        }
                        break;
                }

                break;

            case "x":
                for (int z = 0; z < width; z++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        map[z, y].growth = false;
                    }
                }

                break;
        }
    }
    public static int GetSeedCount()
    {
        return seedCount;
    }
    public static void SetSeedCount(int count)
    {
        seedCount = count;
    }
}
