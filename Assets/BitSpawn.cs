using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BitSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private AudioClip countDownSound;
    [SerializeField]
    private AudioClip loseSound;
    [SerializeField]
    private AudioClip spawnSound;
    private AudioSource sound;
    [SerializeField]
    private int population;
    private float speed = 1f;
    private Vector3 direction;
    private double[][] zero = new double[2][];
    private double[][] one = new double[2][];
    private int time = 9;
    [SerializeField]
    private GameObject display1;
    [SerializeField]
    private GameObject display2;
    [SerializeField]
    private GameObject display3;
    [SerializeField]
    private GameObject display4;
    [SerializeField]
    private GameObject display5;
    [SerializeField]
    private GameObject origin;
    [SerializeField]
    private Canvas GOCanvas;
    private int qubitCount;
    private int stableTimeStart = 180;
    private int stableTimeEnd = 900;

    void Start()
    {
        zero = Constants.zeroState();
        one = Constants.oneState();
        sound = GetComponent<AudioSource>();
        qubitCount = population;
    }
    void Update()
    {
        if (qubitCount == 0)
        {
            sound.PlayOneShot(loseSound, 1);
            //Activate GameOver screen
            GOCanvas.GetComponent<Canvas>().enabled = true;
            player.transform.GetComponent<CameraRayCast>().stopMusic();
        }
    }

    public void startCountdown()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (time > 0)
        {
            sound.PlayOneShot(countDownSound, 1);
            display1.transform.GetComponent<TextMesh>().text = time.ToString();
            display2.transform.GetComponent<TextMesh>().text = time.ToString();
            display3.transform.GetComponent<TextMesh>().text = time.ToString();
            display4.transform.GetComponent<TextMesh>().text = time.ToString();
            display5.transform.GetComponent<TextMesh>().text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }
        display1.transform.GetComponent<TextMesh>().text = "";
        display2.transform.GetComponent<TextMesh>().text = "";
        display3.transform.GetComponent<TextMesh>().text = "";
        display4.transform.GetComponent<TextMesh>().text = "";
        display5.transform.GetComponent<TextMesh>().text = "";
        spawn();
        yield return new WaitForSeconds(2.5f);
        origin.gameObject.SetActive(false);
    }

    void spawn()
    {
        for (int i = 0; i < population; i++)
        {
            sound.PlayOneShot(spawnSound, 0.1f);
            int time = Random.Range(stableTimeStart, stableTimeEnd);
            int state = Random.Range(0, 10);
            direction = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
            if (state <= 5)
            {
                GameObject z = Instantiate(sphere, new Vector3(50f, 50f, 50f), Quaternion.identity) as GameObject;
                z.transform.GetComponent<Qubit>().initialSpawn(zero);
                z.transform.GetComponent<Qubit>().startDecoherence(time, gameObject);
                z.GetComponent<Rigidbody>().velocity = direction * speed;
            }
            else
            {
                GameObject o = Instantiate(sphere, new Vector3(50f, 50f, 50f), Quaternion.identity) as GameObject;
                o.transform.GetComponent<Qubit>().initialSpawn(one);
                o.transform.GetComponent<Qubit>().startDecoherence(time, gameObject);
                o.GetComponent<Rigidbody>().velocity = direction * speed;
            }
        }
    }
    public void reduceCount()
    {
        qubitCount--;
    }
    public void restartLevel()
    {
        SceneManager.LoadScene("Level01");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
