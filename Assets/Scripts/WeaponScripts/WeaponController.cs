using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Mathematics;
using UnityEngine.InputSystem.XR;

public class WeaponController : MonoBehaviour
{
    public float bulletCount;
    public float bulletSpeed;
    public float bulletDamage;
    public float firingSpeed;
    public float shotsPerFire;
    public GameObject bulletSpawn;
    public Rigidbody projectile;

    public PlayerInputHandler playerInputHandler;
    public ActionBasedController controller;

    public PlayerCharacterController target;
    public Camera targetCamera;

    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerCharacterController>();
        targetCamera = target.GetComponentInChildren<Camera>();
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();
        controller = GetComponentInParent<ActionBasedController>();

        if (playerInputHandler == null)
        {
            InvokeRepeating(nameof(EnemyFire), 1f, 1/firingSpeed);
        }
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

}
