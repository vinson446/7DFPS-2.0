using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject[] menuPanels;

    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMenuPanel(int index)
    {
        ResetMenuPanels();
        menuPanels[index].SetActive(true);
    }

    void ResetMenuPanels()
    {
        foreach (GameObject obj in menuPanels)
        {
            obj.SetActive(false);
        }
    }
}
