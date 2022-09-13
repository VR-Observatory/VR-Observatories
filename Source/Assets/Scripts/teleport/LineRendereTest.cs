using UnityEngine;

public class LineRendereTest : MonoBehaviour
{

    public LineRenderer line;

    private Vector3 screenCenterPoint;

    // Use this for initialization

    void Start()
    {

        screenCenterPoint = Camera.main.ScreenToWorldPoint

            (new Vector3(Screen.width / 2, Screen.height / 2, 1));

    }

    // Update is called once per frame

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("test1");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("test2");
                line.SetPositions(new Vector3[] { screenCenterPoint, hitInfo.point });

            }

        }

    }

}