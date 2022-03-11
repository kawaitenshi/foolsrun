using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player object's transform'
    public Transform PlayerTransform;

    // relative position between camera and player
    public Vector3 cameraOffset;

    // smooth factor of moving camera and following player
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;

    // multiplier for camera rotation
    public float rotateSpeedH = 3.0f;
    public float rotateSpeedV = 3.0f;

    // initial camera rotation angle
    public Quaternion center;

    // camera rotaion angle limit
    private float minAngle = -45f;
    private float maxAngle = 45f;

    // camera zoom related
    public Transform Obstruction;
    public float zoomSpeed = 0.1f;
    public float defaultDistance;
    public float minDistance = 1.0f;

    // Start is called before the first frame update
    void Start() {
        // record relative position between camera and player
        this.cameraOffset = this.transform.position - PlayerTransform.position;
        // record initial camera rotation angle
        this.center = transform.rotation;
        // set default obstruction
        Obstruction = PlayerTransform;
        // change player transform to focus point child element (if change directly in unity there will be bugs)
        PlayerTransform = PlayerTransform.Find("Focus");
        // calculate default distance from camera to player
        defaultDistance = Vector3.Distance(PlayerTransform.position, this.transform.position);
    }

    // LateUpdate is called after Update methods
    void LateUpdate() {
        if (Time.timeScale == 1.0f) {
            // let camera follow player
            Vector3 newPosition = PlayerTransform.position + this.cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

            // get mouse input can calculate rotation angles
            float rotateH = Input.GetAxis("Mouse X") * rotateSpeedH;
            float rotateV = Input.GetAxis("Mouse Y") * rotateSpeedV * -1;
        
            // perform horizontal rotation
            Quaternion cameraTurnAngleY = Quaternion.AngleAxis(rotateH, Vector3.up);
            Quaternion cameraTurnAngleZ, cameraTurnAngleX;
        
            // calculate vertical rotation on Z axis within limit
            if (cameraOffset[2] >= 0) {
                cameraTurnAngleZ = Quaternion.AngleAxis(rotateV, Vector3.left);
            } else {
                cameraTurnAngleZ = Quaternion.AngleAxis(rotateV, Vector3.right);
            }
        
            // calculate vertical rotation on X axis within limit
            if (cameraOffset[0] >= 0) {
                cameraTurnAngleX = Quaternion.AngleAxis(rotateV, Vector3.forward);
            } else {
                cameraTurnAngleX = Quaternion.AngleAxis(rotateV, Vector3.back);
            }

            // perform vertical rotation on Z axis within limit (bugs in limit need to be fixed)
            Quaternion tempZ = transform.rotation * cameraTurnAngleZ;
            //Debug.Log(Quaternion.Angle(this.center, tempZ));
            //if (Quaternion.Angle(this.center, tempZ) < this.maxAngle) {
                this.cameraOffset = cameraTurnAngleZ * this.cameraOffset;
            //}
        
            // perform vertical rotation on X axis within limit (bugs in limit need to be fixed)
            Quaternion tempX = transform.rotation * cameraTurnAngleX;
            //if (Quaternion.Angle(this.center, tempX) < this.maxAngle) {
                this.cameraOffset = cameraTurnAngleX * this.cameraOffset;
            //}

            // perform horizontal rotation
            this.cameraOffset = cameraTurnAngleY * this.cameraOffset;

            // update center angle
            this.center = cameraTurnAngleY * this.center;

            // let camera focus on character
            this.transform.LookAt(PlayerTransform.position);

            // zoom camera if obstructed
            viewObstructed();
        }
    }

    public void viewObstructed() {
        RaycastHit hit;
        // set obstructing object back to normal (need to find the right render or this won't work) (may not need at all)
        // if (Obstruction.gameObject.tag != "Player") {
        //    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        // }

        if (Physics.Raycast(PlayerTransform.position, this.transform.position - PlayerTransform.position, out hit, defaultDistance)) {
            if (hit.collider.gameObject.tag != "Player") {
                Obstruction = hit.transform;
                // set obstructing object to transparent (need to find the right render or this won't work) (may not need at all)
                // Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                
                if (Vector3.Distance(this.transform.position, PlayerTransform.position) > minDistance) {
                // calculate direction and distance of zooming in
                    Vector3 direction = cameraOffset.normalized;
                    float distance = Vector3.Distance(PlayerTransform.position, this.transform.position) - hit.distance;
                    Debug.Log("Camera to obstruction: " + hit.distance + ", Camera to player: " + Vector3.Distance(this.transform.position, PlayerTransform.position) + ", Move distance: " + distance);
                    
                    // zoom in
                    transform.position += transform.forward * distance;
                }
            }
        } else {
            // set obstructing object back to normal (need to find the right render or this won't work) (may not need at all)
            // if (Obstruction.gameObject.tag != "Player") {
            //     Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            // }
            
            if (Vector3.Distance(this.transform.position, PlayerTransform.position) < defaultDistance) {
                // calculate direction and distance of zooming out
                Vector3 direction = cameraOffset.normalized;
                float distance = Vector3.Distance(PlayerTransform.position, this.transform.position) - hit.distance;

                // zoom out
                transform.position -= transform.forward * distance* zoomSpeed;
            }
        }
    }
}
