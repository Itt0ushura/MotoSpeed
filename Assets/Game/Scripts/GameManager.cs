using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject motorBike; //gameobj of bike to manipulate with it

    [SerializeField] private WheelJoint2D wheelJoint;
    private JointMotor2D _motor; //var for manipulating with motorSpeed of wheel

    private Camera _camera;
    public float CameraSpeed; // var for t component in Vector3.Lerp

    //ui block
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject inGameUI;

    [SerializeField] private TextMeshProUGUI speedRPM;

    [SerializeField] private TextMeshProUGUI meters;

    private Vector3 _distanceStart;
    private Vector3 _distanceEnd;
    private float _distanceValue;

    [Tooltip("Pauses the game"), SerializeField] private Button pauseButton;
    [Tooltip("Restarting the game"), SerializeField] private Button restartButton;
    [Tooltip("Resume the game"), SerializeField] private Button resumeButton;

    private void Start()
    {
        _distanceStart = motorBike.transform.position;
        restartButton.onClick.AddListener(RestartGame);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        _camera = Camera.main;
        ShowUI();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Ride();
    }

    private void LateUpdate()
    {
        CameraFollow();
        DistanceCounter(_distanceValue);
        RPMdisplay();
    }

    private void CameraFollow()
    {

        float smoothness = CameraSpeed * Time.deltaTime;

        Vector3 currentCamPosition = _camera.transform.position;
        Vector3 targetCamPosition = new Vector3(motorBike.transform.position.x, motorBike.transform.position.y, _camera.transform.position.z);
        _camera.transform.position = Vector3.Lerp(currentCamPosition, targetCamPosition, smoothness);

    }


    private void Ride()
    {

        if (Input.touchCount > 0)
        {

            var input = Input.GetTouch(0).position.x;
            int halfWidth = Screen.width / 2;
            _motor.maxMotorTorque = 2000;

            if (input > halfWidth)
            {

                SetSpeed(-1000f);
                

            }

            if (input < halfWidth)
            {

                SetSpeed(1000f);
            }

            return;

        }

        SetSpeed(0f);

    }

    private void SetSpeed(float speed)
    {

        _motor.motorSpeed = Mathf.Lerp(_motor.motorSpeed, speed, Time.fixedDeltaTime);
        wheelJoint.motor = _motor;
    }


    //Section for work with UI
    public void ShowUI()
    {

        inGameUI.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);

    }
    public void ShowPause()
    {

        inGameUI.SetActive(false);
        pauseScreen.SetActive(true);
        gameOverScreen.SetActive(false);

    }
    public void ShowGameOver()
    {

        inGameUI.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(true);

    }
    private void DistanceCounter(float distance)
    {

        _distanceEnd = motorBike.transform.position;

        distance = Vector3.Distance(_distanceStart, _distanceEnd);

        meters.text = Mathf.Round(distance) + " m";
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("moto");
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        ShowPause();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        ShowUI();
    }
    private void RPMdisplay()
    {
        speedRPM.text = Mathf.Abs(Mathf.Round(_motor.motorSpeed)) + " RPM";
    }
}