using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    //------------------------Variables---------------------------------
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool PlayerInRange;
    //-------------------------------------------------------------------
    void Start()
    {  
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)&& PlayerInRange) {
            if (dialogBox.activeInHierarchy)
                {
                    dialogBox.SetActive(false);
                }
            else 
                { 
                    dialogBox.SetActive(true);
                     dialogText.text = dialog;
                }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            PlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
