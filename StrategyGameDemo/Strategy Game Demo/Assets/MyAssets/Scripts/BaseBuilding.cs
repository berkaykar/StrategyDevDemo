using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseBuilding : Damageable
{
    protected ProductionCanvas proCanvas;
    protected GameObject barrackInfoCanvas, powerPlantInfoCanvas, enemyBarrackInfoCanvas, informationCanvas;

    protected List<GameObject> canvases;

    protected abstract HealthInfo healthInfoText { get; set; }

    protected bool isOpen = false;

    protected void SetCanvases()
    {
        canvases = new List<GameObject>();

        proCanvas = GameObject.FindObjectOfType<ProductionCanvas>();

        informationCanvas = GameObject.Find("Canvas/InformationCanvas/InformationTab");

        barrackInfoCanvas = GameObject.Find("Canvas/InformationCanvas/InformationTab/AllyProductions/Barracks");
        canvases.Append(barrackInfoCanvas);

        powerPlantInfoCanvas = GameObject.Find("Canvas/InformationCanvas/InformationTab/AllyProductions/WindMill");
        canvases.Append(powerPlantInfoCanvas);

        enemyBarrackInfoCanvas = GameObject.Find("Canvas/InformationCanvas/InformationTab/EnemyProductions/Barracks");
        canvases.Append(enemyBarrackInfoCanvas);
    }

    protected void SetHealthText()
    {
        healthInfoText.infoObject = this;
    }

    protected abstract void MouseUp();
    protected abstract void MouseDown();

}
