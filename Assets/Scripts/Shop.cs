using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Camera cam;
    public Canvas shopUI;
    public bool atShop;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        shopUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkShop();
    }

    void openShop()
    {
        Player.paused = true;
        Cursor.lockState = CursorLockMode.Confined;
        shopUI.enabled = true;
    }

    public void closeShop()
    {
        Player.paused = false;
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

    void checkShop()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2f))
        {
            if (hit.collider.tag == "Shop")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (shopUI.enabled == false)
                    {
                        openShop();
                    }
                }
            }
        }
    }


}
