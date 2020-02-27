using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float mass = 1.0f;

    public float maxSpeed = 5;
    public float maxForce = 10;

    public float speed = 0;

    public bool seekEnabled = false;
    public Vector3 target;
    public Transform targetTransform;
    public List<Vector3> waypoints;
    public int numwaypoints = 5;
    private int current = 1;
    private bool looped;

    public bool arriveEnabled = false;
    public float slowingDistance = 10;

    [Range(0.0f, 10.0f)]
    public float banking = 0.1f;

    public bool playerSteeringEnabled = false;
    public float playerForce = 100;

    public float damping = 0.1f;

    public PathFollowing pursueTarget;
    public bool pursueEnabled;
    public Vector3 pursueTargetPos;
    private bool fleeEnabled = false;

    public Vector3 PlayerSteering()
    {
        Vector3 f = Vector3.zero;

        f += Input.GetAxis("Vertical") * transform.forward * playerForce;

        Vector3 projectedRight = transform.right;
        projectedRight.y = 0;
        projectedRight.Normalize();

        f += Input.GetAxis("Horizontal") * projectedRight * playerForce * 0.2f;


        return f;
    }


    // Start is called before the first frame update
    void Start()
    {
        waypoints = targetTransform.GetComponent<Path>().waypoints;
        looped = targetTransform.GetComponent<Path>().isLooped;
    }

    public void OnDrawGizmos()
    {
        int startIndex;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + acceleration);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + velocity);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(targetTransform.position, slowingDistance);

        Gizmos.color = Color.red;

        if (pursueEnabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pursueTargetPos, 0.1f);
        }

        if (looped)
        {
            startIndex = 0;
        }

        else
        {
            startIndex = 1;
        }

        if (waypoints != null)
        {
            for (int i = startIndex; i < waypoints.Count; i++)
            {
                if (i == waypoints.Count - 1)
                {
                    Gizmos.DrawLine(waypoints[i], waypoints[startIndex]);
                }

                else
                {
                    Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
                }
            }
        }
    }

    Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;

        float ramped = (dist / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = (toTarget / dist) * clamped;

        return desired - velocity;
    }

    Vector3 Seek(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        Vector3 desired = toTarget.normalized * maxSpeed;

        return desired - velocity;
        //return toTarget - velocity;
    }

    public Vector3 Pursue(PathFollowing pursueTarget)
    {
        float dist = Vector3.Distance(pursueTarget.transform.position, transform.position);
        float time = dist / maxSpeed;

        pursueTargetPos = pursueTarget.transform.position + pursueTarget.velocity * time;

        return Seek(pursueTargetPos);
    }

    public Vector3 Flee(Vector3 target)//PathFollowing pursueTarget
    {
        Vector3 toTarget = transform.position - target;
        Vector3 desired = toTarget.normalized * maxSpeed;

        return desired - velocity;
        //return -Pursue(pursueTarget);
    }

    public Vector3 CalculateForce()
    {
        Vector3 force = Vector3.zero;
        if (seekEnabled)
        {
            force += Seek(target);
        }
        if (arriveEnabled)
        {
            force += Arrive(target);
        }
        if (playerSteeringEnabled)
        {
            force += PlayerSteering();
        }
        if (pursueEnabled)
        {
            force += Pursue(pursueTarget);
        }
        if (fleeEnabled)
        {
            force += Flee(pursueTargetPos);
            //force += Flee(pursueTarget);
        }
        return force;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null)
        {
            target = waypoints[current];
            if ((target - transform.position).magnitude < 1)
            {
                current = (current + 1) % waypoints.Count;

                if (current == 0 && !looped)
                {
                    current = 1;
                }
            }
        }

        else if (targetTransform != null)
        {
            target = targetTransform.position;
        }

        force = CalculateForce();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
        speed = velocity.magnitude;
        if (speed > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);
            //transform.forward = velocity;
            velocity -= (damping * velocity * Time.deltaTime);
        }

        if (this.gameObject.tag == "Chaser")
        {
            Debug.Log(speed);
        }
    }
}
