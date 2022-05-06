using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour, IInteractable
{
    public GameObject WindowGameObject;
    public void Interact()
    {
        WindowGameObject.SetActive(true);

        var player = FindObjectOfType<PlayerController>();

        player.CanInteract = false;
        player.DeactivateMovement = true;
        player.DeactivateRotation = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
