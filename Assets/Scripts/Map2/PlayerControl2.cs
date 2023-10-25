using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl2 : PlayerControl
{

    //public GameObject GameManageGO;

    //public GameObject PlayerBulletGO;
    //public GameObject bulletPosition01;
    //public GameObject bulletPosition02;
    //public GameObject ExplosionGO;

    //public Text LivesUIText;
    protected int lives2;
    protected int score2;
  

    // text done map
    //public Text textComponent;
    public void Init()
    {
        lives2 = PlayerPrefs.GetInt("lives");
        score2 = PlayerPrefs.GetInt("Score");
        LivesUIText.text = lives2.ToString();
        // transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
        InvokeRepeating("MakeFire", 0f, fireRate * speedFire);
    }
    // Start is called before the first frame update
    void Start()
    {

        scoreUITextGo = GameObject.FindGameObjectWithTag("TextScoreTag");
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);

        GameObject bossShip = GameObject.Find("Boss2");

        int scoreNow2 = scoreUITextGo.GetComponent<GameScore>().Score;
        score2 =+scoreNow2;

        if (score2 > 2500 && bossShip == null)
        {
            UpdateText2("Xong Map 2");

            CancelInvoke("MakeFire");

            Invoke("nextShip", 0.5f);
            StartCoroutine(LoadAfterDelay2());
        }
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;
        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;

    }

    void MakeFire()
    {
        GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
        bullet01.transform.position = bulletPosition01.transform.position;

        GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
        bullet02.transform.position = bulletPosition02.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "HitBoss") || (col.tag == "AsteroidTag"))
        {

            lives2--;
            lives2 = lives2 < 0 ? 0 : lives2;
            LivesUIText.text = lives2.ToString();
            if (lives2 == 0)
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
            lives2++;
            LivesUIText.text = lives2.ToString();
        }

        if ((col.tag == "Kamikaze"))
        {
            StartCoroutine(IncreaseFireRateForDuration(5f));
        }


    }
    // Kamikze
    IEnumerator IncreaseFireRateForDuration(float duration)
    {
        float fireRateNew = fireRate * 15f;
        float speedFireNew = speedFire * 15f;
        // Kích hoạt tăng tốc độ ra đạn
        InvokeRepeating("MakeFire", 0f, fireRateNew * speedFireNew);

        // Đợi trong khoảng thời gian đã cho
        yield return new WaitForSeconds(duration);

        InvokeRepeating("MakeFire", 0f, fireRate * speedFire);

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

    IEnumerator LoadAfterDelay2()
    {

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Map3");
    }
    public void UpdateText2(string newText)
    {
        textComponent.text = newText;
    }
}

//
// - win game 
//     - time
//     - move animation