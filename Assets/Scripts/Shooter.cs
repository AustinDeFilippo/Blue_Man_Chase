using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject pistolShooter;
    public GameObject BibleShooter;
    public Rigidbody Pistol_Bullet;
    public Rigidbody Bible_Bullet;
    public Transform Sight;
    public AudioSource AudioWeaponPickup;
    public AudioClip WeaponPickup;


    bool hasPistolShooter;
    bool hasBibleShooter;
    bool isPistolEquipped = false;
    bool isBibleEquipped = false;

    int pistolAmmo = 6;
    int bibleAmmo = 6;


    void Awake()
    {
        ResetGame();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        HandleShooters();
        ShootShooters();
    }

    void ResetGame()
    { 
        pistolShooter.SetActive(false);
        BibleShooter.SetActive(false);
        hasPistolShooter = false;
        hasBibleShooter = false;
    }

    void HandleShooters()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasPistolShooter)
        {
            pistolShooter.SetActive(true);
            BibleShooter.SetActive(false);
            isPistolEquipped = true;
            isBibleEquipped = false; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasBibleShooter)
        {
            BibleShooter.SetActive(true);
            pistolShooter.SetActive(false);
            isBibleEquipped=true;
            isPistolEquipped=false;
        }
    }

    void ShootShooters()
    {
        if (isPistolEquipped && Input.GetKeyDown(KeyCode.F) && pistolAmmo > 0)
        { 
            Rigidbody pistolBulletCLone = Instantiate(Pistol_Bullet, Sight.transform.position, transform.rotation);
            pistolBulletCLone.velocity = transform.TransformDirection(Vector3.forward * 10);
            pistolAmmo--;
        }
        if (isBibleEquipped && Input.GetKeyDown(KeyCode.F) && bibleAmmo > 0)
        {
            Rigidbody bibleBulletClone = Instantiate(Bible_Bullet, Sight.transform.position, transform.rotation);
            bibleBulletClone.velocity = transform.TransformDirection(Vector3.forward * 10);
            bibleAmmo--;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pistol_Pickup"))
        {
            hasPistolShooter = true;
            AudioWeaponPickup.clip = WeaponPickup;
            AudioWeaponPickup.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bible_Pickup"))
        { 
            hasBibleShooter= true;
            AudioWeaponPickup.clip = WeaponPickup;
            AudioWeaponPickup.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Pistol_Ammo_Box"))
        {
            pistolAmmo += 6;
            AudioWeaponPickup.clip = WeaponPickup;
            AudioWeaponPickup.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bible_Ammo_Box"))
        {
            bibleAmmo += 6;
            AudioWeaponPickup.clip = WeaponPickup;
            AudioWeaponPickup.Play();
            Destroy(other.gameObject);
        }
    }
}
