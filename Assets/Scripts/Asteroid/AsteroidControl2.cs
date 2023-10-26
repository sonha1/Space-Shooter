using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl2 : MonoBehaviour
{
    public float speed;
    public GameObject ExplosionAsteroid;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        this.Rotating();
    }
    protected void Rotating()
    {
        Vector3 eulers = new Vector3(0, 0, 1);
        transform.parent.Rotate(eulers * this.speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionAsteroid);
        explosion.transform.position = transform.position;
    }
}
