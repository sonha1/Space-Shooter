using UnityEngine;
using UnityEngine.SceneManagement;

public class Choi : MonoBehaviour
{
    public void ChoiMoi()
    {
        SceneManager.LoadScene(1);
    }
    public void ThoatMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void HuongDan()
    {
        SceneManager.LoadScene(7);
    }

    public void CaiDat()
    {
        SceneManager.LoadScene("Setting");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
