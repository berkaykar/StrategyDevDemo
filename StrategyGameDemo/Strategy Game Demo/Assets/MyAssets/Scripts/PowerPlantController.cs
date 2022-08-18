using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class PowerPlantController : AllyBuilding
{
    protected override GameObject childRenderer { get; set; }
    protected override GameObject soldierExitPoint { get; set; }
    protected override GameObject myCanvas { get; set; }
    protected override Vector3 lclScale { get; set; }
    protected override int cost { get; set; }

    public int coinRateIncrease = 1;
    private bool isContribute = false;
    protected override HealthInfo healthInfoText { get; set; }
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

                proCanvas.CloseCanvas(enemyBarrackInfoCanvas);
                proCanvas.CloseCanvas(barrackInfoCanvas);

                proCanvas.OpenCanvas(powerPlantInfoCanvas);
                
                SetHealthText();
            }
            else
            {
                proCanvas.CloseCanvas(powerPlantInfoCanvas);
                proCanvas.CloseCanvas(informationCanvas);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        health = 75;
        maxHealth = health;

        cost = 25;
        childRenderer = this.gameObject.transform.GetChild(0).gameObject;

        soldierExitPoint = null;

        myCanvas = powerPlantInfoCanvas;
        lclScale = new Vector3(2, 3, 0);

        healthInfoText = GameObject.Find("Canvas/InformationCanvas/InformationTab/AllyProductions/WindMill/WindMillInformation/WindMillInfo/HealthText").gameObject.GetComponent<HealthInfo>();
    }


    void OnMouseDown()
    {
        MouseDown();
    }

    void OnMouseUp()
    {
        MouseUp();

        if (isDone)
        {
            if (!isContribute)
            {
                GameManager.Instance.IncreaseCoinRate(coinRateIncrease);

                isContribute = true;
            }
        }
    }

}
