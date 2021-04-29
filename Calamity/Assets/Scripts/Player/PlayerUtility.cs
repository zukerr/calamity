using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtility : MonoBehaviour
{
    private static PlayerUtility instance;
    public static PlayerUtility Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private PlayerHealth healthModule = null;
    public PlayerHealth HealthModule => healthModule;

    private void Awake()
    {
        instance = this;
    }

    public float GetDistanceFromPlayer(Vector3 position)
    {
        return Vector3.Distance(transform.position, position);
    }

    public Vector3 GetVectorFromPositionToPlayer(Vector3 position)
    {
        return transform.position - position;
    }
}
