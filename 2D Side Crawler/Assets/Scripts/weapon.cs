using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    enum Type{ Sword, Bow, Spikes, Spear }
    [SerializeField] Type weaponType;
    [SerializeField] private float weaponDamage = 20f;
    [SerializeField] private bool shouldKnockOut = false;
    [SerializeField] private float attackRange;
    [SerializeField] private float startAttackSpeed;
    Vector3 attackPosition;
    private float attackSpeed = 0;
    [SerializeField] LayerMask whatIsEnemies;

    private void Start()
    {
        attackPosition = GetComponent<Transform>().transform.position;
        Debug.Log(attackPosition);
    }
    private void Update()
    {
        DetectEnemiesInRange();
    }
    void DetectEnemiesInRange()
    {
        if(attackSpeed <= 0)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<health>().ChangeHealth(weaponDamage, shouldKnockOut);
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
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPosition, new Vector2(attackRange, 0.1f), 3f);
            return enemiesToDamage;
        }
        else
        return Physics2D.OverlapCircleAll(attackPosition, 0, whatIsEnemies);
    }
   
    //Draw weapon's range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        switch(weaponType)
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
