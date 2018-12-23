using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    // make attack speed a part of CharacterStats
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCooldown = 5;
    float lastAttackTime;

    public float attackDelay = .6f;

    public bool InCombat { get; private set; }
    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats opponentStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();

        if (this.CompareTag("Player"))
        {
            attackSpeed = this.GetComponent<PlayerStats>().AttackSpeed.getValue();

            // !!! Debug, remove later !!!!
            if (attackSpeed == this.GetComponent<PlayerStats>().AttackSpeed.getValue())
            {
                Debug.Log("Successful player AttackSpeed setting.");
            }
        }
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    public void Attack (CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            //StartCoroutine(DoDamage(targetStats, attackDelay));
            opponentStats = targetStats;

            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1 / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
        
    }

    /*
    IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.Damage.getValue());
        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
*/

    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.Damage.getValue());
        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
