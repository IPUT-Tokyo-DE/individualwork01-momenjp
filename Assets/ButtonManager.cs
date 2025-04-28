using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // ゲーム本編のシーン名
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
