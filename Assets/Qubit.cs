using System.Collections;
using UnityEngine;
using System;

public class Qubit : Bit
{
    private AudioSource sound;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player Camera");
        sound = GetComponent<AudioSource>();
    }
    //Mathod which calls the Coroutine to start decoherence.
    public void startDecoherence(int time, GameObject spawner)
    {
        StartCoroutine(decoherence(time, spawner));
    }
    //Method which calls the Coroutine to start timer on destroying qubit after being measured.
    public void measureQubit()
    {
        StartCoroutine(destroyAfterMeasure());
    }
    //After a given length of time the quibit will collapse.
    IEnumerator decoherence(int time, GameObject spawner)
    {
        yield return new WaitForSeconds(time);
        player.transform.GetComponent<CameraRayCast>().Collapse(gameObject);
        measureQubit();
        spawner.transform.GetComponent<BitSpawn>().reduceCount();
    }
    //After being measured the qubit will sit for 10 seconds before being destroyed.
    IEnumerator destroyAfterMeasure()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
    //Initial states spawned by the BitSpaner at the start of the game
    public void initialSpawn(double[][] firstState)
    {
        Start();
        state = firstState;
        calcThetaPhi(firstState);
        prZero = QMath.probZero(firstState);
        prOne = QMath.probOne(firstState);
        spawnState();
    }
    //Collapse a qubit to a base state probabilisticaly 
    public void collapse()
    {
        //Roulette wheel selection to determine measure state.
        var r = UnityEngine.Random.Range(0f, 1f);
        var probList = new ArrayList
        {
            prOne,
            prZero
        };
        probList.Sort();
        if (prOne == prZero)
        {
            if (r <= 0.5)
            {
                //Collapse to zero
                state = Constants.zeroState();
                initialSpawn(state);
            }
            else
            {
                //Collapse to one
                state = Constants.oneState();
                initialSpawn(state);
            }
        }
        else if (r <= (double)probList[0])
        {
            //Collapse to that state
            if (prOne < prZero)
            {
                //Collapse to one
                state = Constants.oneState();
                initialSpawn(state);
            }
            else
            {
                //Collapse to zero
                state = Constants.zeroState();
                initialSpawn(state);
            }
        }
        else
        {
            //Collapse to the other
            if (prOne > prZero)
            {
                //Collapse to one
                state = Constants.oneState();
                initialSpawn(state);
            }
            else
            {
                //Collapse to zero
                state = Constants.zeroState();
                initialSpawn(state);
            }
        }
    }

    //Check if a number is close to zero and if so round it to zero. Also if a number is close then round it. 
    //Avoids innacuracies due to Cos and Sin use.
    void checkCloseToZero(double[][] state)
    {
        foreach (double[] c in state)
        {
            for (int i = 0; i < c.Length; i++)
            {
                if (Math.Abs(c[i]) < 0.01)
                {
                    c[i] = 0;
                }
                else if (Math.Abs(Math.Abs(c[i]) - Math.Round(c[i])) < 0.01)
                {
                    c[i] = Math.Round(c[i]);
                }
            }
        }
    }

}
