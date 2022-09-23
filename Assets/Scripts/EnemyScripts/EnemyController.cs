using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth;
    public int enemyMaxHealth;
    public WeaponController enemyWeapon;
    public PlayerCharacterController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       // FireWeapon();
    }

    void FireWeapon()
    {
        enemyWeapon.Fire();
    }
}
