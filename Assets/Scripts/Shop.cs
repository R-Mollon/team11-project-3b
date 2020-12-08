using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Canvas shopUI;


    // Start is called before the first frame update
    void Start()
    {
        shopUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (shopUI.enabled == false)
            {
                openShop();
            }
        }
    }

    void openShop()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        shopUI.enabled = true;
    }

    public void closeShop()
    {
        Time.timeScale = 1;
        shopUI.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void buyAutomatic()
    {
        Player.hasAutomatic = true;
    }
    public void buyShotgun()
    {
        Player.hasShotgun = true;
    }
    public void buySword()
    {
        Player.hasSword = true;
    }



}
