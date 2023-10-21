using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNextMap : MonoBehaviour
{
    public float delaySecond = 1.5f;
   // public string nameScene = "Map2";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShipTag")
        {
            collision.gameObject.SetActive(false);

            ModeSelect();
        }
    }

     void ModeSelect()
    {
        StartCoroutine(LoadAfterDelay());
    }

     IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delaySecond);

        SceneManager.LoadScene(2);
    }
}
