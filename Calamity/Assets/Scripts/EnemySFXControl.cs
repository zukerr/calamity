using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFXControl : MonoBehaviour
{
    [SerializeField]
    private AudioSource walkingSFX;
    [SerializeField]
    private AudioSource punchSFX;
    [SerializeField]
    private AudioSource getHitSFX;
    
    public void PlayWalkingSFX()
    {
        walkingSFX.Play();
    }

    public void PlayPunchSFX()
    {
        //BAD CODING
        PlayerUtility.Instance.HealthModule.ModifyHealth(-1);

        punchSFX.Play();
    }

    public void PlayGetHitSFX()
    {
        getHitSFX.Play();
    }
}
