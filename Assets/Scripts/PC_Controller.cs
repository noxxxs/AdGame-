using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PC_Controller : MonoBehaviour
{
    [SerializeField] ShootAbility _ShootAbility;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ShootAbility.TriggerAbility();
        }
    }
}
