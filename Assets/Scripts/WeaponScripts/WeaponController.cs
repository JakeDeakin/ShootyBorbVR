using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : MonoBehaviour
{
    public float bulletCount;
    public float bulletSpeed;
    public float bulletDamage;
    public float firingSpeed;
    public float shotsPerFire;
    public GameObject bulletSpawn;
    public GameObject projectile;

    public PlayerInputHandler playerInputHandler;
    public ActionBasedController controller;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();
        controller = GetComponentInParent<ActionBasedController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputHandler != null)
        {
            if (controller.name.Contains("Left"))
            {
                if (playerInputHandler.GetLeftFireInputDown())
                {
                    Fire();
                    print("bang");
                }
            }

            if (controller.name.Contains("Right"))
            {
                if (playerInputHandler.GetRightFireInputDown())
                {
                    Fire();
                    print("bang");
                }
            }
            
        }
    }

    public void Fire()
    {
        if (Physics.Raycast(bulletSpawn.transform.position, transform.up * 100, out hit))
        {
            print(hit.collider.name);
            Debug.DrawLine(bulletSpawn.transform.position, hit.point, Color.yellow, 100);
        }
    }
}
