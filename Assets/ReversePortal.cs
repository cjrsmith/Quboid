using UnityEngine;

public class ReversePortal : MonoBehaviour
{
    [SerializeField]
    private GameObject teleportLocation;
    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    //If a player is detected, move them to the given location
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.position = teleportLocation.transform.position;
            sound.Play();
        }
    }
}
