using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float screenEdgeBuffer = .3f;
    public float safeCloudDistance = 10f;
    public float smoothTime = .3f;
    public GameObject safeCloud;


    private GameObject player;

    private Vector3 velocity;
    private float camOrthSize;
    //private float camOffset;

    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        velocity = Vector3.zero;
        camOrthSize = GetComponent<Camera>().orthographicSize;
        //camOffset = transform.position.y - player.transform.position.y;
	}
	
	void FixedUpdate ()
    {
        Scroll();
        MoveSafeCloud();
	}

    private void Scroll()
    {
        float verticalDistance = player.transform.position.y - transform.position.y;

        //moves camera at same speed as player
        if (Mathf.Abs(verticalDistance) > camOrthSize * (1f - screenEdgeBuffer))
        {
            velocity = Vector3.up * player.GetComponent<Rigidbody2D>().velocity.y;
            transform.position += velocity * Time.deltaTime;
        }

        //slows camera to target position
        else
        {
            if (velocity != Vector3.zero)
            {
                Vector3 targetPos =
                      new Vector3(transform.position.x,
                                  player.transform.position.y,
                                  transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, targetPos,
                                                        ref velocity, smoothTime);
            }
        }
    }

    private void MoveSafeCloud()
    {
        float safeDistance = transform.position.y - camOrthSize - safeCloud.transform.position.y;
        if (safeDistance > safeCloudDistance)
        {
            Vector3 targetPos = safeCloud.transform.position;
            targetPos.y += safeDistance - safeCloudDistance;
            safeCloud.transform.position = targetPos;
        }
    }

    public Vector3 GetCameraVelocity()
    {
        return velocity;
    }
}
