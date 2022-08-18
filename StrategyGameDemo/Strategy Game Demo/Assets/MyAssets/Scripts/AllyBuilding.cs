using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AllyBuilding : BaseBuilding
{
    protected abstract GameObject childRenderer { get; set; }
    protected abstract GameObject soldierExitPoint { get; set; }

    protected abstract GameObject myCanvas { get; set; }

    protected abstract Vector3 lclScale { get; set; }
    protected abstract int cost { get; set; }

    protected bool isTouched = false, isFollow = false, isAvaible = false, isDone = false;

    public bool isPlaced = false;

    private TileManager tileManager;

    private Tile red, white, green;

    protected Vector3 mousePos, exitPointOffset;
    
    private Vector3Int loc;



    void Start()
    {
        tileManager = GameObject.FindObjectOfType<TileManager>();

        white = tileManager.whiteTile;
        red = tileManager.redTile;
        green = tileManager.greenTile;

        exitPointOffset = new Vector3(0, -3, 0);

        SetCanvases();
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        if (!isDone)
        {
            Debug.Log(1);
            if (isPlaced)
            {
                Place();

                isDone = true;
            }
            else
            {
                Debug.Log(11);
                IsAvaible();
            }

            if (isFollow)
            {
                Debug.Log(11111);
                Follow();
            }
        }

        if (!isPlaced && Input.GetKeyUp(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

    bool IsAffordable()
    {
        return GameManager.Instance.IsCoinEnough(cost);
    }

    public void IsFollow()
    {
        isTouched = true;
        isFollow = true;
    }

    void IsAvaible()
    {
        if (IsAffordable())
        {
            Debug.Log(111);
            for (int i = 0; i < lclScale.x; i++)
            {
                for (int j = 0; j < lclScale.y; j++)
                {
                    Vector3Int newLoc = new Vector3Int(loc.x + i, loc.y + j, loc.z);

                    if (tileManager.GetTileState(newLoc) == TileState.Red)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                        Debug.Log(111234);
                        if (childRenderer != null)
                        {
                            childRenderer.GetComponent<SpriteRenderer>().color = Color.red;
                        }

                        isAvaible = false;

                        i = Mathf.RoundToInt(lclScale.x + 1);
                        break;
                    }

                    gameObject.GetComponent<SpriteRenderer>().color = Color.green;


                    if (childRenderer!=null)
                    {
                        childRenderer.GetComponent<SpriteRenderer>().color = Color.green;
                    }

                    Debug.Log(1112341231243);

                    isAvaible = true;

                }
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void Place()
    {
        for (int i = 0; i < lclScale.x; i++)
        {
            for (int j = 0; j < lclScale.y; j++)
            {
                Vector3Int newLoc = new Vector3Int(loc.x + i, loc.y + j, loc.z);

                tileManager.SetTileState(newLoc, red);
            }
        }

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        if (childRenderer)
        {
            childRenderer.GetComponent<SpriteRenderer>().color = Color.white;
        }

        AstarPath.active.Scan();
    }

    void Follow()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        loc = tileManager.MainTilemap.WorldToCell(mousePos);

        transform.position = loc + new Vector3(lclScale.x / 2.65f, lclScale.y / 2.65f, 0);
    }
    /*
    protected override void MouseUp()
    {
        if (isDone)
        {
            if (!isOpen)
            {
                if (soldierExitPoint != null)
                {
                    soldierExitPoint.transform.position = this.transform.position + exitPointOffset;
                }

                proCanvas.OpenCanvas(informationCanvas);


                for (int i = 0; i < canvases.Capacity; i++)
                {
                    if (myCanvas != canvases[i])
                    {
                        proCanvas.CloseCanvas(canvases[i]);
                    }
                    else
                    {
                        proCanvas.OpenCanvas(myCanvas);
                    }
                }

                SetHealthText();
            }
            else
            {
                proCanvas.CloseCanvas(myCanvas);
                proCanvas.CloseCanvas(informationCanvas);
            }
        }
    }
    */

    protected override void MouseDown()
    {
        if (!isTouched)
        {
            Debug.Log(2);
            isTouched = true;
            isFollow = true;
        }
        else
        {
            Debug.Log(22);
            if (isAvaible)
            {
                Debug.Log(222);
                if (!isPlaced)
                {
                    Debug.Log(2222);
                    GameManager.Instance.SpendCoin(cost);
                    isPlaced = true;
                    isFollow = false;
                }
            }
            else
            {
                Debug.LogError("Bu Kareye Müsait Deðil.");
            }
        }
    }
}
