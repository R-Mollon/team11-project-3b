using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseUI;


    // Start is called before the first frame update
    void Start()
    {
        pauseUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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

    void openMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        pauseUI.enabled = true;
    }

    public void closeMenu()
    {
        Time.timeScale = 1;
        pauseUI.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
