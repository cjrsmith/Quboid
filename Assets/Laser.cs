using UnityEngine;

public abstract class Laser : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private Color startColour;
    [SerializeField]
    private Color endColour;
    private AudioSource sound;
    private LineRenderer beam;
    protected void Start()
    {
        sound = GetComponent<AudioSource>();

        beam = this.gameObject.AddComponent<LineRenderer>();
        beam.startWidth = 0.01f;
        beam.endWidth = 0.005f;

        beam.startColor = startColour;
        beam.endColor = endColour;

        beam.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

        float alpha = 0.8f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(startColour, 0.0f), new GradientColorKey(endColour, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        beam.colorGradient = gradient;
    }
    //Abstract class to allow for different mouse inputs on update
    protected abstract void Update();
    //Turn the laser on
    protected void checkLaser()
    {
        beam.SetPosition(0, spawnPoint.transform.position);
        beam.SetPosition(1, spawnPoint.transform.position + spawnPoint.transform.forward * 12f);
        beam.enabled = true;
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }
    //Turn the laser off
    protected void offLaser()
    {
        beam.enabled = false;
        if (sound.isPlaying)
        {
            sound.Stop();
        }
    }
}
