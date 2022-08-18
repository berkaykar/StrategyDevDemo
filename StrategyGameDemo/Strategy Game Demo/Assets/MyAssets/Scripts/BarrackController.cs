using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class BarrackController : AllyBuilding
{
    protected override GameObject childRenderer { get; set; }
    protected override GameObject soldierExitPoint { get; set; }
    protected override GameObject myCanvas { get; set; }
    protected override Vector3 lclScale { get; set; }
    protected override int cost { get; set; }
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

                proCanvas.CloseCanvas(powerPlantInfoCanvas);
                proCanvas.CloseCanvas(enemyBarrackInfoCanvas);
                
                proCanvas.OpenCanvas(barrackInfoCanvas);

                SetHealthText();
            }
            else
            {
                proCanvas.CloseCanvas(barrackInfoCanvas);
                proCanvas.CloseCanvas(informationCanvas);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        childRenderer = null;
        soldierExitPoint = GameObject.Find("SoldierExitPoint");
        myCanvas = barrackInfoCanvas;
        cost = 50;
        lclScale = new Vector3(4, 4, 0);

        health = 100;
        maxHealth = health;

        healthInfoText = GameObject.Find("Canvas/InformationCanvas/InformationTab/AllyProductions/Barracks/BarrackInformation/BarrackInfo/HealthText").gameObject.GetComponent<HealthInfo>();
    }

    void OnMouseUp()
    {
        MouseUp();
    }

    void OnMouseDown()
    {
        MouseDown();
    }

}
