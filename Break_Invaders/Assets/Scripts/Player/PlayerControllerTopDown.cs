using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerTopDown : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f, strafeSpeed = 7.5f, rotationSpeed = 1.0f;

    private Rigidbody rb;

    private TargetCollection tc;

    private PlayerSpeedPush push;

    private bool cursorVisible = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tc = GetComponent<TargetCollection>();
        push = GetComponent<PlayerSpeedPush>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tc.gameOver)
        {
            float v = Input.GetAxis("Vertical") * moveSpeed; //Forwards and Backwards
            float s = Input.GetAxis("Strafe") * strafeSpeed; //Strafe left and right
                                                             //float r = Input.GetAxis("Horizontal") * rotationSpeed; //Rotate left and right

            //if (r != 0)
            //{
            //    transform.Rotate(0.0f, r, 0.0f); //Rotate around y axis
            //}

            RaycastHit mouseHit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                transform.LookAt(new Vector3(mouseHit.point.x, transform.position.y, mouseHit.point.z));
            }

            if(!push.pushInProgress)
            {
                if (v != 0 || s != 0)
                {
                    rb.velocity = (Vector3.forward * v) + (Vector3.right * s);
                    //rb.velocity = (transform.forward * v) + (transform.right * s);
                }
            }
        }

    }
}
