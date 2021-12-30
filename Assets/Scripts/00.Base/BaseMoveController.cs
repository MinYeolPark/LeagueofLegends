using UnityEngine;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseMoveController : MonoBehaviour
{

    private Animator anim;
    private NavMeshAgent agent;

    float motionSmoothTime = .1f;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);     
    }

    public void OnRightClick(InputValue value)
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            {
                //if (raycastHit.collider.TryGetComponent(out LeagueObjectData component))
                //{
                //    Debug.Log("Set Target");
                //}

                {
                    //Move
                    agent.SetDestination(raycastHit.point);
                    agent.stoppingDistance = 0;

                    //ROTATION
                    Quaternion rotationToLookAt = Quaternion.LookRotation(raycastHit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y,
                        ref rotateVelocity,
                        rotateSpeedMovement * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }
            }
        }
    }
}
