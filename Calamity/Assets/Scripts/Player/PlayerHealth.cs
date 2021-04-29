using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> healthBars = null;

    private int healthValue = 3;
    private const int MAX_HEALTH = 3;

    public void ModifyHealth(int value)
    {
        healthValue += value;
        for(int i = 0; i < healthValue; i++)
        {
            healthBars[i].SetActive(true);
        }
        for(int i = healthValue; i < MAX_HEALTH; i++)
        {
            healthBars[i].SetActive(false);
        }

        if(healthValue == 0)
        {
            //TO-DO
            //Game Over
            LevelController.Instance.OnGameOver();
        }
    }
}
