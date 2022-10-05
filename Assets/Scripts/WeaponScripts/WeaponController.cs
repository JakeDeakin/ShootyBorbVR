using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
<<<<<<< HEAD
using TMPro;
=======
using Unity.Mathematics;
using UnityEngine.InputSystem.XR;
>>>>>>> main

public class WeaponController : MonoBehaviour
{
    public float currentBulletCount;
    public float maxBulletCount;
    public float bulletSpeed;
    public float bulletDamage;
    public float firingSpeed;
    public int shotsPerFire;
    public float shotsLeft;
    public float weaponRange;

    public AudioClip gunFire;
    public AudioClip reload;
    public AudioClip noAmmo;

    public GameObject bulletSpawn;
    public Rigidbody projectile;

    public PlayerInputHandler playerInputHandler;
    public ActionBasedController controller;

<<<<<<< HEAD
    public float shotTime;
    TextMeshProUGUI ammoText;
=======
    public PlayerCharacterController target;
    public Camera targetCamera;
>>>>>>> main

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerCharacterController>();
        targetCamera = target.GetComponentInChildren<Camera>();
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();
        controller = GetComponentInParent<ActionBasedController>();
<<<<<<< HEAD
        shotsLeft = shotsPerFire;
        shotTime = Time.time;
        ammoText = GetComponentInChildren<TextMeshProUGUI>();
        ammoText.text = currentBulletCount.ToString();
=======

        if (playerInputHandler == null)
        {
            InvokeRepeating(nameof(EnemyFire), 1f, 1/firingSpeed);
        }
>>>>>>> main
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputHandler != null)
        {
            if (controller.name.Contains("Left"))
            {
                if (playerInputHandler.GetLeftFireInputHeld())
                {
                    if (Time.time > shotTime + 1 / firingSpeed)
                    {
                        TryFire();

                        print("left trigger held");
                    }
                }

                if (playerInputHandler.GetLeftFireInputReleased())
                {
                    shotsLeft = shotsPerFire;
                    shotTime = 0;
                }
            }

            if (controller.name.Contains("Right"))
            {
                
                if (playerInputHandler.GetRightFireInputHeld())
                {
                    if (Time.time > shotTime + 1 / firingSpeed)
                    {
                        TryFire();

                        print("right trigger held");
                    }

                }

                if (playerInputHandler.GetRightFireInputReleased())
                {
                    shotsLeft = shotsPerFire;
                    shotTime = 0;
                }
            }


            if ((Vector3.Angle(bulletSpawn.transform.up, Vector3.up) > 80 || Vector3.Angle(bulletSpawn.transform.up, Vector3.up) < -80) && currentBulletCount != maxBulletCount)
            {
                Reload();
            }

        }
    }

    public void Fire()
    {
        currentBulletCount--;
        shotsLeft--;
        shotTime = Time.time;
        ammoText.text = currentBulletCount.ToString();

        if (Physics.Raycast(bulletSpawn.transform.position, transform.up * weaponRange, out hit))
        {
            print(hit.collider.name);
            Debug.DrawLine(bulletSpawn.transform.position, hit.point, Color.yellow, 100);
            GameObject line = Instantiate(projectile);
            line.GetComponent<LineRenderer>().SetPositions(new Vector3[] { bulletSpawn.transform.position, hit.point });
            Destroy(line, 0.2f);
        }
    }

<<<<<<< HEAD
    public bool CanFire()
    {
        if (HasAmmo() && HasShotsLeft())
        {
            return true;
        }
        
        return false;
    }

    public bool HasAmmo()
    {
        if (currentBulletCount > 0)
        {
            return true;
        }

        return false;
    }

    public bool HasShotsLeft()
    {
        if (shotsLeft > 0)
        {
            return true;
        }

        return false;
    }

    public void TryFire()
    {
        if (CanFire())
        {
            Fire();
        }
    }

    public void Reload()
    {
            currentBulletCount = maxBulletCount;
            ammoText.text = currentBulletCount.ToString();
            print("Reload");
    }
=======
    public void EnemyFire()
    {
        var instance = Instantiate(projectile, bulletSpawn.transform.position, quaternion.identity);
        if (InterceptionDirection(targetCamera.transform.position, transform.position, new Vector3(0,0,target.PlayerMovementSpeed), bulletSpeed, out var direction))
        {
            print("Did this work?");
            instance.velocity = direction * bulletSpeed;
        }
        else
            instance.velocity = (targetCamera.transform.position - transform.position).normalized * bulletSpeed;
    }

    // Math courtesy of jean-gobert de coster on Youtube. Works beautifully! https://www.youtube.com/user/jeangodecoster

    public bool InterceptionDirection(Vector3 a, Vector3 b, Vector3 vA, float sB, out Vector3 result)
    {
        var aToB = b - a;
        var dC = aToB.magnitude;
        var alpha = Vector3.Angle(aToB, vA) * Mathf.Deg2Rad;
        var sA = vA.magnitude;
        var r = sA / sB;

        if (SolveQuadratic(1-r*r, 2*r*dC*Mathf.Cos(alpha), -(dC*dC), out var root1, out var root2) == 0)
        {
            result = Vector3.zero;
            return false;
        }

        var dA = Mathf.Max(root1, root2);
        var t = dA / sB;
        var c = a + vA * t;
        result = (c - b).normalized;
        return true;    

    }

    // Math courtesy of jean-gobert de coster on Youtube. Works beautifully! https://www.youtube.com/user/jeangodecoster
    public static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
    {
        var discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            root1 = Mathf.Infinity;
            root2 = -root1;
            return 0;
        }

        root1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        return discriminant > 0 ? 2 : 1;
    }

>>>>>>> main
}
