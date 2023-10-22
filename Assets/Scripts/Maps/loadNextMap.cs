using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class loadNextMap : MonoBehaviour
{
    public float delaySecond = 1f;
    public Text textComponent;
    private float speed = 4f;
    
    public void nextMap()
    {
        GameObject bossShip = GameObject.Find("Boss");
        GameObject playerShip = GameObject.Find("PlayerGO");
        //if(bossShip == null)
        //{
           // UpdateText("Hoàn thành Map 1");

            Vector2 postition = playerShip.transform.position;
            postition = new Vector2(postition.x, postition.y + speed * Time.deltaTime);
            playerShip.transform.position = postition;
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            if (playerShip.transform.position.y > max.y)
            {
                Destroy(playerShip);
            }

            //playerShip.SetActive(false);

            ModeSelect();
        //}
    }
    public void UpdateText(string newText)
    {
        textComponent.text = newText;
    }


     void ModeSelect()
    {
        StartCoroutine(LoadAfterDelay());
    }

     IEnumerator LoadAfterDelay() { 

        yield return new WaitForSeconds(delaySecond);

        SceneManager.LoadScene(2);
    }
}
