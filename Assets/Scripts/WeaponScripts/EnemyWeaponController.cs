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

    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 targetDirection)
    {
        var instance = Instantiate(rbProjectile, bulletSpawn.transform.position, quaternion.identity);

        instance.velocity = targetDirection * bulletSpeed; 
    }
}
