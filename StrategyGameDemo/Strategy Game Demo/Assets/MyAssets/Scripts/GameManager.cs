using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private int coin = 250, coinRate = 1;

    private Vector3 rectStart, rectEnd, mousePos;

    public Rect rectangle = new Rect();

    public FindTarget[] soldiers;

    public GameObject selectionRect;

    private ProductionCanvas productionCanvas;

    public GameObject informationCanvas;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("Game Manager").AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    #region UnityFunctions
    void Awake()
    {
        _instance = this;

        informationCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Start()
    {
        rectStart = Vector3.zero;
        rectEnd = Vector3.zero;

        selectionRect.SetActive(false);


        InvokeRepeating("EarnCoin", 1f, 1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        else if (Input.GetMouseButton(0))
        {
            MouseDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }

    

    #endregion

    #region CoinFunctions
    public int GetCoin()
    {
        return coin;
    }

    public bool IsCoinEnough(int cost)
    {
        if (coin >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseCoinRate(int extraCoinRate)
    {
        coinRate += extraCoinRate;
    }

    private void EarnCoin()
    {
        coin += coinRate;
    }

    public void SpendCoin(int cost)
    {
        coin -= cost;
    }



    #endregion

    #region MouseFunctions

    void MouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (informationCanvas.transform.GetChild(0).gameObject.active)
            {
                informationCanvas.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);

        rectStart = mousePos;

        rectangle = new Rect();

        selectionRect.SetActive(true);

        selectionRect.transform.position = mousePos;
    }

    void MouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);

        selectionRect.transform.localScale = new Vector3((mousePos.x - rectStart.x), (mousePos.y - rectStart.y), -1);
    }

    void MouseUp()
    {
        selectionRect.SetActive(false);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);

        rectEnd = mousePos;

        rectangle = new Rect(rectStart, rectEnd - rectStart);

        soldiers = GameObject.FindObjectsOfType<FindTarget>();

        for (int i = 0; i < soldiers.Length; i++)
        {
            if (rectangle.Contains(new Vector2(soldiers[i].transform.position.x,
                    soldiers[i].transform.position.y), true))
            {
                soldiers[i].selected = true;
            }
            else
            {
                soldiers[i].selected = false;
            }
        }
    }

    #endregion
}
