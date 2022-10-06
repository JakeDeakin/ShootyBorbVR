using UnityEngine;
using Unity.Mathematics;
using ShootyBorbVRMath;

public class EnemyWeaponController : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    public float firingSpeed;

    public AudioClip gunFire;

    public GameObject bulletSpawn;
    public Rigidbody rbProjectile;

    public PlayerInputHandler playerInputHandler;

    public PlayerCharacterController target;
    public Camera targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerCharacterController>();
        targetCamera = target.GetComponentInChildren<Camera>();

        InvokeRepeating(nameof(EnemyFire), 1f, 1/firingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyFire()
    {
        var instance = Instantiate(rbProjectile, bulletSpawn.transform.position, quaternion.identity);
        if (Math.InterceptionDirection(targetCamera.transform.position, transform.position, new Vector3(0,0,target.PlayerMovementSpeed), bulletSpeed, out var direction))
        {
            instance.velocity = direction * bulletSpeed;
        }
        else
            instance.velocity = (targetCamera.transform.position - transform.position).normalized * bulletSpeed;
    }
}
