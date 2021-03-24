using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI diamondTXT;

    [SerializeField] TextMeshProUGUI diamondTXT1;

    [SerializeField] GameObject niceTMP;

    [SerializeField] Animator niceTmpAnimator;

    public ParticleSystem bubbleBlowPS;

    public int diamondsGained;

    public float waitForNicePopup;

    private bool popup = true;

    // Start is called before the first frame update
    void Start()
    {
        diamondsGained = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        diamondTXT.text = "" + diamondsGained;

        diamondTXT1.text = "" + diamondsGained;

    }

    public void NiceTextStartSequance()
    {
        var rndPopupTXT = UnityEngine.Random.Range(0, 4);

        var tmpText = "";

        switch (rndPopupTXT)
        {
            case 0:

                tmpText = "Sweet!";

                break;
            case 1:

                tmpText = "Great!";

                break;
            case 2:

                tmpText = "Nice!";

                break;
            case 3:

                tmpText = "Wow!";

                break;

        }

        StartCoroutine(NiceTextPopup(tmpText));

    }

    public IEnumerator NiceTextPopup(string popupTXT)
    {
        niceTMP.GetComponent<TextMeshProUGUI>().text = popupTXT;

        niceTMP.SetActive(true);

        niceTmpAnimator.SetTrigger("textLarge");

        Debug.Log("before wait");

        yield return new WaitForSeconds(waitForNicePopup);

        Debug.Log("waited");

        niceTMP.gameObject.SetActive(false);

    }

    public void PlayBubbleParticleInPosition(Vector3 pos)
    {
        bubbleBlowPS.transform.position = pos;

        bubbleBlowPS.Play();

    }

}
