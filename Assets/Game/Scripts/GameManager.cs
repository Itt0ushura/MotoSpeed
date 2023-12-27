using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject motorBike; //gameobj of bike to manipulate with it
    [SerializeField] private WheelJoint2D wheelJoint;
    private JointMotor2D _motor; //var for manipulating with motorSpeed of wheel
    private Camera _camera;
    public float CameraSpeed; // var for t component in Vector3.Lerp


    [SerializeField] private Rigidbody2D rb;
    void Start()
    {
        _camera = Camera.main;
    }
    private void FixedUpdate()
    {
        RideControl();
    }
    void LateUpdate()
    {
        CameraFollow();
    }
    void CameraFollow()
    {
        Vector3 currentCamPosition = _camera.transform.position;
        Vector3 targetCamPosition = new Vector3(motorBike.transform.position.x, motorBike.transform.position.y, _camera.transform.position.z);
        Vector3 newCamPosition = Vector3.Lerp(currentCamPosition, targetCamPosition, CameraSpeed * Time.deltaTime);
        _camera.transform.position = newCamPosition;
    }
    void RideControl()
    {
        if (Input.touchCount > 0)
        {
            _motor.maxMotorTorque = 2000;
            if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                _motor.motorSpeed = Mathf.Lerp(_motor.motorSpeed, -1000f, Time.fixedDeltaTime);
                wheelJoint.motor = _motor;
            }
            if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                _motor.motorSpeed = Mathf.Lerp(_motor.motorSpeed, 1000f, Time.fixedDeltaTime);
                wheelJoint.motor = _motor;
            }
        }
        else
        {
            _motor.motorSpeed = Mathf.Lerp(_motor.motorSpeed, 0f, Time.fixedDeltaTime);
            wheelJoint.motor = _motor;
        }
    }
}
