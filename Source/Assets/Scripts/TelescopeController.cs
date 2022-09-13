using System;
using UnityEngine;
using UnityEngine.UI;

public class TelescopeController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private GameObject midpartConnection;
    [SerializeField]
    private GameObject toppartConnection;
    [SerializeField]
    [Range(0f, 100f)]
    private int rotationSpeed;

  

    [Header("Midpart Rotation")]
    [SerializeField]
    [Range(-90f, 270f)]
    private float midpartMinimumAngle;
    [SerializeField]
    [Range(-90f, 270f)]
    private float midpartMaximumAngle;

    [Header("Toppart Rotation")]
    [SerializeField]
    [Range(-90f, 270f)]
    private float toppartMinimumAngle;
    [SerializeField]
    [Range(-90f, 270f)]
    private float toppartMaximumAngle;

    [Header("Start Angles")]
    [SerializeField]
    private float midpartStartAngle = 90f;
    [SerializeField]
    private float toppartStartAngle = 90f;

    private float midpartDefaultAngle, toppartDefaultAngle;
    //public float sliderValue;
    //public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        midpartDefaultAngle = midpartStartAngle;
        toppartDefaultAngle = toppartStartAngle;

        //slider.minValue = midpartMinimumAngle;
        //slider.maxValue = midpartMaximumAngle;

        //slider.onValueChanged.AddListener(rotateUpdate);
    }

    private void rotateUpdate(float value)
    {
        midpartConnection.transform.localEulerAngles = new Vector3(midpartConnection.transform.localEulerAngles.x,-value,midpartConnection.transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
         
            Move();
        
        //print(midpartConnection.transform.localEulerAngles);
    }

    private void Move() {
        
            // Button A/B for rotating middle part
            if (Input.GetButton("Telescope Mid Rotate Pos") && midpartDefaultAngle <= midpartMaximumAngle)
            {
                // Debug.Log(IsPlayerDetected(player,playerDetection));
                midpartConnection.transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
                midpartDefaultAngle += Time.deltaTime * rotationSpeed;
            }
            else if (Input.GetButton("Telescope Mid Rotate Neg") && midpartDefaultAngle >= midpartMinimumAngle) {
                midpartConnection.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
                midpartDefaultAngle -= Time.deltaTime * rotationSpeed;
            }

            // Button X/Y for rotating top part
            if (Input.GetButton("Telescope Top Rotate Pos") && toppartDefaultAngle <= toppartMaximumAngle)
            {
                toppartConnection.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                toppartDefaultAngle += Time.deltaTime * rotationSpeed;
            }
            else if (Input.GetButton("Telescope Top Rotate Neg") && toppartDefaultAngle >= toppartMinimumAngle)
            {
                toppartConnection.transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
                toppartDefaultAngle -= Time.deltaTime * rotationSpeed;
            }
        
       
    }

    public void MidpartUIMove() {

    }

   
}
