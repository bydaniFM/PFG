using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

    #region Attributes

    /// <summary>
    /// Target to follow.
    /// </summary>
    public Transform target;

    /// <summary>
    /// Interpolation speed.
    /// </summary>
    private float smoothSpeed = 0.125f;

    /// <summary>
    /// Offset between player and camera.
    /// </summary>
    private Vector3 offset = new Vector3(-32, 25, -32);

    /// <summary>
    /// Camera.
    /// </summary>
    private Camera cam;

    /// <summary>
    /// Zoom in/out speed.
    /// </summary>
    private float scrollSpeed = 200;

    #endregion

    #region Methods

    /// <summary>
    /// On Start...
    /// </summary>
    void Start()
    {
        //Sets the camera properties.
        this.cam = GetComponent<Camera>();
        this.cam.orthographic = true;
        //this.cam.orthographicSize = 8;
    }

    /// <summary>
    /// Each frame...
    /// </summary>
    void Update()
    {
        //Zooms in or out with mouse scroll.
        this.cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        //Limits the zoom.
        if (this.cam.orthographicSize > 15)
            this.cam.orthographicSize = 15;
        else if (this.cam.orthographicSize < 10)
            this.cam.orthographicSize = 10;
    }

    /// <summary>
    /// At the end of each frame...
    /// </summary>
    private void LateUpdate()
    {
        if (target != null)
        {
            //Final position of the camera.
            Vector3 desiredPosition = target.position + offset;

            //Transition between the actual and desired position.
            if (this.transform.position != desiredPosition)
                this.transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }        
    }

    #endregion
}