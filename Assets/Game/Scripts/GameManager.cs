using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject motorBike; //gameobj of bike to manipulate with it
    private Camera _camera;
    public float CameraSpeed; // var for t component in Vector3.Lerp
    void Start()
    {
        _camera = Camera.main;
    }
    void LateUpdate()
    {
        //CameraFollowRaw();
        //CameraFollowLerpBad();
        CameraFollowLerbGood();
    }
    void CameraFollowRaw() //test feature
    {
        Vector3 cameraPos = new Vector3(motorBike.transform.position.x, motorBike.transform.position.y, -9);
        _camera.transform.position = cameraPos;
    }
    void CameraFollowLerpBad() //test feature
    {
        Vector3 storage = _camera.transform.position;
        storage.y = Mathf.Lerp(_camera.transform.position.y, motorBike.transform.position.y, 0.3f);
        storage.x = Mathf.Lerp(_camera.transform.position.x, motorBike.transform.position.x, 0.3f);
        _camera.transform.position = storage;
    }
    void CameraFollowLerbGood()
    {
        Vector3 current = _camera.transform.position;
        Vector3 target = new Vector3(motorBike.transform.position.x, motorBike.transform.position.y, _camera.transform.position.z);
        Vector3 lerp = Vector3.Lerp(current, target, CameraSpeed * Time.deltaTime);
        _camera.transform.position = lerp;
    }
}
