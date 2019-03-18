using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
#pragma warning disable 0649
    enum Type { Sword, Bow, Spikes, Spear } //types of weapons
    [SerializeField] Type weaponType;
    [SerializeField] private float weaponDamage = 20f;
    [SerializeField] private bool shouldKnockOut = false; //if this weapon can move enemies or player change it to true
    [SerializeField] private float attackRange;
    [SerializeField] private float startAttackSpeed; //weapon's attack speed
    Vector3 attackPosition;
    private float attackSpeed = 0;
    [SerializeField] LayerMask whatIsEnemies;

    private void Awake()
    {
        attackPosition = GetComponent<Transform>().transform.position;
        ChoseWeaponType();
    }
    
    private void Update()
    {
        attackPosition = GetComponent<Transform>().transform.position;
        DetectEnemiesInRange();
    }
    void DetectEnemiesInRange()
    {
        if(attackSpeed <= 0) //if attack speed has no cooldown
        {
            if(Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = EnemiesToDamage();
                for(int i=0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<health>().ChangeHealth(weaponDamage, shouldKnockOut);
                }
                attackSpeed = startAttackSpeed;
            } 
        }
        else
        {
            attackSpeed -= Time.deltaTime;
        }
    }
    
    //chose range for current type of weapon
    Collider2D[] EnemiesToDamage()
    {
        if(weaponType == Type.Sword)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, attackRange, whatIsEnemies);
            return enemiesToDamage;
        }
        else if(weaponType == Type.Spear)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPosition, new Vector2(attackRange, 0.1f), 3f, whatIsEnemies);
            return enemiesToDamage;
        }
        else
        return Physics2D.OverlapCircleAll(attackPosition, 0, whatIsEnemies);
    }

    void ChoseWeaponType()
    {
        switch(weaponType)
        {
            case Type.Spear:
                attackRange = 1.35f;
                break;
        }
            
    }
   
    //Draw weapon's range
    private void OnDrawGizmos()
    {
        if(weaponType!=Type.Spikes)
        {
            Gizmos.color = Color.red;
            switch (weaponType)
            {
                case Type.Sword:
                    Gizmos.DrawWireSphere(attackPosition, attackRange);
                    break;
                case Type.Spear:
                    Gizmos.DrawWireCube(attackPosition, new Vector3(attackRange, 0.1f, 0));
                    break;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponType == Type.Spikes)
            collision.gameObject.GetComponent<health>().ChangeHealth(weaponDamage, shouldKnockOut);
    }
}
