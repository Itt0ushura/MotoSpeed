using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject motorBike; //gameobj of bike
    [SerializeField] private Rigidbody2D wheelRb;
    public float Speed;

    private Camera _camera;
    public float CameraSpeed; // var for t component in Vector3.Lerp

    //ui block
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject inGameUI;

    [SerializeField] private TextMeshProUGUI speedKMH;
    [SerializeField] private TextMeshProUGUI meters;

    private Vector3 _distanceStart;
    private Vector3 _distanceEnd;

    [Tooltip("Pauses the game"), SerializeField] private Button pauseButton;
    [Tooltip("Restarting the game"), SerializeField] private Button restartButton;
    [Tooltip("Resume the game"), SerializeField] private Button resumeButton;

    private void Start()
    {
        _camera = Camera.main;
        _distanceStart = motorBike.transform.position;

        restartButton.onClick.AddListener(RestartGame);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);

        ShowUI();
    }

    private void FixedUpdate()
    {

        Ride();

    }

    private void LateUpdate()
    {

        CameraFollow();
        DistanceCounter();
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

            if (input > halfWidth)
            {

                SetSpeed(Speed);

            }

            if (input < halfWidth)
            {

                SetSpeed(-Speed);

            }

            return;

        }
    }

    private void SetSpeed(float speed)
    {

        wheelRb.AddForce(Vector2.right * speed, ForceMode2D.Force);

    }


    //Section for UI
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
    private void DistanceCounter()
    {

        _distanceEnd = motorBike.transform.position;

        float distance = Mathf.Round(Vector3.Distance(_distanceStart, _distanceEnd));
        meters.text = distance + " m";
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
        var kmhSpeed = wheelRb.velocity.x;
        speedKMH.text = Mathf.Abs(Mathf.Round(kmhSpeed)) + " km/h";
    }
}