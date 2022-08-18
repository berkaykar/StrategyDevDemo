using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionCanvas: MonoBehaviour
{
    public GameObject soldier;
    [SerializeField] private int soldierCost = 10;
    [SerializeField] private Text coinText;

    [SerializeField] private UI_InfiniteScrolls objPoolScr;

    private int soldierCount = 0;
    private Vector3 offset;

    void Update()
    {
        ShowCoin();
    }

    public void OpenCanvas(GameObject open)
    {
        open.SetActive(true);
    }

    public void CloseCanvas(GameObject close)
    { 
        close.SetActive(false);
    }

    public void Build(GameObject building)
    {
        BarrackController[] bbuildings = GameObject.FindObjectsOfType<BarrackController>();
        PowerPlantController[] pbuildings = GameObject.FindObjectsOfType<PowerPlantController>();

        for (int i = 0; i < bbuildings.Length; i++)
        {
            if (!bbuildings[i].isPlaced)
            {
                Destroy(bbuildings[i].gameObject);
            }
        }

        for (int i = 0; i < pbuildings.Length; i++)
        {
            if (!pbuildings[i].isPlaced)
            {
                Destroy(pbuildings[i].gameObject);
            }
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 buildingPos = mousePos + new Vector3(0, 0, 5);
        
        GameObject newBuilding = Instantiate(building, buildingPos, Quaternion.identity);

        newBuilding.GetComponent<AllyBuilding>().IsFollow();
    }

    public void RaiseSoldier(Transform exitPoint)
    {

        GameManager instance = GameManager.Instance;

        if (instance.IsCoinEnough(soldierCost))
        {
            instance.SpendCoin(soldierCost);


            offset = new Vector3(((soldierCount ) % 5) - 2, -((soldierCount ) / 5), 0);
            soldierCount++;

            for (int i = 0; i < soldierCount-1; i++)
            {
                Debug.LogError((instance.soldiers[i].transform.position - offset).magnitude);

                if ((instance.soldiers[i].transform.position - offset).magnitude < 10)
                {
                    break;
                }

                if (i >= soldierCount - 2)
                {
                    soldierCount = 0;

                }
            }

            GameObject newSoldier = Instantiate(soldier, exitPoint.transform.position + offset, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Yetersiz Para!");
        }
    }

    public void ShowCoin()
    {
        coinText.text = "Coin: " + GameManager.Instance.GetCoin().ToString();
    }

    public void SwapDown()
    {
        objPoolScr.SwitchPooledObject();
    }
}
