using UnityEngine;

public class Gun : MonoBehaviour
{

    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f; 
    public float bigDamage = 2f;
    public float smallDamage = 1f;


    public float fireRate = 1f;
    private float nextTimeToFire;

    public int maxAmmo;
    private int ammo;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;

    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&& Time.time >nextTimeToFire && ammo > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        Collider[] enemyColliders;
       enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);
           
       foreach (var enemyCollider in enemyColliders)
       {
        enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
       }
        //audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        //damage enemy
       foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            if (enemy == null) continue;

            Vector3 localEnemyPos = transform.InverseTransformPoint(enemy.transform.position);

           
            float halfWidth = gunTrigger.size.x * 0.5f;
            float halfHeight = gunTrigger.size.y * 0.5f;

            if (Mathf.Abs(localEnemyPos.x) <= halfWidth && Mathf.Abs(localEnemyPos.y) <= halfHeight)
            {
                var dir = enemy.transform.position - transform.position;
                RaycastHit hit;

                if (Physics.Raycast(transform.position, dir, out hit, range, raycastLayerMask))
                {
                    if (hit.transform == enemy.transform)
                    {
                        float dist = Vector3.Distance(enemy.transform.position, transform.position);
                        float finalDamage;

                        
                        if (dist > range * 0.5f) {
                            finalDamage = smallDamage;
                        } else {
                            finalDamage = bigDamage;
                        }

                        
                        if (localEnemyPos.y > 1.0f) 
                        {
                            finalDamage *= 0.5f; 
                        }
                        // -------------------------------------

                        enemy.TakeDamage(finalDamage);
                    }
                }
            }
        }
        //reset time
        nextTimeToFire = Time.time + fireRate;

        ammo--;

    }

    public void GiveAmmo(int amount, GameObject pickup)
    {
        if(ammo <maxAmmo)
        {
            ammo += amount;
            Destroy(pickup);
        }
        {
            if(ammo > maxAmmo)
            {
                ammo = maxAmmo;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //add potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

     private void OnTriggerExit(Collider other)
    {
        //remove enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
