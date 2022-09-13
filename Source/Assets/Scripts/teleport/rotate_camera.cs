using UnityEngine;
using System.Collections;

public class rotate_camera : MonoBehaviour
{
    public GameObject target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 tar = target.transform.position;
        tar.y = transform.position.y;
        transform.LookAt(tar);
    }
}