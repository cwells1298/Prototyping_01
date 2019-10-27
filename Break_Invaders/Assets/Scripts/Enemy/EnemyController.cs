using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool needToMove = true;

    public FiringPosition firePos;

    public Vector3 targetPosition;

    public Vector3 startPos;

    public float maxMoveTime = 5.0f;
    [SerializeField]
    private float currentMoveTime = 0.0f;

    public float rotationMax = 45.0f;

    private int positionInQueue = 0;

    private TargetCollection tc;
    private Target target;

    private void Start()
    {
        tc = FindObjectOfType<TargetCollection>();
    }

    void Update()
    {
        if (needToMove)
        {         
            float percentage = currentMoveTime / maxMoveTime;
            if (percentage >= 1.0f)
            {
                needToMove = false;
                firePos.healthCanvas.SetActive(true);
                if (tc.targets.Count >= 1)
                {
                    RotateToTarget();
                }
            }
            else
            {
                Vector3 newPos = Vector3.Lerp(startPos, targetPosition, percentage);
                transform.position = newPos;
                currentMoveTime += Time.deltaTime;
            }
        }
        else
        {
            if (!tc.targets.Contains(target) && tc.targets.Count >= 1) //Check if current target is still alive
            {
                RotateToTarget();
            }
        }



    }

    void RotateToTarget()
    {
        int randTarget = Random.Range(0, tc.targets.Count);
        target = tc.targets[randTarget];

        Vector3 targetPos = new Vector3(target.transform.position.x, 0.0f, target.transform.position.z);
 
        transform.LookAt(targetPos);

        float startRot = transform.localEulerAngles.y;
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);
        float newRot = (rand1 - rand2) * rotationMax;

        transform.Rotate(new Vector3(0.0f, newRot, 0.0f));
        bool validFacing = false;

        while (validFacing == false)
        {
            RaycastHit hit;

            if (Physics.Raycast(new Vector3(transform.position.x, 1.25f, transform.position.z), transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    validFacing = false;

                    transform.localEulerAngles = new Vector3(0.0f, startRot, 0.0f);
                    rand1 = Random.Range(0.0f, 1.0f);
                    rand2 = Random.Range(0.0f, 1.0f);
                    newRot = (rand1 - rand2) * rotationMax;
                    transform.Rotate(new Vector3(0.0f, newRot, 0.0f));
                }
                else
                {
                    validFacing = true;
                }
            }
            else
            {
                validFacing = true;
            }
        }
    }
}
