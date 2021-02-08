using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI diamondTXT;

    public int diamondsGained;

    // Start is called before the first frame update
    void Start()
    {
        diamondsGained = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        diamondTXT.text = ":" + diamondsGained;
        
    }

}
