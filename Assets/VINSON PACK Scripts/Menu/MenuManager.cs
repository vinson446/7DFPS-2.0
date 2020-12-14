using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject[] menuPanels;

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

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
