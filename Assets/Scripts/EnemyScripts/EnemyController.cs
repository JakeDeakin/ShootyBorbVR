using UnityEngine;
using ShootyBorbVRMath;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth;
    public int enemyMaxHealth;
    public EnemyWeaponController enemyWeapon;
    public GameObject player;
    public PlayerCharacterController pController;
    public Camera playerCamera;
    private Vector3 predictedPlayerDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>();
        pController = player.GetComponent<PlayerCharacterController>();
        enemyWeapon = GetComponentInChildren<EnemyWeaponController>();

        InvokeRepeating(nameof(TryFireWeapon), 1f, 1 / enemyWeapon.firingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
      LookAtPlayer();
    }

    void LookAtPlayer()
    {
        transform.LookAt(player.GetComponentInChildren<Camera>().transform);
    }

    bool CanHitPlayer()
    {
        if(transform.position.z < playerCamera.transform.position.z)
        {
            return false;
        }

        if(Math.InterceptionDirection(playerCamera.transform.position, transform.position, new Vector3(0, 0, pController.PlayerMovementSpeed), enemyWeapon.bulletSpeed, out var direction))
        {
            predictedPlayerDirection = direction;
            return true;
        }
        return false;
    }


    void TryFireWeapon()
    {
        if (CanHitPlayer())
        {
            enemyWeapon.Fire(predictedPlayerDirection);
        }
        
    }
}
