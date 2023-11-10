using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AsteroidControl : MonoBehaviour
{

    
    public float speedAsteroid;
    public GameObject ExplosionAsteroid;

    public int health;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 postition = transform.position;
        postition = new Vector2(postition.x, postition.y - speedAsteroid * Time.deltaTime);
        transform.position = postition;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            health -= PlayerControl.damage;

            if (health == 0)
            {
                PlayExplosion();

                Destroy(gameObject);
            }
           
          
           
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionAsteroid);
        explosion.transform.position = transform.position;
    }
}
