using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldTXT;

    [SerializeField] TextMeshProUGUI diamondTXT;

    public int goldGained;

    public int diamondsGained;

    // Start is called before the first frame update
    void Start()
    {
        goldGained = 0;
        diamondsGained = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        goldTXT.text = "Gold: " + goldGained;

        diamondTXT.text = "Diamonds: " + diamondsGained;
        
    }

}
