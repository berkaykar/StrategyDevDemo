using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthInfo : MonoBehaviour
{
    public Damageable infoObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "Health   =   " + infoObject.GetHealth().ToString() + " / " + infoObject.GetMaxHealth().ToString();
    }
}
