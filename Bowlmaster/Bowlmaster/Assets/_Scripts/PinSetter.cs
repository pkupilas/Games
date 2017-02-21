﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    public int lastStandingCount = 1;
    public Text standingCountText;
    public GameObject pinSet;

    private bool ballEnteredBox = false;
    private float lastChangeTime;
    private Ball ball;

	// Use this for initialization
	void Start ()
	{
	    ball = FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        standingCountText.text = CountStanding().ToString();

	    if (ballEnteredBox)
	    {
	        UpdateStandingCountAndSettle();
	    }
	}

    private void UpdateStandingCountAndSettle()
    {
        int currentStandingPins = CountStanding();
        float settleTime = 3;
        if (currentStandingPins != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStandingPins;
        }
        else if ((Time.time - lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
        }
    }

    private void PinsHaveSettled()
    {
        ball.Reset();
        lastStandingCount = -1;
        ballEnteredBox = false;
        standingCountText.color = Color.green;
    }

    public int CountStanding()
    {
        var pins = FindObjectsOfType<Pin>();
        int standingPinsCounter = 0;

        foreach (var pin in pins)
        {
            if (pin.IsStanding())
            {
                standingPinsCounter++;
            }
        }
        return standingPinsCounter;
    }

    public void OnTriggerEnter(Collider coll)
    {
        GameObject objectToHit = coll.gameObject;

        if (objectToHit.GetComponent<Ball>() != null)
        {
            standingCountText.color = Color.red;
            ballEnteredBox = true;
        }
    }

    public void RenewPins()
    {
        // TODO: Check other form of Instantiate
        GameObject pins = Instantiate(pinSet);
        pins.transform.position = new Vector3(0,10,1829);
    }

    public void RaisePins()
    {
        var pins = FindObjectsOfType<Pin>();

        foreach (var pin in pins)
        {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins()
    {
        var pins = FindObjectsOfType<Pin>();

        foreach (var pin in pins)
        {
            pin.Lower();
        }
    }
}
