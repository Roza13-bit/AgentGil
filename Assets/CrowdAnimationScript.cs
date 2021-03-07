using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAnimationScript : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();

        SetCrowdAnimations();

    }

    void SetCrowdAnimations()
    {
        var rnd = UnityEngine.Random.Range(0, 3);

        switch (rnd)
        {
            case 0:

                myAnimator.SetTrigger("Cheer1");

                break;
            case 1:

                myAnimator.SetTrigger("Cheer2");

                break;
            case 2:

                myAnimator.SetTrigger("Cheer3");

                break;

        }

    }

}
