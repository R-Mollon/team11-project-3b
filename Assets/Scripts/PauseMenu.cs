using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseUI;
    public Shop shopManager;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.enabled = false;
        shopManager = GameObject.Find("ShopManager").GetComponent<Shop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shopManager.atShop)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseUI.enabled == false)
                {
                    openMenu();
                }
                else
                {
                    closeMenu();
                }
            }
        }
    }

    void openMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Player.paused = true;
        pauseUI.enabled = true;
    }

    public void closeMenu()
    {
        Time.timeScale = 1;
        pauseUI.enabled = false;
        Player.paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void returnToMainMenu()
    {
        closeMenu();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
