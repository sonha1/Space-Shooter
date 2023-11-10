using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public GameObject scoreUITextGo;
    public GameObject liveUITextGo;
    public static int scoresss;
    public float minX = -5f;  // Giới hạn bên trái
    public float maxX = 5f;  // Giới hạn bên phải
    public float minY = -2f;  // Giới hạn dưới
    public float maxY = 2f;  // Giới hạn trên
    public float moveSpeed = 5f;  // Tốc độ di chuyển của boss

    private Vector2 targetPosition;  // Vị trí đích di chuyển của boss

  public GameObject ExplosionGO;

    public GameObject BossBulletGO;

    public Transform firePoint;  // Vị trí xuất phát của đạn
    public Transform firePoint2;
    public Transform firePoint3;

    public float fireRate = 0.8f;  // Tốc độ bắn đạn (số lần bắn đạn mỗi giây)
    private float fireTimer = 0.2f;  // Đếm thời gian giữa các lần bắn đạn
    public int bulletCount = 10;  // Số lượng đạn trong chùm
    public float spreadAngle = 50f;  // Góc phân tán của chùm đạn
    public int health = 12000;
    public int lowHealthThreshold = 3000;
    //public int damage = 100;
    public int score = 1000;
    

    public bool isFlashing = false;    // The boss's effect when it is running out of health.
    public Sprite normalSprite; // Sprite bình thường
    public Sprite flashSprite; // Sprite nhấp nháy


    private void Start()
    {
        scoreUITextGo = GameObject.FindGameObjectWithTag("TextScoreTag");
        liveUITextGo = GameObject.FindGameObjectWithTag("LiveTextTag");
    }

    private void Update()
    {
        // Di chuyển boss đến vị trí đích
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Kiểm tra nếu boss đã đến vị trí đích, chọn một vị trí ngẫu nhiên mới
        if ((Vector2)transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }


        // Tính toán thời gian giữa các lần bắn đạn
        fireTimer += Time.deltaTime;

        // Kiểm tra nếu đến lượt bắn đạn
        if (fireTimer >= fireRate)
        {
            // Bắn đạn
            // FireBossBullet();
            ShootBurst();
            // Đặt lại đếm thời gian
            fireTimer = 0f;
        }

    }

    private void SetRandomTargetPosition()
    {
        // Chọn một vị trí ngẫu nhiên trong khoảng xác định
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY / 10, maxY);

        // Gán vị trí đích di chuyển mới cho boss
        targetPosition = new Vector2(randomX, randomY);
    }



    // Bắn đạn chùm
    private void ShootBurst()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");
        if (playerShip != null)
        {
            // Tính toán góc giữa các viên đạn trong chùm
            float angleStep = spreadAngle / (bulletCount);
            float startAngle = -spreadAngle / 2f;

            // Bắn các viên đạn trong chùm
            for (int i = 0; i < bulletCount; i++)
            {
                // Tính toán góc của đạn hiện tại
                float bulletAngle = startAngle + (angleStep * i);
               
                // Tạo đạn từ prefab và thiết lập hướng di chuyển
                GameObject bullet = Instantiate(BossBulletGO, firePoint.position, Quaternion.identity);
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, bulletAngle);

                GameObject bullet2 = Instantiate(BossBulletGO, firePoint2.position, Quaternion.identity);
                bullet2.transform.rotation = Quaternion.Euler(0, 0f, bulletAngle);

                GameObject bullet3 = Instantiate(BossBulletGO, firePoint3.position, Quaternion.identity);
                bullet3.transform.rotation = Quaternion.Euler(0f, 0f, bulletAngle);

                Vector2 direction = playerShip.transform.position - bullet.transform.position;
                bullet.GetComponent<EnemyBullet>().SetDirection(direction);

                Vector2 direction2 = playerShip.transform.position - bullet2.transform.position;
                bullet2.GetComponent<EnemyBullet>().SetDirection(direction2);

                Vector2 direction3 = playerShip.transform.position - bullet3.transform.position;
                bullet3.GetComponent<EnemyBullet>().SetDirection(direction3);
            }
        }
    }

    // Bắn đạn đơn
    void FireBossBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");
        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate((BossBulletGO));

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }



    // The effect of the boss when it is running out of health.
    IEnumerator FlashBoss()
    {
        isFlashing = true;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float flashDuration = 0.5f; // Thời gian mỗi lần chuyển đổi sprite


        while(health > 0)
        {
            spriteRenderer.sprite = flashSprite;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.sprite = normalSprite;
            yield return new WaitForSeconds(flashDuration);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
      
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            health -= PlayerControl.damage;

            if (health <= lowHealthThreshold)
            {
                StartCoroutine(FlashBoss());
            }

            if (health == 0)
            {
                PlayExplosion();
                scoreUITextGo.GetComponent<GameScore>().Score += score;
                scoresss = scoreUITextGo.GetComponent<GameScore>().Score;
                // livessss = liveUITextGo.GetComponent<G>().lives;
                Destroy(gameObject);
            }
        }
        
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

}