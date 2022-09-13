using System;
using UnityEngine;
using UnityEngine.UI;

public class DomeController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private GameObject dome;
    [SerializeField]
    private GameObject pivot;
    [SerializeField]
    [Range(0f, 100f)]
    private int rotationSpeed;

    [Header("Dome Rotation")]
    [SerializeField]
    [Range(-90f, 270f)]
    private float domeMinimumAngle;
    [SerializeField]
    [Range(-90f, 270f)]
    private float domeMaximumAngle;

    private float domeDefaultAngle;
    //public float sliderValue;
    //public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        domeDefaultAngle = 90f;

        //slider.minValue = midpartMinimumAngle;
        //slider.maxValue = midpartMaximumAngle;

        //slider.onValueChanged.AddListener(rotateUpdate);
    }

    //private void rotateUpdate(float value)
    //{
       // dome.transform.localEulerAngles = new Vector3(dome.transform.localEulerAngles.x, -value, dome.transform.localEulerAngles.z);
    //}

    // Update is called once per frame
    void Update()
    {
        Move();
        //print(midpartConnection.transform.localEulerAngles);
    }

    private void Move()
    {

        // Button A/B for rotating middle part
        if (Input.GetButton("Telescope Mid Rotate Pos") && domeDefaultAngle <= domeMaximumAngle)
        {
            dome.transform.RotateAround(pivot.GetComponent<Renderer>().bounds.center, Vector3.down, Time.deltaTime * rotationSpeed);
            domeDefaultAngle += Time.deltaTime * rotationSpeed;
        }
        else if (Input.GetButton("Telescope Mid Rotate Neg") && domeDefaultAngle >= domeMinimumAngle)
        {
            dome.transform.RotateAround(pivot.GetComponent<Renderer>().bounds.center, Vector3.up, Time.deltaTime * rotationSpeed);
            domeDefaultAngle -= Time.deltaTime * rotationSpeed;
        }
    }

    public void MidpartUIMove()
    {

    }
}
