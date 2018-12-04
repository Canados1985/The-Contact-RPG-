using UnityEngine;

public class Interactable : MonoBehaviour {


    public float f_radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;

    Transform player;
    bool hasInteracted = false;



    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }


    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = true;
        player = null;
        hasInteracted = false;
    }

    void Update()
    {
        if (isFocus && !hasInteracted && this.gameObject.tag == "PickUp")
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if(distance <= f_radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, f_radius);
    }

}
