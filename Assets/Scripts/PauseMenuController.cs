using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    private bool isPaused = false; 
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("mainMenu"); 
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit(); 
        #endif
    }
}
