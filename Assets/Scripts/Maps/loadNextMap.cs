using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class loadNextMap : MonoBehaviour
{
    public float delaySecond = 5f;
    public Text textComponent;
  



    public void nextShip()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");
        Vector2 postition = playerShip.transform.position;
        postition = new Vector2(postition.x, postition.y + 13f * Time.deltaTime);
        playerShip.transform.position = postition;
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (playerShip.transform.position.y > max.y)
        {
            Destroy(playerShip);
            ModeSelect();
        }
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
