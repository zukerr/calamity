using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float aggroRadius = 5f;
    [SerializeField]
    private float combatRadius = 1f;
    [SerializeField]
    private Animator enemyAC = null;
    [SerializeField]
    private Collider enemyCollider = null;
    [SerializeField]
    private NavMeshAgent agent = null;
    [SerializeField]
    private float playerLeeway;
    [SerializeField]
    private EnemySFXControl enemySFX;
    [SerializeField]
    private GameObject hitPsPrefab = null;

    private bool inAggro = false;
    private bool isDead = false;

    private void Start()
    {
        LevelController.Instance.RegisterEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            return;
        }

        if(!inAggro)
        {
            if (PlayerUtility.Instance.GetDistanceFromPlayer(transform.position) < aggroRadius)
            {
                enemyAC.SetTrigger("Aggro");
                inAggro = true;
            }
        }
        
        if (PlayerUtility.Instance.GetDistanceFromPlayer(transform.position) < combatRadius)
        {
            enemyAC.SetBool("IsPlayerInMelee", true);
        }
        else
        {
            enemyAC.SetBool("IsPlayerInMelee", false);
        }

        if(inAggro)
        {
            agent.destination = PlayerUtility.Instance.transform.position - (PlayerUtility.Instance.GetVectorFromPositionToPlayer(transform.position).normalized * playerLeeway);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isDead)
        {
            if (collision.gameObject.GetComponent<BasicBullet>() != null)
            {
                enemyAC.SetBool("IsDead", true);
                isDead = true;
                Instantiate(hitPsPrefab, collision.GetContact(0).point, Quaternion.identity);
                LevelController.Instance.EnemyEliminated();
                enemyCollider.isTrigger = true;
                enemySFX.PlayGetHitSFX();
            }
        }
    }
}
