using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HywirlController : MonoBehaviour
{
    
    [SerializeField]
    private List<Transform> path; 
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private CharacterController controller;
    private int currentTargetIndex = 0;
    private float pathProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (path.Count > 0)
        {
            transform.position = path[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path.Count < 2) return; // Need at least two points to move between

        if (Input.GetKey(KeyCode.D)) 
        {
            MoveAlongPath(-1);
        }
        else if (Input.GetKey(KeyCode.A)) 
        {
            MoveAlongPath(1);
        }
    }

    private void MoveAlongPath(int direction)
    {
        // Calculate the target index based on direction
        int targetIndex = Mathf.Clamp(currentTargetIndex + direction, 0, path.Count - 1);
        if (targetIndex != currentTargetIndex)
        {
            // pathProgress = 0f; // Reset progress when we change targets
            currentTargetIndex = targetIndex;
        }

        // Increment the progress towards the next waypoint
        pathProgress += speed * Time.deltaTime;
        pathProgress = Mathf.Clamp01(pathProgress);

        // Determine the current and next waypoints
        Transform startPoint = path[currentTargetIndex];
        Transform endPoint = path[(currentTargetIndex + direction + path.Count) % path.Count];

        // Move the character smoothly between the current waypoint and the next
        Vector3 newPosition = Vector3.Lerp(startPoint.position, endPoint.position, pathProgress);
        controller.Move(newPosition - transform.position); // Move method requires a relative vector

        // If we've reached the target waypoint, prepare for the next segment
        if (pathProgress >= 1f)
        {
            pathProgress = 0f; // Reset progress
            currentTargetIndex = (currentTargetIndex + direction + path.Count) % path.Count; // Loop around the path
        }
    }
}
