using UnityEngine;
using UnityEngine.UI;

public class CameraRayCast : MonoBehaviour
{
    [SerializeField]
    private AudioClip correct;
    [SerializeField]
    private AudioClip measured;
    private AudioSource sound;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject moveToPoint;
    [SerializeField]
    private Text statText;
    [SerializeField]
    private GameObject hudPanel;
    [SerializeField]
    private GameObject bloch;
    [SerializeField]
    private GameObject spawner;
    private bool captured;
    private double[][] required = new double[2][];
    private GameObject currentPlatform;
    private bool measure = false;
    private bool capture = false;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Holding down both left and right mouse buttons.
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            Release();
            Measure();
        }
        //Holding down just left button.
        else if (Input.GetMouseButton(0))
        {
            Hold();
            //Holding down both left and tab button.
            if (Input.GetKey(KeyCode.Tab))
            {
                MoveCloser();
            }
        }
        //Holding down just right mouse button.
        else if (Input.GetMouseButton(1))
        {
            Measure();
        }
        //Releasing left mouse button after capturing a qubit.
        if (Input.GetMouseButtonUp(0) && captured)
        {
            Release();
        }
    }
    //Pull a captured qubit closer to you by holding down the "tab" key.
    void MoveCloser()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.tag == "captured")
            {
                raycastHit.transform.position = Vector3.MoveTowards(raycastHit.transform.position, moveToPoint.transform.position, 0.4f);
            }
        }
    }
    //Hold a qubit in place and move it around by holding down the left mouse button.
    void Hold()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.tag == "qubit")
            {
                captured = true;
                raycastHit.transform.parent = mainCamera.transform;
                raycastHit.transform.GetComponent<Rigidbody>().isKinematic = true;
                raycastHit.transform.tag = "captured";
                turnTagsOff();
                displayStats(raycastHit);
                if (capture && !measure)
                {
                    check(required, raycastHit.transform.GetComponent<Qubit>().getStateArray());
                }
            }
        }

    }
    //Apply a force to the qubit that was captured and propel it forward in the direction the player is facing.
    void Release()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.tag == "captured")
            {
                captured = false;
                raycastHit.transform.parent = null;
                raycastHit.transform.GetComponent<Rigidbody>().isKinematic = false;
                raycastHit.transform.GetComponent<Rigidbody>().velocity = transform.forward * 30f;
                raycastHit.transform.tag = "qubit";
                turnTagsOn();
                hudPanel.SetActive(false);
                bloch.SetActive(false);
                bloch.transform.GetComponent<BlochSphereRotate>().resetState();
            }
        }
    }
    //Measure a qubit, collapsing it into a basis state. The qubit stops and drops to the floor, unable to be manipulated further.
    public void Measure()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.tag == "qubit")
            {
                raycastHit.transform.GetComponent<Qubit>().collapse();
                raycastHit.transform.parent = null;
                raycastHit.transform.GetComponent<Rigidbody>().isKinematic = false;
                raycastHit.transform.GetComponent<Rigidbody>().useGravity = true;
                raycastHit.transform.GetComponent<Rigidbody>().mass = 100;
                raycastHit.transform.GetComponent<Rigidbody>().angularDrag = 10;
                raycastHit.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, -20, 0);
                raycastHit.transform.GetComponent<SphereCollider>().enabled = false;
                raycastHit.transform.GetComponent<BoxCollider>().enabled = true;
                raycastHit.transform.tag = "collapsed";
                sound.PlayOneShot(measured);
                //Compare states and check if winning
                if (measure && !capture)
                {
                    check(required, raycastHit.transform.GetComponent<Qubit>().getStateArray());
                }
                spawner.transform.GetComponent<BitSpawn>().reduceCount();
                raycastHit.transform.GetComponent<Qubit>().measureQubit();
            }
        }
    }
    public void Collapse(GameObject qubit)
    {
        if (qubit.tag == "captured")
        {
            turnTagsOn();
            hudPanel.SetActive(false);
            bloch.SetActive(false);
            bloch.transform.GetComponent<BlochSphereRotate>().resetState();
        }
        qubit.transform.GetComponent<Qubit>().collapse();
        qubit.transform.parent = null;
        qubit.transform.GetComponent<Rigidbody>().isKinematic = false;
        qubit.transform.GetComponent<Rigidbody>().useGravity = true;
        qubit.transform.GetComponent<Rigidbody>().mass = 100;
        qubit.transform.GetComponent<Rigidbody>().angularDrag = 10;
        qubit.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, -20, 0);
        qubit.transform.GetComponent<SphereCollider>().enabled = false;
        qubit.transform.GetComponent<BoxCollider>().enabled = true;
        qubit.transform.tag = "collapsed";
        sound.PlayOneShot(measured);
    }
    //Find all the objects in the game labelled as "quibit" and change their tag to "no-qubit".
    //This is so that other qubits arene't captured whilst already captured another.
    void turnTagsOff()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("qubit");
        foreach (GameObject obst in obstacles)
            obst.tag = "no-qubit";
    }
    //Dual of the method above. Turn all tags back to "quibit" after releasing the captured qubit.
    void turnTagsOn()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("no-qubit");
        foreach (GameObject obst in obstacles)
            obst.tag = "qubit";
    }
    void displayStats(RaycastHit bit)
    {
        hudPanel.SetActive(true);
        statText.text = "Theta: " + bit.transform.GetComponent<Qubit>().getTheta() + "\u00B0" +
                        "\r\nPhi: " + bit.transform.GetComponent<Qubit>().getPhi() + "\u00B0" +
                        "\r\nProbability |0>: " + bit.transform.GetComponent<Qubit>().getProbZero() + "%" +
                        "\r\nProbability |1>: " + bit.transform.GetComponent<Qubit>().getProbOne() + "%" +
                        "\r\n" + bit.transform.GetComponent<Qubit>().getState();
        bloch.SetActive(true);
        bloch.transform.GetComponent<BlochSphereRotate>().setState(bit.transform.GetComponent<Qubit>().getTheta(), bit.transform.GetComponent<Qubit>().getPhi());
    }
    public void requiredState(GameObject platform, double[][] required, bool measure, bool capture)
    {
        currentPlatform = platform;
        this.required = required;
        this.measure = measure;
        this.capture = capture;
    }
    //Check if have what is required
    void check(double[][] required, double[][] have)
    {
        if (have[0][0] == required[0][0] && have[0][1] == required[0][1] && have[1][0] == required[1][0] && have[1][1] == required[1][1])
        {
            sound.PlayOneShot(correct, 1);
            switch (currentPlatform.transform.tag)
            {
                case "Begin":
                    currentPlatform.transform.GetComponent<Begin>().updatePortal();
                    break;
                case "Platform2":
                    currentPlatform.transform.GetComponent<Platform2>().updatePortal();
                    break;
                case "Platform3":
                    currentPlatform.transform.GetComponent<Platform3>().updatePortal();
                    break;
                case "Platform4":
                    currentPlatform.transform.GetComponent<Platform4>().updatePortal();
                    break;
                case "Platform5":
                    currentPlatform.transform.GetComponent<Platform5>().updatePortal();
                    break;
                case "Final":
                    currentPlatform.transform.GetComponent<Final>().updatePortalWin();
                    break;
            }
        }
    }
    public void stopMusic()
    {
        transform.GetComponent<AudioSource>().Stop();
    }
}
