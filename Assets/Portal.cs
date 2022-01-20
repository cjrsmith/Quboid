using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject teleportLocation;

    private AudioSource sound;
    [SerializeField]
    private AudioClip teleportSound;
    [SerializeField]
    private AudioClip winSound;
    private GameObject winCanvas;
    private bool canTeleport = false;
    [SerializeField]
    private GameObject instruction;
    [SerializeField]
    private string instructionInput;
    [SerializeField]
    private GameObject hologram;
    [SerializeField]
    private GameObject platform;
    private bool winState = false;

    void Start()
    {
        winCanvas = GameObject.Find("WinCanvas"); ;
        sound = GetComponent<AudioSource>();
        setInstruction();
        setState();
    }

    //Set the instruction text above the qubit hologram.
    private void setInstruction()
    {
        instruction.transform.GetComponent<TextMesh>().text = instructionInput;
    }
    //If winState is not true and a player is detected then transport them to the given location.
    //If winState is true then display the win canvas.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && canTeleport)
        {
            if (!winState)
            {
                collision.transform.position = teleportLocation.transform.position;
                sound.PlayOneShot(teleportSound, 1);
            }
            else
            {
                sound.PlayOneShot(winSound, 1);
                //Activate winning screen
                winCanvas.GetComponent<Canvas>().enabled = true;
                //collision.transform.GetComponent<Laser>().stopMusic();
                collision.transform.GetChild(0).GetChild(0).GetComponent<CameraRayCast>().stopMusic();
            }
        }
    }
    //Set the teleport state
    public void teleport(bool decision)
    {
        canTeleport = decision;
    }
    //Set the state based on the platform associated
    private void setState()
    {
        switch (platform.transform.tag)
        {
            case "Begin":
                hologram.transform.GetComponent<Hologram>().setState(platform.transform.GetComponent<Begin>().getState());
                break;
            case "Platform2":
                hologram.transform.GetComponent<Hologram>().setState(platform.transform.GetComponent<Platform2>().getState());
                break;
            case "Platform3":
                hologram.transform.GetComponent<Hologram>().setState(platform.transform.GetComponent<Platform3>().getState());
                break;
            case "Platform4":
                hologram.transform.GetComponent<Hologram>().setState(platform.transform.GetComponent<Platform4>().getState());
                break;
            case "Platform5":
                hologram.transform.GetComponent<Hologram>().setState(platform.transform.GetComponent<Platform5>().getState());
                break;
            case "Final":
                hologram.transform.GetComponent<Hologram>().setState(platform.transform.GetComponent<Final>().getState());
                break;
        }
    }
    //Activate the portal (allow player to use it)
    public void activate()
    {
        hologram.SetActive(false);
        instruction.SetActive(false);
        canTeleport = true;
    }
    //Activate the portal to be the winning one
    public void win()
    {
        hologram.SetActive(false);
        instruction.SetActive(false);
        winState = true;
        canTeleport = true;
    }
}
