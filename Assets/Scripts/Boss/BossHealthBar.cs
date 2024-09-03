using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] BossHealth health;
    [SerializeField] RectTransform foreground;
    [SerializeField] GameObject healthbar;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreground.localScale = new Vector3(Mathf.Clamp(health.GetPercentage(), 0, health.MaxHealth), 1, 1);

        if (health.health <= 0 || health.health == health.MaxHealth)
        {
            healthbar.SetActive(false);
        }

        if (!health.isActiveAndEnabled)
        {
            healthbar.SetActive(false);
        }

        else
        {
            healthbar.SetActive(true);
        }
    }
}
