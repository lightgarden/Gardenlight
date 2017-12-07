using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public float bgScrollSpeed = 0f; //relative to camera speed
    public GameObject[] backgrounds;
    public GameObject scaleReference;
    private Vector3 cameraVelocity;

    void Start ()
    {
        cameraVelocity = Vector3.zero;
        foreach (GameObject bg in backgrounds)
          newScale(bg);

        backgrounds[0].transform.position =
            new Vector3 (0, backgrounds[0].transform.position.y, 20);
        float oldHeight = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
        float curHeight;
        for (int i = 1; i < backgrounds.Length; i++)
        {
            curHeight = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.y;
            float vert = backgrounds[i-1].transform.position.y;
            vert += oldHeight/2 + curHeight/2;
            backgrounds[i].transform.position = new Vector3 (0, vert, 20);
            oldHeight = curHeight;
        }
    }

    void Update()
    {
        cameraVelocity = GameObject.Find("Main Camera").GetComponent<CameraController>().GetCameraVelocity();
        foreach (GameObject bg in backgrounds)
            bg.transform.position += cameraVelocity * bgScrollSpeed * Time.deltaTime;
    }

    public void newScale(GameObject obj)
    {
        float reference = scaleReference.GetComponent<SpriteRenderer>().bounds.size.x;
        float size = obj.GetComponent<SpriteRenderer> ().bounds.size.x;
        Vector3 rescale = obj.transform.localScale;
        rescale.x = reference * rescale.x / size;
        obj.transform.localScale = rescale;
    }

}
