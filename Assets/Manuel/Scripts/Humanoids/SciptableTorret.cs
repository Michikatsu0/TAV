using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Turret1", menuName = "ScriptableObjects/Turret1", order = 3)]
public class SciptableTorret : TypesTurret
{
     public bool weaponEnemy;
    public enum WeaponSlots { Primary, Secundary }
       private bool chase=false;
       public float attackspeed=20f;
       public float attackTime=1f;
    private bool stop=false;
   
     public GameObject sliderPrefab;
       private GameObject sliderInstance;
    private GameObject canvas;
    public float chaseDistance=7;
    private Slider sliderComponent;
    
private CanvasGroup sliderCanvasGroup;
private SliderColor sliderColor;
public Vector3 offset= new Vector3(0,2f,0);
private bool start;
private bool startcon;

private bool back;
private bool backcon;
private bool starstop;
private bool backstop;
private bool trigger;

private bool changetrigger1=true;
private bool changetrigger2=true;
public GameObject bulletprefab;
    float rotationSpeed = 10.0f;
private GameObject player;
public class Bullet
    {
        public int bounce;
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    [SerializeField] public WeaponSettings weaponSettings;
    [SerializeField] private ParticleSystem[] muzzleEffects;
   
    private Transform raycastOrigin;

    public WeaponSlots weaponSlot;
    public string weaponName;
    private float accumulatedTime, fireInterval;
    private Ray ray;
    private RaycastHit hit;
    private Transform raycastDestination;
    private List<GameObject> bullets = new List<GameObject>();
    private AudioSource audioSource;
    private Rigidbody rgbd;
    private Collider[] boxColliders;
    private GameObject laser;
    private bool isDeath = false;

    private bool shoot=true;
    public override void Inicialize(GameObject obj)
    {
          weaponSettings.isFiring=false;
        player=GameObject.FindGameObjectWithTag("Player");
        raycastDestination=player.transform;
        canvas = GameObject.Find("Canvas");  

        if (sliderPrefab != null && canvas != null)
        {
           
             sliderInstance = Instantiate(sliderPrefab, canvas.transform);
            
           
            sliderInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);  
            
         
            sliderComponent = sliderInstance.GetComponent<Slider>();
                 sliderColor=sliderComponent.GetComponent<SliderColor>();

          
                  
       
            if (sliderComponent != null)
            {
                sliderComponent.value = 0.5f;  
            }
      
        }
        if (sliderInstance != null)
    {
      
        sliderCanvasGroup = sliderInstance.GetComponent<CanvasGroup>();
        if (sliderCanvasGroup == null)
        {
            sliderCanvasGroup = sliderInstance.AddComponent<CanvasGroup>();
        }
    }
    
    }
      void OnEnable()
{
     start=true;
  startcon=true;

 back=true;
 backcon=true;
  starstop=true;
  backstop=true;
chase=false;
    stop=false;
    trigger=false;
     
 changetrigger1=true;
  changetrigger2=true;
  shoot=true;

}
    public override void ExecuteBehavior(GameObject obj)
    {
         GameObject[] finds = GameObject.FindGameObjectsWithTag("bullet");
        
    
        foreach (GameObject find in finds)
        {
             Vector3 velocity = (player.transform.position - find.transform.position).normalized;
         find.transform.Translate(velocity * attackspeed * Time.deltaTime);
        
    }
         Vector3 worldPosition = obj.transform.position + offset;


if (sliderInstance != null)
{
 
    sliderInstance.transform.position = worldPosition;

 
    sliderInstance.GetComponent<RectTransform>().localScale = new Vector3(0.02f, 0.01f, 0.1f);  

   
    float distanceToCamera = Vector3.Distance(Camera.main.transform.position, worldPosition);

    
    if (distanceToCamera > 0)  
    {
        sliderCanvasGroup.alpha = 1f;  
    }
    else
    {
        sliderCanvasGroup.alpha = 0f;  
    }
}
if (sliderInstance != null)
{
  
 
      Vector3 directionToCameera= (Camera.main.transform.position - obj.transform.position).normalized;
          
 
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToCameera.x, 0, directionToCameera.z));
                 sliderInstance.transform.rotation = Quaternion.Slerp( sliderInstance.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);



}

  float distanceToPlayer =  Vector3.Distance(obj.transform.position, player.transform.position);
               
               if(distanceToPlayer> chaseDistance)
               {
                if(trigger==true)
                { sliderColor.triggerColorChange=true;
                back=true;
                    backstop=true;
                    backcon=true;
                    changetrigger1=true;
                if(sliderColor.end2)
                {
                    if(starstop==true)
                    {
                       sliderColor.getChangeColorbackStop();
                    stop=true;
                    starstop=false;
                    }
                }
                if(stop==false)
                {
                   
                    if(sliderColor.triggerColorChangeStop)
                    {
                  
                    Debug.Log("1");
                    if(start==true)
                    {
                    sliderColor.getChangeColorStar();
                    start=false;
                   }
                    }
                    else{
 shoot=false;
  Debug.Log("2");
                    }
                
    
               
              
    
    
}
else{
     if(sliderColor.triggerColorChangeStop==true)
     {
         Debug.Log("3");
                    {
    if(startcon)
    {
       sliderColor.getChangeColorStarcon();
       startcon=false;
    }
}
     }
     else{
         Debug.Log("4");
         shoot=false;
     }
                }
               }
               }
