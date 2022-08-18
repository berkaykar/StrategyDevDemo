using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerManager : BaseBuilding
{
    private GameObject myCanvas;
    protected override HealthInfo healthInfoText { get; set; }

    [SerializeField] private GameObject cannon, cannonStart;

    private float coolDown = 2;

    // Start is called before the first frame update
    void Start()
    {
        health = 500;

        maxHealth = health;

        SetCanvases();

        myCanvas = enemyBarrackInfoCanvas;

        healthInfoText = GameObject.Find("Canvas/InformationCanvas/InformationTab/EnemyProductions/CannonTower/CannonTowerInformation/CannonTowerInfo/HealthText").gameObject.GetComponent<HealthInfo>();
        
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget[] newSoldiers = GameManager.Instance.soldiers;
        
        if (health <= 0)
        {
            Die();
        }

        for (int i = 0; i < newSoldiers.Length; i++)
        {
            if ((newSoldiers[i].gameObject.transform.position - transform.position).magnitude < 7)
            {
                if (coolDown - Time.deltaTime <= 0)
                {
                    coolDown = 2;
                    
                    Shoot(newSoldiers[i].gameObject);
                    break;
                }

            }
        }
    }

    void OnMouseUp()
    {
        MouseUp();
    }


    protected override void MouseUp()
    {

        if (!isOpen)
        {

            proCanvas.OpenCanvas(informationCanvas);

            proCanvas.CloseCanvas(powerPlantInfoCanvas);
            proCanvas.CloseCanvas(barrackInfoCanvas);

            proCanvas.OpenCanvas(enemyBarrackInfoCanvas);

            SetHealthText();
        }
        else
        {
            proCanvas.CloseCanvas(enemyBarrackInfoCanvas);
            proCanvas.CloseCanvas(informationCanvas);
        }

    }

    protected override void MouseDown()
    {
        throw new System.NotImplementedException();
    }

    void Shoot(GameObject soldier)
    {
        GameObject newCannon = Instantiate(cannon, cannonStart.transform.position, Quaternion.identity);

        Vector2.Lerp(cannonStart.transform.position, soldier.transform.position, Time.deltaTime * 30);
    }
}
