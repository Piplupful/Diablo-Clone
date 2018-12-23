using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    // Stats
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat Damage;
    public Stat AttackSpeed;
    public Stat CritChance;
    public Stat FireDamage;         // Elemental damage can be changed if design changes
    public Stat ColdDamage;         // Accuracy or nah?
    public Stat LightningDamage;
    public Stat Armor;


    public event System.Action<int, int> OnHealthChanged;

    public void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Test damage button (t), works on all characters in scene

        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
        */
    }

    public void TakeDamage(int damage)
    {
        // DAMAGE CALCULATION

        float critRNG = Random.Range(0, 100);
        // Debug.Log("CRIT # = " + critRNG);

        damage -= Armor.getValue();
        damage = Mathf.Clamp(damage, 1, int.MaxValue);  // 1 damage is the lowest possible

        if (critRNG <= CritChance.getValue())
        {
            damage = damage * 2;
        }

        // TAKE DAMAGE

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        // Death if HP hits 0
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // Overwrite
        Debug.Log(transform.name + " died.");
    }
}
