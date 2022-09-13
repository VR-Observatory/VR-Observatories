using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class resize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject child = transform.Find("Button").gameObject;
        //note: enable this part when VR side is finished
        /**
        if (OVRManager.isHmdPresent)
        {
            child.GetComponent<Button>().onClick.AddListener(buttonclick);

        }**/
        //note: disable this line when VR side is finished
        child.GetComponent<Button>().onClick.AddListener(buttonclick);


        transform.localPosition = new Vector3((float)0.51, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localEulerAngles = new Vector3(0, 90, 0);
        GetComponent<button_control>().disableButton();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buttonclick()
    {
        GameObject Father = transform.parent.gameObject;
        //Debug.Log(Father.name);
        Father.GetComponent<click_teleport>().OnClick();

    }
}
