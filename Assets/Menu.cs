using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Canvas beginCanvas;
    [SerializeField]
    private Canvas creditCanvas;
    public void startGame()
    {
        beginCanvas.GetComponent<Canvas>().enabled = true;
    }
    public void credits()
    {
        creditCanvas.GetComponent<Canvas>().enabled = true;
    }
    public void begin()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void mainMenu()
    {
        beginCanvas.GetComponent<Canvas>().enabled = false;
        creditCanvas.GetComponent<Canvas>().enabled = false;
    }
    public void close()
    {
        creditCanvas.GetComponent<Canvas>().enabled = false;
    }
}
