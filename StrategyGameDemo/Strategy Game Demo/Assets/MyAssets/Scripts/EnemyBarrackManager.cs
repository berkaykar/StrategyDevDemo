using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrackManager : BaseBuilding
{
    private GameObject myCanvas;
    protected override HealthInfo healthInfoText { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        health = 100;

        maxHealth = health;

        SetCanvases();

        myCanvas = enemyBarrackInfoCanvas;

        healthInfoText = GameObject.Find("Canvas/InformationCanvas/InformationTab/EnemyProductions/Barracks/BarrackInformation/BarrackInfo/HealthText").gameObject.GetComponent<HealthInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
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
}
