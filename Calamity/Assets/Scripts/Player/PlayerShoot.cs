using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Animator gunAnimator;
    [SerializeField]
    private GameObject barrelEnd;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Camera weaponCamera;
    [SerializeField]
    private float bulletSpeed = 1f;
    [SerializeField]
    private float shootingCooldown = 0.1f;
    [SerializeField]
    private List<AudioClip> shootingAudioClips = null;
    [SerializeField]
    private AudioSource gunSoundSFX = null;

    private bool shootOnCd = false;
    
    public void Shoot()
    {
        if(shootOnCd)
        {
            return;
        }

        gunAnimator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, barrelEnd.transform.position, barrelEnd.transform.rotation);
        bool bulletHitSomething = Physics.Raycast(weaponCamera.transform.position, weaponCamera.transform.forward, out RaycastHit hit, 1000f);
        Vector3 shootingVector;
        if (bulletHitSomething)
        {
            shootingVector = hit.point - barrelEnd.transform.position;
        }
        else
        {
            shootingVector = weaponCamera.transform.forward * 1000 - barrelEnd.transform.position;
        }
        
        shootingVector.Normalize();
        bullet.GetComponent<Rigidbody>().AddForce(shootingVector * bulletSpeed);
        PlayRandomGunSound();
        shootOnCd = true;
        StartCoroutine(WaitForCooldown());
    }

    private IEnumerator WaitForCooldown()
    {
        float cTime = shootingCooldown;
        while(cTime > 0f)
        {
            cTime -= Time.deltaTime;
            yield return null;
        }
        shootOnCd = false;
    }

    private void PlayRandomGunSound()
    {
        int rng = Random.Range(0, shootingAudioClips.Count);
        gunSoundSFX.clip = shootingAudioClips[rng];
        gunSoundSFX.Play();
    }
}
