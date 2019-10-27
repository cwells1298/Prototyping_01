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

            RaycastHit mouseHit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity ,layerMask))
            {
                if (Vector3.Distance(mouseHit.point, transform.position) > 1.75f)
                {
                    if (MouseOptions.mouseInverted)
                    {
                        transform.LookAt(new Vector3(-mouseHit.point.x, transform.position.y, -mouseHit.point.z));
                    }
                    else
                    {
                        transform.LookAt(new Vector3(mouseHit.point.x, transform.position.y, mouseHit.point.z));
                    }
                }            

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
