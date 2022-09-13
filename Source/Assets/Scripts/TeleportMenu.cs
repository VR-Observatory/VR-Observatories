using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMenu : MonoBehaviour
{
    public GameObject canvas;
    public GameObject teleportMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTeleportMenu()
    {
        canvas.SetActive(false);
        teleportMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        canvas.SetActive(true);
        teleportMenu.SetActive(false);
    }
}
