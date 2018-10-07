using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOffense : MonoBehaviour {

    //Melee Stats
    private float meleeDistance = 5f;


    //Modifiable Stats
    public string Melee = "MeleeP1";
    public string GroundPound = "GroundPoundP1";
    public string Ability1 = "Ability1P1";
    public string Ability2 = "Ability2P1";
   


    //Damage Values
    private float GroundPoundDamage = 15f;
    public bool GroundPoundAttack = false;

    /// ///////////////////////////////////////////////////////    /// ///////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////    /// ///////////////////////////////////////////////////////    /// ///////////////////////////////////////////////////////

    //Righeous fire
    public float RighteousFireDamage = 2f;                           //How much damage is inflict from RF
    public float RighteousFireMultiDamage = 0.2f;                    //How much damage the multi hits inflict
    private float RighteousFireMultiDamageNode = 1.1f;               //Extra Damage from the skill tree
    public float RighteousFireIFrames = 0f;                          //Iframes for RF
    public float RighteousFireIFramesMax = 1f;                       //Maximum Amount of IFrames given to a player after been hit by the attack
    private int RighteousFireMultiStrike = 4;                        //How many times the MultiStike hits
   
    private float RFCostPS = 1f;                                     //How quickly mana is depleted

    //MultiHit Speed
    private float righteousFireMultiHittimer = 0;
    private float righteousFireMultiHittimerMax = 1f;
    private float RFCastTime = 0;
    private float RFCost = 5f;

    //LifeSteal
    private float RFHPLifeSteal = 0f;                               //Steal HP from Enemy and absord a percent of it
    private float RFMPLifeSteal = 0f;

    /// ///////////////////////////////////////////////////////    /// ///////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////    /// ///////////////////////////////////////////////////////    /// ///////////////////////////////////////////////////////

    //Energy Blast
    private int MultiHit = 2;
    private float EBDamage = 15f;
    private float EBlastRadius = 1.5f;
    private float EBlastSpeed = 150f;
    private float EBHPLifeSteal = 0;
    private float EBMPLifeSteal = 0;
    private float EBCrit = 0;
    private float AoERadius = 0;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private float EBRateOfFire = 0f;
    private float EBRateOfFireMax = 1f;
    private float EBRateOfFireMuliti = 0f;
    private float EBRateOfFireMulitiMax = 0.3f;
    private int EBRateOfFireMulitiCount = 0;
    private int EBRateOfFireMulitiCountMax = 0;
    private float EBRateOfFireMulitiCool = 0;
    private int EBRateOfFireMulitiCoolMax = 1;

    private float ProjectileLife = 0.75f;


    public Vector3 rotation;
    public Quaternion q;


    public int PlayerNumber = 1;

    private float attackPause = 0;
    private float attackPauseTimer = 0.5f;


    private GameObject MatchController;
    PlayerMovement movementScript;
    CharacterController Controller;
    PlayerHealth PlayerHP;
    PlayerOffense PlayerAttack;


    private float radius = 7.5f;

    ///////////////
    ///Silence Status Ailment Variables
    private float _silenceTimer = 0;
    private float _silenceTimerMax = 5f;


    // Use this for initialization
    void Start ()
    {
        Quaternion q = Quaternion.AngleAxis(100 * Time.time, Vector3.up);
        Vector3 Rotation = transform.forward * 20;
        movementScript = GetComponent<PlayerMovement>();
        Controller = GetComponent<CharacterController>();
        PlayerHP = GetComponent<PlayerHealth>();
        PlayerAttack = GetComponent<PlayerOffense>();
        MatchController = GameObject.FindWithTag("GameController");
        EBRateOfFireMulitiCountMax = MultiHit;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(Melee))
        {
            print("Attack");
            RaycastHit Attack;
            Ray rayForward = new Ray(transform.position, transform.forward);

            Debug.DrawRay(transform.position, transform.forward);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Physics.Raycast(rayForward, out Attack) && Attack.collider.CompareTag("Player") && !(Attack.collider.gameObject == gameObject) && Attack.distance < meleeDistance)
            {
                Attack.collider.GetComponent<PlayerHealth>().AddDamage(50, this.gameObject);
            }
        }

        //If Player is in the air and Ground Pound Button is pressed
        if (Controller.isGrounded == false && Input.GetButtonDown(GroundPound))
        {
            //Ground Pound is active. Reset the Pause before the attack
            GroundPoundAttack = true;
            attackPause = 0;

            //Set Ground pound to be active in Movement Script
            GetComponent<PlayerMovement>().GroundPoundactive = true;
        }


        if (GroundPoundAttack == true && GetComponent<PlayerMovement>().GroundPoundMove == false)
        {
            if (attackPause <= attackPauseTimer)
            {
                attackPause += 1 * Time.deltaTime;
                GetComponent<PlayerMovement>().VerticalVelocity = 0;
                GetComponent<PlayerMovement>().StopFall = true;

            }
            else
            {
                GetComponent<PlayerMovement>().GroundPoundMove = true;
                GetComponent<PlayerMovement>().StopFall = false;

            }
        }
        if (GroundPoundAttack == true && Controller.isGrounded == true)
        {
            GroundPoundAction(transform.position, radius);
            GroundPoundAttack = false;
        }

        if (GetComponent<PlayerMovement>().GroundPoundactive == true && GetComponent<PlayerMovement>().StopFall == false)
        {
            attackPause = 0;
            GetComponent<PlayerMovement>().VerticalVelocity -= 2f;
            CencelGroundPound();
            RaycastHit hit;
            Ray rayForward = new Ray(transform.position, transform.forward);

            Debug.DrawRay(transform.position, transform.forward);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Physics.Raycast(rayForward, out hit) && hit.collider.CompareTag("Floor") && hit.distance < 1 && Controller.isGrounded == false)
            {
                GetComponent<PlayerMovement>().GroundPoundMove = false;
            }
        }

        //If Silence Status Ailment is Active
        if (PlayerHP.Silence == false)
        {
            //Righteous Fire Ability
            if (RighteousFireIFrames < RighteousFireIFramesMax)
            {
                RighteousFireIFrames += 1 * Time.deltaTime;
            }
            if (Input.GetAxis(Ability1) > 0.5)
            {
                RighteousFire(transform.position, radius);
            }
            if (Input.GetAxis(Ability2) > 0.5)
            {
                EnergyBall();
            }

        }
        else
        {
            //If Silence Status Ailment is active. Count down until is it passes
            if (_silenceTimer < _silenceTimerMax)
            {
                _silenceTimer += 1 * Time.deltaTime;
            }
            else
            {
                PlayerHP.Silence = false;
            }
              

        }
    }

    void EnergyBall()
    {
        // Create the Bullet from the Bullet Prefab
        if (EBRateOfFire > EBRateOfFireMax)
        {
            // Add velocity to the bullet
            var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, movementScript.CameraT.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * EBlastSpeed;

            EnergyBall PlayerShooter = bullet.GetComponent<EnergyBall>();
            PlayerShooter.Shooter = this.gameObject;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, ProjectileLife);
            EBRateOfFire = 0;

        }
        else
        {
            EBRateOfFire += 1 * Time.deltaTime;
        }
        if (MultiHit > 0)
        {
           //Can the Player shoot any more projectiles for Multi Cast?
            if (EBRateOfFireMulitiCount > 0)
            {
                //Cast Timer for MultiCast
                if (EBRateOfFireMuliti > EBRateOfFireMulitiMax)
                {
                    EBRateOfFireMulitiCount -= 1;
                    EBRateOfFireMuliti = 0;
                    // Add velocity to the bullet
                    var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, movementScript.CameraT.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * EBlastSpeed;
                    EnergyBall PlayerShooter = bullet.GetComponent<EnergyBall>();
                    PlayerShooter.Shooter = this.gameObject;


                    // Destroy the bullet after 2 seconds
                    Destroy(bullet,ProjectileLife);
                    EBRateOfFire = 0;
                }
                else
                {
                    EBRateOfFireMuliti += (1 * Time.deltaTime) * 3;
                }
            }
            else
            {
                //Cooldown
                if (EBRateOfFireMulitiCool > EBRateOfFireMulitiCoolMax)
                {
                    EBRateOfFireMulitiCount = EBRateOfFireMulitiCountMax;
                    EBRateOfFireMulitiCool = 0;
                }
                else
                {
                    EBRateOfFireMulitiCool += 1 * Time.deltaTime;
                }

            }
        }
    }
    //Ground Pound Attack
    void GroundPoundAction(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //Is the object another player and not the caster.
            if (hitColliders[i].CompareTag("Player") && !(hitColliders[i].gameObject == gameObject))
            {
                //Gonna Change to a less intensive method. needs to send a value as well.
                hitColliders[i].GetComponent<PlayerHealth>().AddDamage(GroundPoundDamage,this.gameObject);

                hitColliders[i].GetComponent<PlayerHealth>().transform.Translate(this.transform.forward);
                if (hitColliders[i].GetComponent<PlayerHealth>().Health <= 0)
                {
                    switch (PlayerNumber)
                    {
                        //If Game is 2 Player
                        default:
                            {
                                break;
                            } 
                        case 1:
                            {
                                MatchController.GetComponent<GameController>().UpdateScore(1,(1* MatchController.GetComponent<GameController>().Score_Modifier));
                                break;
                            } 
                        case 2:
                            {
                                MatchController.GetComponent<GameController>().UpdateScore(2, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                                break;
                            }
                        case 3:
                            {
                                MatchController.GetComponent<GameController>().UpdateScore(3, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                                break;
                            }
                        case 4:
                            {
                                MatchController.GetComponent<GameController>().UpdateScore(4, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                                break;
                            } 
                    }
                    
                }

            }
            //Next in Array of objects in Sphere Overlay
            i++;
        }
    }
    //Ground Pound Attack
    void CencelGroundPound()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //Is the object another player and not the caster.
            if (hitColliders[i].CompareTag("Floor") && Controller.isGrounded == false)
            {
                GetComponent<PlayerMovement>().GroundPoundMove = false;
                GetComponent<PlayerMovement>().StopFall = false;
                attackPause = 0;
                GroundPoundAttack = false;
            }
            //Next in Array of objects in Sphere Overlay
            i++;
        }
    }

    //Righteous Fire Ability CAll
    void RighteousFire(Vector3 center, float radius)
    {
        if (PlayerHP.Mana >= RFCost)
        {
            if (RFCostPS >= 1)
            {
                PlayerHP.Mana -= RFCost;
                RFCostPS = 0;
                print(PlayerHP.Mana);
            }
            else
            {
                RFCostPS += 1 * Time.deltaTime;
            }

            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                //Is the object another player and not the caster.
                if (hitColliders[i].CompareTag("Player") && !(hitColliders[i].gameObject == gameObject))
                {
                    if (hitColliders[i].GetComponent<PlayerOffense>().RighteousFireIFrames >= hitColliders[i].GetComponent<PlayerOffense>().RighteousFireIFramesMax)
                    {

                        //Gonna Change to a less intensive method. needs to send a value as well.
                        hitColliders[i].GetComponent<PlayerHealth>().AddDamage(RighteousFireDamage, this.gameObject);

                        //If player has invested in Life Absorb, steal HP And Or Mana
                        if (RFHPLifeSteal > 0)
                        {
                            PlayerHP.Health += (RighteousFireDamage * RFHPLifeSteal);
                        }
                        if (RFMPLifeSteal > 0)
                        {
                            PlayerHP.Mana += (RighteousFireDamage * RFMPLifeSteal);
                        }
                        print("Hit Fire");
                        hitColliders[i].GetComponent<PlayerOffense>().RighteousFireIFrames = 0;

                        hitColliders[i].GetComponent<PlayerHealth>().transform.Translate(Vector3.forward);
                        if (hitColliders[i].GetComponent<PlayerHealth>().Health <= 0)
                        {
                            switch (PlayerNumber)
                            {
                                //If Game is 2 Player
                                default:
                                    {
                                        break;
                                    }
                                case 1:
                                    {
                                        MatchController.GetComponent<GameController>().UpdateScore(1, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                                        break;
                                    }
                                case 2:
                                    {
                                        MatchController.GetComponent<GameController>().UpdateScore(2, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                                        break;
                                    }
                                case 3:
                                    {
                                        MatchController.GetComponent<GameController>().UpdateScore(3, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                                        break;
                                    }
                                case 4:
                                    {
                                        MatchController.GetComponent<GameController>().UpdateScore(4, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                                        break;
                                    }
                            }

                        }
                    }

                }
                //Next in Array of objects in Sphere Overlay
                i++;
            }
            //Every Second, Spawn a MultiHit
            if (righteousFireMultiHittimer >= righteousFireMultiHittimerMax)
            {
                righteousFireMultiHittimer = 0;
                if (RighteousFireMultiStrike == 4)
                {
                    RighteousFireMultiHit();
                    RighteousFireMultiHit();
                    RighteousFireMultiHit();
                    RighteousFireMultiHit();
                }
                else if (RighteousFireMultiStrike == 3)
                {
                    RighteousFireMultiHit();
                    RighteousFireMultiHit();
                    RighteousFireMultiHit();
                }
                else if (RighteousFireMultiStrike == 2)
                {
                    RighteousFireMultiHit();
                    RighteousFireMultiHit();
                }
                else if (RighteousFireMultiStrike == 1)
                {
                    RighteousFireMultiHit();
                }
            }
            else
            {
                righteousFireMultiHittimer += 1 * Time.deltaTime;
            }
        }
    }
    //Handles the Righteous Fire Multihit. Spawns a seperate Hitbox around the player at random.
    void RighteousFireMultiHit()
    {
        //rotation = Random.rotation.eulerAngles;
        Debug.DrawRay(transform.position, q * rotation, Color.green);
        RaycastHit Attack1;
        q = Quaternion.AngleAxis(100 * Time.time, Vector3.up);
        q.w = Random.Range(-1f, 1f);
        q.y = Random.Range(-1f, 1f);
        rotation = transform.forward * 20;
        if (Physics.SphereCast(transform.position, radius, q * rotation, out Attack1, 64) && Attack1.collider.CompareTag("Player") && !(Attack1.collider.gameObject == gameObject))
        {
            print("Attack");
            //Gonna Change to a less intensive method. needs to send a value as well.
            Attack1.collider.GetComponent<PlayerHealth>().AddDamage(RighteousFireMultiDamage * RighteousFireMultiDamageNode, this.gameObject);
            print("Hit Fire");
            Attack1.collider.GetComponent<PlayerOffense>().RighteousFireIFrames = 0;
            if (Attack1.collider.GetComponent<PlayerHealth>().Health <= 0)
            {
                switch (PlayerNumber)
                {
                    //If Game is 2 Player
                    default:
                        {
                            break;
                        }
                    case 1:
                        {
                            MatchController.GetComponent<GameController>().UpdateScore(1, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                            break;
                        }
                    case 2:
                        {
                            MatchController.GetComponent<GameController>().UpdateScore(2, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                            break;
                        }
                    case 3:
                        {
                            MatchController.GetComponent<GameController>().UpdateScore(3, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                            break;
                        }
                    case 4:
                        {
                            MatchController.GetComponent<GameController>().UpdateScore(4, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                            break;
                        }
                }

            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnergyBall"  && other.GetComponent<EnergyBall>().Shooter != this.gameObject)
        {
            print(other.GetComponent<EnergyBall>().Shooter.gameObject);
            //Gonna Change to a less intensive method. needs to send a value as well.
            PlayerHP.AddDamage(EBDamage, other.GetComponent<EnergyBall>().Shooter.gameObject);

            //If player has invested in Life Absorb, steal HP And Or Mana
            if (EBHPLifeSteal > 0)
            {
                other.GetComponent<EnergyBall>().Shooter.GetComponent<PlayerHealth>().Health += (EBDamage * EBHPLifeSteal);
            }
            if (EBMPLifeSteal > 0)
            {
                other.GetComponent<EnergyBall>().Shooter.GetComponent<PlayerHealth>().Mana += (EBDamage * EBMPLifeSteal);
            }

            if (PlayerHP.Health <= 0)
            {
                print("access");
                switch (other.GetComponent<EnergyBall>().Shooter.GetComponent<PlayerHealth>().PlayerNumber)
                {
                    
                    //If Game is 2 Player
                    default:
                        {
                            break;
                        }
                    case 1:
                        {
                            MatchController.GetComponent<GameController>().UpdateScore(1, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                            break;
                        }
                    case 2:
                        {
                            MatchController.GetComponent<GameController>().UpdateScore(2, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                            break;
                        }
                    case 3:
                        {
                            MatchController.GetComponent<GameController>().UpdateScore(3, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                            break;
                        }
                    case 4:
                        {
                            MatchController.GetComponent<GameController>().UpdateScore(4, (1 * MatchController.GetComponent<GameController>().Score_Modifier));
                            break;
                        }
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
         Gizmos.DrawWireSphere(transform.position, radius); 
    }
}