using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFunction : MonoBehaviour
{
    public GameObject shoot_item;
    public GameObject pick_position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Oculus_CrossPlatform_PrimaryThumbstick")) {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject item = Instantiate(shoot_item, pick_position.transform.position, Quaternion.identity) as GameObject;
        item.GetComponent<Rigidbody>().AddForce(pick_position.transform.forward * 500);
    }
}
