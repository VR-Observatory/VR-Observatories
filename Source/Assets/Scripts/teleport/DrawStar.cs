using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DrawStar : MonoBehaviour
{
    public Texture2D texture;
    public Texture2D texture02;

    public bool Crosshairs_visible=false;
    public LayerMask guideboard_layer;
    public Camera main_camera;
    public GameObject teleport_notice;
    public GameObject elevator_notice;
    public bool whether_first = false;
    public bool whether_Andriod = false;
    private bool whether_VR = false;
    private bool whether_colorchanged = false;
    private GameObject last_hit;
    void Start()
    {
        if (OVRManager.isHmdPresent)
        {
            whether_VR = true;
        }
    }

    // Use this for initialization
    void OnGUI()
    {
        if (whether_Andriod == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                //cvs2.GetComponent<button_delete>().debugClick();
                teleport_notice.SetActive(false);
                elevator_notice.SetActive(false);
            }
        }

        if (Crosshairs_visible == true && !whether_VR && whether_Andriod == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                //cvs2.GetComponent<button_delete>().debugClick();
                teleport_notice.SetActive(false);
                elevator_notice.SetActive(false);
            }
                //Debug.Log("show");
                Vector3 center_pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            Rect rect = new Rect(center_pos.x - (texture.width / 2), Screen.height - center_pos.y - (texture.height / 2), texture.width, texture.height);

            Ray ray = main_camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, guideboard_layer))
            {
                GUI.DrawTexture(rect, texture);
                if (hit.collider.gameObject.GetComponent<click_teleport>() == null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        ColorBlock cb = hit.collider.gameObject.GetComponent<Button>().colors;
                        hit.collider.gameObject.GetComponent<Image>().color = cb.pressedColor;
                        whether_colorchanged = true;
                        last_hit = hit.collider.gameObject;
                        //hit.collider.gameObject.GetComponent<Button>().colors = cb;
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (hit.collider.gameObject.GetComponent<click_teleport>() == null)
                    {
                        ColorBlock cb = hit.collider.gameObject.GetComponent<Button>().colors;
                        hit.collider.gameObject.GetComponent<Image>().color = cb.normalColor;
                        whether_colorchanged = false;

                        //hit.collider.gameObject.GetComponent<Button>().colors = cb;
                        //cvs2.GetComponent<button_delete>().debugClick();
                        //hit.collider.gameObject.GetComponent<click_teleport>().OnClick();
                        hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<click_teleport>().OnClick();
                    }

                    
                    //hit.collider.gameObject.GetComponent<Button>().Select();
                }
            }
            else
            {

                if (whether_colorchanged == true)
                {
                    ColorBlock cb = last_hit.GetComponent<Button>().colors;
                    last_hit.GetComponent<Image>().color = cb.normalColor;
                    whether_colorchanged = false;
                }


                Rect rect02 = new Rect(center_pos.x - (texture02.width / 2), Screen.height - center_pos.y - (texture02.height / 2), texture02.width, texture02.height);
                GUI.DrawTexture(rect, texture02);

            }
        }

    }
}