else 
    {
       
                    starstop=true;
                    start=true;
                    startcon=true;
                    trigger=true;
                    
     if(sliderColor.end)
                {
                    
                    if(backstop)
                    {
                    sliderColor.getChangeColorbackStop();
                      
                    chase=true;
                    backstop=false;
                    }
                }
                if(chase==false)
                {
                    if(sliderColor.triggerColorChange)
                   {
                    Debug.Log(sliderColor.triggerColorChange);
                    Debug.Log("5");
                    sliderColor.triggerColorChangeStop=true;
                    starstop=true;
                    start=true;
                    startcon=true;
                    if(back)
                    {
                         sliderColor.getChangeColorStopStart();
                   
                    back=false;
                    
                }
                }
                 else{
                  Debug.Log("6");
                   
       if(changetrigger1==true)
    {
        sliderColor.getChangeColorStop();
    sliderColor.getExitTime(attackTime);
    changetrigger1=false;
    }
    if(sliderColor.changeTimer==false)
{      Debug.Log("fe");
            FireBullet();
            sliderColor.changeTimer=true;
            changetrigger1=true;

       
}
               }
                }
                else{
                     
                  if(sliderColor.triggerColorChange)
                  {
                     Debug.Log("7");
if(backcon)
{
sliderColor.getChangeColorbackcon();
backcon=false;
}
                }
                   
                else{
                  
                    Debug.Log("8");
       if(changetrigger1==true)
    {
        sliderColor.getChangeColorStop();
    sliderColor.getExitTime(attackTime);
    changetrigger1=false;
    }
    if(sliderColor.changeTimer==false)
{      Debug.Log("fe");
            FireBullet();
            sliderColor.changeTimer=true;
            changetrigger1=true;

       
}
    
               }
    
    }
}
    }
public void OnDeathWeapon(bool isDeath)
    {
        this.isDeath = isDeath;
    }

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * weaponSettings.bulletDrop; 
        return bullet.initialPosition + bullet.initialVelocity * bullet.time + (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 position, GameObject obj)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.time = 0.0f;
       
    
        bullet.bounce = weaponSettings.maxNumberBounces;
        return bullet;
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        fireInterval = attackTime;
        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets(GameObject obj)
    {
        SimulateBullets(obj);
        DestroyBullets();
    }
    
    void SimulateBullets(GameObject obj)
    {
           
    }

    void DestroyBullets()
    {
        if(changetrigger2==true)
        {
 sliderColor.getExitTime2(100f);
 changetrigger2=false;
        }
       
        if(sliderColor.changeTimer2==false)
        {
 bullets.RemoveAll(obj => obj != null);
 sliderColor.changeTimer2=false;
 changetrigger2=true;
        }
       
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = (end - start).magnitude;
        
        ray.origin = start;
        ray.direction = direction;


        if (Physics.Raycast(ray, out hit, distance))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 0.1f);

            

            var ramdonHitClip = Random.Range(0, weaponSettings.weaponHitsAudioClips.Count);
            PlayAudioAtPosition.PlayClipAtPoint(weaponSettings.weaponHitsAudioClips[ramdonHitClip], hit.point, Random.Range(0.85f, 1f));

            bullet.time = weaponSettings.maxLifeTime;
            bullet.tracer.transform.position = hit.point;
            end = hit.point;

            if (bullet.bounce > 0)
            {
                bullet.time = 0;
                bullet.initialPosition = hit.point;
                bullet.initialVelocity = Vector3.Reflect(bullet.initialVelocity, hit.normal);
                bullet.bounce--;
            }

            var rgbd = hit.collider.GetComponent<Rigidbody>();

            if (rgbd && !rgbd.isKinematic)
            {
                if (hit.collider.gameObject.CompareTag("Probs"))
                {
                    rgbd.AddForceAtPosition(ray.direction * 5f, hit.point, ForceMode.Impulse);
                }
                else if (hit.collider.gameObject.CompareTag("Bridges"))
                {
                    rgbd.AddForceAtPosition(ray.direction * 2.5f, hit.point, ForceMode.Impulse);
                }
            }

            
            else
            {
                var playerHitBox = hit.collider.GetComponentInChildren<PlayerHitBoxResponse>();
                var rgbdPlayer = hit.collider.GetComponentInChildren<Rigidbody>();
                if (playerHitBox)
                {
                    playerHitBox.TakeHitBoxDamage(weaponSettings.damage);
                    if (playerHitBox.healthResponse.currentHealth <= 0.0f)
                        rgbdPlayer?.AddForceAtPosition(ray.direction * 1.5f, hit.point, ForceMode.Impulse);
                    
                }
            }
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
        
    }

    private void FireBullet()
    {
        foreach (var particleSystem in muzzleEffects)
            particleSystem.Emit(1);

GameObject shotOrigen=GameObject.Find("shot origen");
       
        GameObject obj= Instantiate(bulletprefab, shotOrigen.transform.position, Quaternion.identity);
        
         Vector3 velocity = (player.transform.position - obj.transform.position).normalized;
bullets.Add(obj);

        var ramdonShootClip = Random.Range(0, weaponSettings.weaponShootAudioClips.Count);
       
    }

    public void OnFiringWeapon(bool isFiring, WeaponResponse weapon)
    {
        weapon.weaponSettings.isFiring = isFiring;
    }

    private void OnDestroy()
    {
        PlayerActionsResponse.ActionShootWeaponTrigger -= OnFiringWeapon;
    }
}
