using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RetryButton()
    { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    
}
