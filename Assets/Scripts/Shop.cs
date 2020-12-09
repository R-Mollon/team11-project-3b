using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Camera cam;
    public Canvas shopUI;
    public bool atShop;
    public Rigidbody player;
    public Player playerData;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerData = GameObject.Find("Player").GetComponent<Player>();
        shopUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkShop();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (atShop)
            {
                closeShop();
            }
        }
    }

    void openShop()
    {
        atShop = true;
        player.velocity = new Vector3(0, 0, 0);
        Player.paused = true;
        Cursor.lockState = CursorLockMode.Confined;
        shopUI.enabled = true;
    }

    public void closeShop()
    {
        atShop = false;
        Player.paused = false;
        shopUI.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void buyAutomaticAmmo()
    {
        if(playerData.automaticBullets != 90)
        {
            if(playerData.credits >= 6)
            {
                playerData.credits -= 6;
                playerData.automaticBullets = 90;
            }
        }
    }
    public void buyShotgunAmmo()
    {
        if(playerData.shotgunShells != 20)
        {
            if (playerData.credits >= 7)
            {
                playerData.credits -= 7;
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
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5f))
        {
            if (hit.collider.tag == "Shop")
            {
                if (Input.GetKeyDown(KeyCode.E) && !playerData.reloading)
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
