using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.VFX;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
using TMPro;

public class PointMeterScript : MonoBehaviour
{
    [Header ("Serialized References")]

    [SerializeField] CenterGuysSphereScript cgsSC;

    [SerializeField] MyGuyCenterController myGuyccSC;

    [SerializeField] CameraFollowScript cameraSC;

    [SerializeField] CanvasController canvasControlSC;

    [SerializeField] Rigidbody centerSphereRB;

    [SerializeField] GameObject winPopup;

    [SerializeField] TextMeshProUGUI diamondFinal;

    [SerializeField] TextMeshProUGUI diamondFinal1;

    [SerializeField] ParticleSystem[] confettiArray = new ParticleSystem[4];

    [SerializeField] VisualEffect[] fireworksArray = new VisualEffect[4];

    [SerializeField] Animator[] spotlightsArray = new Animator[2];

    [SerializeField] float waitAtEnd;

    [SerializeField] float buttonSpeed;

    [SerializeField] GameObject TopUI;

    private Vector3 buttonEndPos;

    private void Start()
    {
        buttonEndPos = new Vector3(0f, -40f, 0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other : " + other.name);

        if (other.CompareTag("MyGuy"))
        {
            Debug.Log("Point meter collision");

            myGuyccSC.fallSpeed = 0;

            myGuyccSC.glideSpeed = 0;

            centerSphereRB.isKinematic = false;

            centerSphereRB.mass = 2;

            centerSphereRB.freezeRotation = true;

            // cameraSC.isLanded = true;

            StartCoroutine(StartButtonClickLerp());

        }

    }

    private IEnumerator StartButtonClickLerp()
    {
        var timeSinceStartedPress = 0.0f;

        while (true)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, buttonEndPos, timeSinceStartedPress * buttonSpeed);

            timeSinceStartedPress += Time.deltaTime;

            if (transform.localPosition == buttonEndPos)
            {
                break;

            }

            yield return null;

        }

        PointMeterClick();

    }

    private void PointMeterClick()
    {
        foreach (Animator anime in spotlightsArray)
        {
            anime.SetTrigger("SpotlightOn");

        }

        foreach (GameObject item in cgsSC.guysParachute)
        {
            item.SetActive(false);

        }

        foreach (ParticleSystem confetti in confettiArray)
        {
            confetti.Play();

        }

        foreach (VisualEffect fire in fireworksArray)
        {
            fire.Play();

        }

        StartCoroutine(OpenWinPopup());

    }

    private IEnumerator OpenWinPopup()
    {
        yield return new WaitForSeconds(waitAtEnd);

        diamondFinal.text = " X " + canvasControlSC.diamondsGained;

        diamondFinal1.text = " X " + canvasControlSC.diamondsGained;

        TopUI.SetActive(false);

        winPopup.SetActive(true);

    }

}
