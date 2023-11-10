using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public GameObject GameManageGO;
    
    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject ExplosionGO;

   
    public Text LivesUIText;
    private const int MaxLives = 3;
    protected int lives;
    public float speed;
    protected GameObject scoreUITextGo;
    public float speedFire = 0.3f;
    public float fireRate = 1f;
    protected int oldScore = 0;
    public int upgradeSocre;
    // text done map
    public Text textComponent;
    public int Map;
    public int nextMap;
    public static int damage = 100;

    // In Android
    private Rigidbody rb;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        // transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
        InvokeRepeating("MakeFire",0f, fireRate * speedFire);
    }
        // Start is called before the first frame update
        void Start()
        {
           
            scoreUITextGo = GameObject.FindGameObjectWithTag("TextScoreTag");

        rb = GetComponent<Rigidbody>();
         }
    
        // Update is called once per frame
        void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
    
            Vector2 direction = new Vector2(x, y).normalized;
           // Move(direction);

            // chuyển map
            GameObject bossShip = GameObject.Find("Boss");
            int scoreNow = scoreUITextGo.GetComponent<GameScore>().Score;
            oldScore = scoreNow;

            if (oldScore > upgradeSocre && bossShip == null)
            {
                UpdateText("Xong Map " + "" + Map);

                CancelInvoke("MakeFire");

                Invoke("nextShip", 0.5f);

           
            StartCoroutine(LoadAfterDelay());
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = touch.deltaPosition;
                    Vector3 moveDirection = new Vector3(touchDeltaPosition.x, 0, touchDeltaPosition.y);
                    rb.AddForce(moveDirection * speed);
                }
            }
    
        }

            void Move(Vector2 direction)
        {
            Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
            
            max.x = max.x - 0.225f;
            min.x = min.x + 0.225f;
            max.y = max.y - 0.285f;
            min.y = min.y + 0.285f;
    
            Vector2 pos = transform.position;        
            
            pos += direction * speed * Time.deltaTime;
            pos.x = Mathf.Clamp (pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            
            transform.position = pos;
    
        }

        void MakeFire()
        {
            GetComponent<AudioSource>().Play();
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;
                
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;
        }

         void OnTriggerEnter2D(Collider2D col)
        {
            if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "HitBoss") || (col.tag == "AsteroidTag"))
            {

              //  lives--;
                lives = lives < 0 ? 0 : lives;
                LivesUIText.text = lives.ToString();

          
                if (lives == 0)
                {
                    PlayExplosion();
                    GameManageGO.GetComponent<GameManage>().SetGameManageState(GameManage.GameManageState.GameOver);
                    gameObject.SetActive(false);
                    CancelInvoke("MakeFire");
                    speedFire = 0.3f;
                }
            // Destroy(gameObject);
             }
             if ((col.tag == "Resurrection"))
             {
                lives++;
                LivesUIText.text = lives.ToString();
                }

                if ((col.tag == "Kamikaze"))
                { 
                     StartCoroutine(IncreaseFireRateForDuration(6f));

                }
                if ((col.tag == "Cannon"))
                {
                    StartCoroutine(IncreaseDamageRateForDuration(6f));

                }
                // Lưu điểm, mạng
                PlayerPrefs.SetInt("lives", lives);
                PlayerPrefs.SetInt("Score", oldScore);
           }
        // Kamikze
        IEnumerator IncreaseFireRateForDuration(float duration)
        {
            // Kích hoạt tăng tốc độ ra đạn
            InvokeRepeating("MakeFire", 0f, 0.1f);

            // Đợi trong khoảng thời gian đã cho
            yield return new WaitForSeconds(duration);
            CancelInvoke("MakeFire");
            InvokeRepeating("MakeFire", 0f, 0.3f);
         }

    //Cannon
    IEnumerator IncreaseDamageRateForDuration(float duration)
    {
        int damageNew = 300;
        damage = damageNew;
        // Đợi trong khoảng thời gian đã cho
        yield return new WaitForSeconds(duration);
        damage = 100;
    }

    void PlayExplosion()
         {
             GameObject explosion = (GameObject)Instantiate(ExplosionGO);
             explosion.transform.position = transform.position;
         }

    public void nextShip()
    {

        GameObject playerShip = GameObject.Find("PlayerGO");
        Vector2 postition = playerShip.transform.position;
        postition = new Vector2(postition.x, postition.y + 3f * Time.deltaTime);
        playerShip.transform.position = postition;
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));


        if (playerShip.transform.position.y > max.y)
        {
            playerShip.gameObject.SetActive(false);

            Destroy(playerShip);

        }
    }

    IEnumerator LoadAfterDelay()
    {

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Map" + "" + nextMap);
    }
    public void UpdateText(string newText)
    {
        textComponent.text = newText;
    }
}

//
// - win game 
//     - time
//     - move animation