using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    //private Animator anim;
    private int currentLane = 1;
    private bool jumping = false;
    private bool sliding = false;
    private float jumpStart;
    private float slideStart;
    private BoxCollider boxCollider;
    private Vector3 boxColliderSize;
    private bool isSwipping = false;
    private Vector2 startingTouch;
    private bool canmove = true;
    private bool isDead = false;
    private float speedOnPause;
    private float count;

    public GameObject model;
    //public float invincibleTime;
    public float speed;
    public float Lanespeed;
    public float jumpLength;
    public float jumpHeight;
    public float slideLength;

    public AudioClip coinSFX;
    public AudioClip trashSFX;
    public AudioClip hitSFX;

    public GameObject coinFX;
    public GameObject trashFX;

    public float minSpeed;
    public float maxSpeed;
    Animator anim;
    public Camera1 camera1;


    private Vector3 VerticalTargetPosition;


    public UImanager uimanager;
    public ScnceManger scncemanger;

    [HideInInspector]
    public int coin;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        anim = GetComponent<Animator>(); 
        // anim.Play("RunStart");
        minSpeed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        if (!canmove)
            return;


        uimanager.Updatecoin(coin);
        uimanager.coinend(coin);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
            anim.SetTrigger("TurnRight");
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
            anim.SetTrigger("TurnLeft");

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
            anim.SetTrigger("Jump");

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }
    

        if(Input.touchCount == 1)
        {
            if (isSwipping)
            {
                Vector2 diff = Input.GetTouch(0).position - startingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
                if(diff.magnitude > 0.01f)
                {
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                        {
                            Slide();
                            jumpHeight = 0;
                            Invoke("Jumpanddown", 0.5f);
                        }
                        else
                        {
                            Jump();
                            anim.SetTrigger("Jump");
                        }
                    }
                    else
                    {
                        if(diff.x < 0)
                        {
                            ChangeLane(-1);
                            anim.SetTrigger("TurnRight");
                        }
                        else
                        {
                            ChangeLane(1);
                            anim.SetTrigger("TurnLeft");
                        }
                    }

                    isSwipping = false;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startingTouch = Input.GetTouch(0).position;
                isSwipping = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isSwipping = false;
            }
        }




        if (jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLength;            
            if (ratio >= 1f)
            {
                jumping = false;
            }
            else
            {
                VerticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {                
                jumpHeight = 0;
                Invoke("Jumpanddown", 0.5f);
            }

        }
        else
        {
            
            VerticalTargetPosition.y = Mathf.MoveTowards(VerticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        if (sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLength;
            if(ratio >= 1f)
            {
                sliding = false;
                boxCollider.size = boxColliderSize;
            }
        }
        

        

        Vector3 targetPosition = new Vector3(VerticalTargetPosition.x, VerticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Lanespeed * Time.deltaTime);
      
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
    }
    void Jumpanddown()
    {
        jumpHeight = 2.3f;
    }

    void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < -1 || targetLane > 3)
            return;
        if (targetLane == -1)
        {
            camera1.Shake();
            targetLane += 1;
            StartCoroutine(CountHit());
           
        }
        if (targetLane == 3)
        {
            camera1.Shake();
            targetLane -= 1;
            StartCoroutine(CountHit());
        }
            
        currentLane = targetLane;
        VerticalTargetPosition = new Vector3((currentLane - 1), 0, 0);        
    }
    void Jump()
    {
        if (!jumping)
        {
            jumpStart = transform.position.z;           
            jumping = true;
        }
    }
    void Slide()
    {
        if(!jumping && !sliding)
        {
            slideStart = transform.position.z;;
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 2;
            boxCollider.size = newSize;
            sliding = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Coins"))
        {
            coin++;
            coinFx1();
            AudioSource.PlayClipAtPoint(coinSFX, transform.position);
            other.gameObject.SetActive(false);
        }

        if (isDead)
            return;
        if (other.CompareTag("ob"))
        {
            AudioSource.PlayClipAtPoint(hitSFX, transform.position);
            camera1.Shake();
            canmove = false;
            uimanager.die = true; // stop score
            speed = 0;
            scncemanger.OnDie(); // open panel die
        }
        if (other.CompareTag("Trash"))
        {
            other.gameObject.SetActive(false);
            camera1.Shake();
            trashFX1();
            AudioSource.PlayClipAtPoint(trashSFX, transform.position);
            TrashHP trashp = this.GetComponent<TrashHP>();
            trashp.Trash++;
            if (trashp.Trash == 3)
            {
                canmove = false;
                uimanager.die = true;
                speed = 0;
                scncemanger.OnDie();
            }

        }

    }
    public void CanMove()
    {
        canmove = true;
    }
    public void Onpause()
    {
        speedOnPause = speed;
        canmove = false;
        uimanager.die = true; // หยุดแต้ม
        speed = 0;
    }
    public IEnumerator Unpause1()
    {
        uimanager.unpause = true;
        yield return new WaitForSeconds(2.25f);
        speed = speedOnPause;
        canmove = true;
        uimanager.die = false;
    }
    public IEnumerator CountHit()
    {
        count++;
        if (count >= 2)
        {
            canmove = false;
            uimanager.die = true; // stop score
            speed = 0;
            scncemanger.OnDie(); // open panel die

        }
        yield return new WaitForSeconds(3f);
        count = 0;
    }

    public void coinFx1()
    {
        GameObject FX = (GameObject)Instantiate(coinFX, transform.position, Quaternion.identity);
        Destroy(FX, 1f);
    }
    public void trashFX1()
    {
        GameObject FX = (GameObject)Instantiate(trashFX, transform.position, Quaternion.identity);
        Destroy(FX, 1f);
    }
    public void IncreaseSpeed()
    {
        speed *= 1.15f;
        jumpLength += 1;
        if (speed >= maxSpeed)
            speed = maxSpeed;
    }









































    /*public IEnumerator Blinking(float time)
    {
        isDead = true;
        float timer = 0;
        float currentBlink = 1f;
        float lastBlink = 0;
        float blinkPeriod = 0.1f;
        bool enabled = false;
        yield return new WaitForSeconds(0.5f);
        speed = minSpeed;

        while (timer < time && isDead)
        {
            model.SetActive(enabled);
            yield return null;
            timer += Time.deltaTime;
            lastBlink += Time.deltaTime;
            if (blinkPeriod < lastBlink)
            {
                lastBlink = 0;
                currentBlink = 1f - currentBlink;
                enabled = !enabled;
            }
        }
        model.SetActive(true);;
        isDead = false;
    }
    public void power()
    {
        StartCoroutine(Blinking(invincibleTime));
        Invoke("CanMove", 0.75f);
        scncemanger.OnDie2();
    }*/
}
