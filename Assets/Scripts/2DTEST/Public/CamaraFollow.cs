using UnityEngine;
using System.Collections;

public class CamaraFollow : MonoBehaviour
{
    public Transform target;  //主角
    public float xMargin = 1f; // Distance in the x axis the player can move before the camera follows.
    public float xSmooth = 3f; // How smoothly the camera catches up with it's target movement in the x axis.
    public Vector2 clampX; // The minimum x and y coordinates the camera can have.
    public Camera ca;

    float adapt;  //适配值
    private void Awake()
    {
        // Setting up the reference.
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        ca = GetComponent<Camera>();
        adapt = ca.orthographicSize * 2 - ca.orthographicSize * ca.aspect;
        clampX.x -= adapt;
        clampX.y += adapt;
    }

    private bool CheckXMargin()
    {
        // Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
        return Mathf.Abs(transform.position.x - target.position.x) > xMargin;
    }

    private void Update()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        float targetX = transform.position.x;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, target.position.x, xSmooth * Time.deltaTime);
        }
        targetX = Mathf.Clamp(targetX, clampX.x, clampX.y);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    //---------------------------- 
    public void LimitCaView(float x1, float x2)
    {
        clampX.x = x1 + ca.orthographicSize * 2 - adapt;
        clampX.y = x2 - ca.orthographicSize * 2 + adapt;
    }
    public void LimitCaView(Vector2 v)
    {
        clampX.x = v.x + ca.orthographicSize * 2 - adapt;
        clampX.y = v.y - ca.orthographicSize * 2 + adapt;
    }
}