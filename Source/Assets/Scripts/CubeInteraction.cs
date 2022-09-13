using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    public GameObject text;
    public GameObject text_2;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        text_2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            text.GetComponent<TextMesh>().text = "Use Hand \nTrigger to \nPick Me Up";
        }

        if (other.gameObject.tag.Equals("Text"))
        {
            text.GetComponent<TextMesh>().text = "Use Right \nJoystick to \nLook Around";
            animator.SetBool("Move_Light",true);
            text_2.SetActive(true);
        }
    }
}
