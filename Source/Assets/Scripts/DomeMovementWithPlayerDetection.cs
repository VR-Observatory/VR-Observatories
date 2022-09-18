using UnityEngine;

public class DomeMovementWithPlayerDetection : MonoBehaviour
{
    public Animator dome;

    private bool _isIn;

    private static readonly int Move = Animator.StringToHash("Move");
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Open Roof"))
        {
            DomeMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _isIn = false;
        }
    }

    private void DomeMove()
    {
        if (_isIn)
        {
            if (!dome.IsInTransition(0) && dome.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                dome.SetBool(Move,!dome.GetBool(Move));
            }
        }
    }
}