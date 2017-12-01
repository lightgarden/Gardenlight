using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public bool tile = true; //tesselate?
    public float bgScrollSpeed = 1f; //relative to camera speed
    public GameObject background;

    private float backgroundHeight;
    private GameObject[] tesselate;
    private float camOrthSize;
    private Vector3 cameraVelocity;
    private Transform cameraTransform;
    private int bottom;
    private int top;

    void Start ()
    {
        backgroundHeight = background.GetComponent<SpriteRenderer>().bounds.size.y;
        cameraTransform = transform;
        camOrthSize = GetComponent<Camera>().orthographicSize;
        cameraVelocity = Vector3.zero;

        //initialize tiles to tesselate
        if (tile)
        {
            tesselate = new GameObject[(int)Mathf.Ceil((2 * camOrthSize + backgroundHeight) / backgroundHeight)];
            bottom = 0;
            top = tesselate.Length - 1;
            for (int i = 0; i < tesselate.Length; i++)
            {
                Vector3 position = new Vector3(cameraTransform.position.x,
                                               cameraTransform.position.y - camOrthSize
                                                + i * backgroundHeight + backgroundHeight / 2,
                                               cameraTransform.position.z + 20);
                GameObject unit = Object.Instantiate(background, position, Quaternion.identity);
                unit.name = "Tile " + i.ToString();
                tesselate[i] = unit;
            }
            background.SetActive(false);
        }
    }

    void Update()
    {
        cameraVelocity = GetComponent<CameraController>().GetCameraVelocity();

        //transform array of tiles and tesselate up or down accordingly
        if (tile)
        {
            foreach (GameObject unit in tesselate)
            {
                unit.transform.position += cameraVelocity * bgScrollSpeed * Time.deltaTime;
            }

            if (cameraTransform.position.y - camOrthSize < (tesselate[bottom].transform.position.y - backgroundHeight / 2))
            {
                TesselateDown();
            }

            if (cameraTransform.position.y + camOrthSize > (tesselate[top].transform.position.y + backgroundHeight / 2))
            {
                TesselateUp();
            }
        }
        else
        {
            background.transform.position += cameraVelocity * bgScrollSpeed * Time.deltaTime;
        }
    }

    private void TesselateUp()
    {
        tesselate[bottom].transform.position = Vector3.up *
            (tesselate[top].transform.position.y + backgroundHeight);
        top = bottom;
        bottom++;
        if (bottom == tesselate.Length) bottom = 0;
    }

    private void TesselateDown()
    {
        tesselate[top].transform.position = Vector3.up *
            (tesselate[bottom].transform.position.y - backgroundHeight);
        bottom = top;
        top--;
        if (top < 0) top = tesselate.Length - 1;
    }

}
