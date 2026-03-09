using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI currencyDisplay;
    public TextMeshProUGUI healthDisplay;
    public float playerHealth = 100f;
    public float playerMaxHealth = 300f;
    public float playerSpeed = 1f;
    public float rotationSpeed = 1f;
    public float playerExeleration = 1f;
    public float spikeTrapSpeed = 1f;
    public float lavaTrapSpeed = 1f;
    public float humanMonsterAttackSpeed = 1f;
    public PlayableDirector CylinderTrapTimeline;
    public AudioSource AudioPotionPickup;
    public AudioClip potionPickup;
    public AudioSource AudioCoinPickup;
    public AudioClip CoinPickup;
    
    
    [SerializeField]
    private int playerCurrency;
    private float timeInTrigger = 1f;
    private bool inSpikeTrap = false;
    private bool inLavaFloor = false;
    private bool inHumanMonster = false;

    


    void Start()
    {
        AddGold();
        DisplayCurrency();
        DisplayPlayerHealth();
        


    }

    void Update()
    {
      //  Movement();
      
        if (inSpikeTrap)
        {
            timeInTrigger += Time.deltaTime;
            if (timeInTrigger >= spikeTrapSpeed)
            {
                timeInTrigger = 0f;
                SpikeTrapDamage();

            }
        }
        if (inLavaFloor)
        {
            timeInTrigger += Time.deltaTime;
            if (timeInTrigger >= lavaTrapSpeed)
            {
                timeInTrigger = 0f;
                LavaFloorDamage();

            }
        }
        if (inHumanMonster)
        {
            timeInTrigger += Time.deltaTime;
            if (timeInTrigger >= humanMonsterAttackSpeed)
            {
                timeInTrigger = 0f;
                HumanMonsterDamage();
            }
        }
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("Lose");
        }

        CylinderTrap();
        AudioPotionPickup = GetComponent<AudioSource>();
        AudioCoinPickup = GetComponent<AudioSource>();
       
    }

    private void Movement()
    {
        float forwardMovement = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        float sideMovement = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;


        transform.Translate(Vector3.forward * forwardMovement);
        transform.Rotate(Vector3.up * sideMovement); 
       




    }

    void AddGold()
    {
        GameObject[] goldPeices = new GameObject[] { };
        goldPeices = GameObject.FindGameObjectsWithTag("Gold Coin");
        playerCurrency = goldPeices.Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        //CollectItems
        if (other.gameObject.CompareTag("Gold Coin"))
        {
            playerCurrency--;
            DisplayCurrency();
            AudioCoinPickup.clip = CoinPickup;
            AudioCoinPickup.Play();
            other.gameObject.SetActive(false);
            if (playerCurrency == 0)
            {
                SceneManager.LoadScene("Win");
            }
            
        }
        
        if (other.gameObject.CompareTag("Health Potion"))
        { 
            playerHealth += 50;
            DisplayPlayerHealth();
            AudioPotionPickup.clip = potionPickup;
            AudioPotionPickup.Play();
            
            other.gameObject.SetActive(false);
            
        }
        if (other.gameObject.CompareTag("FireBall_Trap"))
        {
            playerHealth -= 20f;
            DisplayPlayerHealth();
        }
        if (other.gameObject.CompareTag("Danger"))
        {
            playerHealth -= 30f;
            DisplayPlayerHealth();
        }
      

    }

    void CylinderTrap()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CylinderTrapTimeline.Play();
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Lava_Floor"))
        { 
            inLavaFloor= true;
        }

        if (other.gameObject.CompareTag("Spike_Trap_Damage"))
        {
             
            inSpikeTrap = true;

        }
        if (other.gameObject.CompareTag("Human_Monster"))
        { 
            inHumanMonster = true;
        }
        
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lava_Floor"))
        {
            timeInTrigger = 0f;
            inLavaFloor = false;
        }
        if (other.gameObject.CompareTag("Spike_Trap_Damage"))
        {
           
            
            timeInTrigger = 0f;
            inSpikeTrap = false;
        }
        if (other.gameObject.CompareTag("Human_Monster"))
        {
            timeInTrigger = 0f;
            inHumanMonster = false;
        }
    }


    void DisplayCurrency()
    {
        currencyDisplay.text = "Gold: " + playerCurrency.ToString();
    }

    void DisplayPlayerHealth()
    { 
        healthDisplay.text = "Health " + playerHealth.ToString();
    }

    void SpikeTrapDamage()
    {
        if (playerHealth != 0)
        {
            
            playerHealth -= 20f;
            DisplayPlayerHealth();
        }
    }
    void LavaFloorDamage()
    {
        if (playerHealth != 0)
        {
            playerHealth -= 35f;
            DisplayPlayerHealth();
        }
    }
    void HumanMonsterDamage()
    {
        if (playerHealth != 0)
        {
            playerHealth -= 40;
            DisplayPlayerHealth();
        }
    }

   
   
   

  
}
