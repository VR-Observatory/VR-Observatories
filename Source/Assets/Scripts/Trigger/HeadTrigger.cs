using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject telescopeOVRCamera;
    [SerializeField]
    private GameObject playerPCObject;
    [SerializeField]
    private GameObject playerVRObject;
    [SerializeField]
    private OVRPlayerController playerVRController;
    [SerializeField]
    private GameObject vrGlasses;
    [SerializeField]
    private GameObject vrGlassesPosition;

    [SerializeField]
    private bool isUseTelescope;



    // Start is called before the first frame update
    void Start()
    {
        telescopeOVRCamera.SetActive(false);
        isUseTelescope = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Vertical") <= -0.5f && isUseTelescope == true)
            ExitTelescope();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("VRGlasses"))
        {
            playerVRObject.SetActive(false);
            telescopeOVRCamera.SetActive(true);
            isUseTelescope = true;
        }
    }

    private void ExitTelescope() {
        
        playerVRObject.SetActive(true);
        telescopeOVRCamera.SetActive(false);
        isUseTelescope = false;
        vrGlasses.transform.position = vrGlassesPosition.transform.position;
        vrGlasses.transform.rotation = vrGlassesPosition.transform.rotation;
    }


}
