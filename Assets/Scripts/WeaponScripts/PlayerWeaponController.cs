using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class PlayerWeaponController : MonoBehaviour
{
    public float currentBulletCount;
    public float maxBulletCount;
    public float bulletDamage;
    public float firingSpeed;
    public int shotsPerFire;
    public float shotsLeft;
    public float weaponRange;

    public AudioClip gunFire;
    public AudioClip reload;
    public AudioClip noAmmo;

    public GameObject bulletSpawn;
    public GameObject goProjectile;

    public PlayerInputHandler playerInputHandler;
    public ActionBasedController controller;


    public float shotTime;
    TextMeshProUGUI ammoText;


    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();
        controller = GetComponentInParent<ActionBasedController>();

        shotsLeft = shotsPerFire;
        shotTime = Time.time;
        ammoText = GetComponentInChildren<TextMeshProUGUI>();
        ammoText.text = currentBulletCount.ToString();
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

        if (Physics.Raycast(bulletSpawn.transform.position, transform.forward * weaponRange, out hit))
        {
            print(hit.collider.name);
            Debug.DrawLine(bulletSpawn.transform.position, hit.point, Color.yellow, 100);
            GameObject line = Instantiate(goProjectile);
            line.GetComponent<LineRenderer>().SetPositions(new Vector3[] { bulletSpawn.transform.position, hit.point });
            Destroy(line, 0.2f);
        }
    }


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
}
