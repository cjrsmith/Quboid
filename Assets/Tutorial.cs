using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private int count = 0;
    [SerializeField]
    private GameObject moveToPoint2;
    [SerializeField]
    private GameObject moveToPoint3;
    [SerializeField]
    private GameObject moveToPoint4;
    [SerializeField]
    private GameObject moveToPoint5;
    [SerializeField]
    private GameObject moveToPoint6;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Canvas continueCanvas;
    [SerializeField]
    private Canvas Scene1canvas;
    [SerializeField]
    private Canvas Scene2canvas;
    [SerializeField]
    private Canvas Scene3canvas;
    [SerializeField]
    private Canvas Scene4canvas;
    [SerializeField]
    private Canvas Scene5canvas;
    [SerializeField]
    private Canvas Scene6canvas;
    [SerializeField]
    private GameObject qubit;
    [SerializeField]
    private Canvas missionCanvas;
    private bool recordReturn = true;
    private Canvas[] canvasList = new Canvas[6];
    void Start()
    {
        canvasList[0] = Scene1canvas;
        canvasList[1] = Scene2canvas;
        canvasList[2] = Scene3canvas;
        canvasList[3] = Scene4canvas;
        canvasList[4] = Scene5canvas;
        canvasList[5] = Scene6canvas;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && recordReturn)
        {
            count++;
            recordReturn = false;
        }
        moveCamera();
    }
    public void moveCamera()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        switch (count)
        {
            case 2:
                //Deactivate continuecanvas
                deactivateContinueCanvas();
                transform.position = Vector3.MoveTowards(transform.position, moveToPoint2.transform.position, step);
                if (transform.position == moveToPoint2.transform.position)
                {
                    endScene();
                    //Activate new canvas
                    activateNewCanvas(Scene2canvas);
                }
                break;
            case 4:
                //Deactivate continuecanvas
                deactivateContinueCanvas();
                transform.position = Vector3.MoveTowards(transform.position, moveToPoint3.transform.position, step);
                if (transform.position == moveToPoint3.transform.position)
                {
                    endScene();
                    //Activate new canvas
                    activateNewCanvas(Scene3canvas);
                }
                break;
            case 6:
                //Deactivate continuecanvas
                deactivateContinueCanvas();
                transform.position = Vector3.MoveTowards(transform.position, moveToPoint4.transform.position, step);
                if (transform.position == moveToPoint4.transform.position)
                {
                    endScene();
                    //Activate new canvas
                    activateNewCanvas(Scene4canvas);
                }
                break;
            case 8:
                //Deactivate continuecanvas
                deactivateContinueCanvas();
                transform.position = Vector3.MoveTowards(transform.position, moveToPoint5.transform.position, step);
                if (transform.position == moveToPoint5.transform.position)
                {
                    endScene();
                    //Activate new canvas
                    activateNewCanvas(Scene5canvas);
                }
                break;
            case 10:
                //Deactivate continuecanvas
                deactivateContinueCanvas();
                transform.position = Vector3.MoveTowards(transform.position, moveToPoint6.transform.position, step);
                if (transform.position == moveToPoint6.transform.position)
                {
                    endScene();
                    //Activate new canvas
                    activateNewCanvas(Scene6canvas);
                    //end game
                    qubit.transform.GetComponent<TutorialQubit>().endTutorial();
                }
                break;
            case 12:
                //load game
                missionCanvas.gameObject.GetComponentInChildren<Canvas>().enabled = true;
                break;
        }
    }
    // Get the current number of times the return key has been pressed
    public int getCount()
    {
        return count;
    }
    public void endScene()
    {
        recordReturn = true;
    }
    public void activateNewCanvas(Canvas c)
    {
        c.gameObject.GetComponentInChildren<Canvas>().enabled = true;
    }
    public void deactivateCurrentCanvas()
    {
        foreach (Canvas c in canvasList)
        {
            c.gameObject.GetComponentInChildren<Canvas>().enabled = false;
        }
    }
    public void activateContinueCanvas()
    {
        continueCanvas.gameObject.GetComponentInChildren<Canvas>().enabled = true;
    }
    public void deactivateContinueCanvas()
    {
        continueCanvas.gameObject.GetComponentInChildren<Canvas>().enabled = false;
    }
    public void begin()
    {
        SceneManager.LoadScene("Level01");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
