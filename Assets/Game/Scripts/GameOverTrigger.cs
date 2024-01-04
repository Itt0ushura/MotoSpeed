using System.Collections;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        HeadCam();
        StartCoroutine(DeathTimer(2f));
        StopCoroutine("DeathTimer");
    }
    IEnumerator DeathTimer(float time)
    {
        yield return new WaitForSeconds(time);
        gameManager.ShowGameOver();
    }
    private void HeadCam()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position, 0.4f);
    }
}
