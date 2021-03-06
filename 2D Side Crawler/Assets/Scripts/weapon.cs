﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField] private float weaponDamage = 20f;
    [SerializeField] private bool shouldKnockOut = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<health>().ChangeHealth(weaponDamage, shouldKnockOut);
    }

}
