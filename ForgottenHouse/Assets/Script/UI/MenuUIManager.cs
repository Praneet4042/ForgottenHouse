using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;

    public TypewriterText panel1Text;
    public TypewriterText panel2Text;
    public TypewriterText panel3Text;
    public TypewriterText panel4Text;

    public void OpenInstructions()
    {
        mainMenuPanel.SetActive(false);
        panel1.SetActive(true);
    }

    public void Next1()
    {
        panel1Text.CompleteText();
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    public void Next2()
    {
        panel2Text.CompleteText();

        panel2.SetActive(false);
        panel3.SetActive(true);
    }

    public void Next3()
    {
        panel3Text.CompleteText();
        panel3.SetActive(false);
        panel4.SetActive(true);
    }

    public void Back1()
    {
        panel2Text.CompleteText();

        panel2.SetActive(false);
        panel1.SetActive(true);
    }

    public void Back2()
    {
        panel3Text.CompleteText();

        panel3.SetActive(false);
        panel2.SetActive(true);
    }

    public void Back3()
    {
        panel4Text.CompleteText();
        panel4.SetActive(false);
        panel3.SetActive(true);
    }

   
    public void ReturnToMainMenu()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);

        mainMenuPanel.SetActive(true);
    }
    public void StartGame()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}