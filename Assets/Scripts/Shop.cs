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
    public Player playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<Player>();
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

    public void buyAutomaticAmmo()
    {
        if(playerData.automaticBullets != 90)
        {
            if(playerData.credits >= 5)
            {
                playerData.credits -= 5;
                playerData.automaticBullets = 90;
            }
        }
    }
    public void buyShotgunAmmo()
    {
        if(playerData.shotgunShells != 20)
        {
            if (playerData.credits >= 5)
            {
                playerData.credits -= 5;
                playerData.shotgunShells = 20;
            }
        }
    }
    public void buyHandgunAmmo()
    {
        if (playerData.handgunBullets != 80)
        {
            if (playerData.credits >= 5)
            {
                playerData.credits -= 5;
                playerData.handgunBullets = 80;
            }
        }
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
