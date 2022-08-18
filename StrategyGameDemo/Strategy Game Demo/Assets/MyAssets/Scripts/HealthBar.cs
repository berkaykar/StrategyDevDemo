using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private Damageable damageableScript;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        damageableScript = gameObject.transform.parent.parent.gameObject.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = damageableScript.GetMaxHealth();
        slider.value = damageableScript.GetHealth();
    }
}
