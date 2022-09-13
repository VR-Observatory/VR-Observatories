using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTest : MonoBehaviour
{

    //public Animator animator;
    [Header("Elevator Current Position")]
    public int currentFloor = 1;

    [Header("Elevator Positions")]
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;

    [Header("Elevator Door Object and Positions")]
    public GameObject doorObject;
    public GameObject doorOpenPosition;
    public GameObject doorClosePosition;

    [Header("Elevator and Door Speed")]
    public float liftSpeed = 1;
    public float doorSpeed = 100;

    public Animator[] animators;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        MoveLift();
        MoveOuterDoor();
    }

    public void LiftUp() {
        //animator.SetInteger("Floor",2);
    }

    public void LiftDown()
    {
        //animator.SetInteger("Floor", 1);
    }

    private void MoveLift() {
        Vector3 targetPosition = currentFloor == 1 ? position1.transform.position :
                                    currentFloor == 2 ? position2.transform.position :
                                        currentFloor == 3 ? position3.transform.position :
                                            position4.transform.position;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * liftSpeed);

        Vector3 doorPosition = Vector3.Distance(transform.position, targetPosition) < 0.01f ? doorOpenPosition.transform.position :
                                                                                                doorClosePosition.transform.position;

        doorObject.transform.position = Vector3.MoveTowards(doorObject.transform.position,doorPosition, Time.deltaTime * doorSpeed);
    }

    public void SwitchFloor(int floorNum) {
        currentFloor = floorNum == 1 ? 1 :
                            floorNum == 2 ? 2 :
                                floorNum == 3 ? 3 :
                                    4;
    }

    private void MoveOuterDoor() {
        for (int i = 0; i < animators.Length; i++) {
            if (i == currentFloor - 1) {
                animators[i].SetBool("isOpen",true);
                // print(animators[i].GetBool("isOpen"));
            }
            else { 
                animators[i].SetBool("isOpen", false);
                // print(i == currentFloor - 1);
            }
        }
    }
    
}
