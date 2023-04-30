using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool NPCinRange = false;
    public GameObject InteractButton;
    [SerializeField] private TextAsset inkJSON;
    // Start is called before the first frame update
    void Start()
    {
        InteractButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCinRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDiaalogueMode(inkJSON);
                
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            NPCinRange = true;
            InteractButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            NPCinRange = false;
            InteractButton.SetActive(false);
        }
    }
}
