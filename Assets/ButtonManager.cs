using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // �Q�[���{�҂̃V�[����
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
