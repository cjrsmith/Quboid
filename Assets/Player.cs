using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject spawner;
    [SerializeField]
    private Text pauseText;
    private bool begin = false;
    private bool paused = false;

    void Update()
    {
        if (paused && Input.GetButtonDown("Pause"))
        {
            resumeGame();
            pauseText.gameObject.SetActive(false);
            paused = false;
        }
        else if (!paused && Input.GetButtonDown("Pause"))
        {
            pauseGame();
            pauseText.gameObject.SetActive(true);
            paused = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Begin")
        {
            if (begin == false)
            {
                spawner.transform.GetComponent<BitSpawn>().startCountdown();
                begin = true;
            }
        }
    }

    void pauseGame()
    {
        Time.timeScale = 0;
    }
    void resumeGame()
    {
        Time.timeScale = 1;
    }
}
