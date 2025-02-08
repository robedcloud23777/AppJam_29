using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public abstract void OnInteract();
    bool isInInteractRange;

    private void Update()
    {
        if(Vector3.Distance(PlayerInteractions.instance.transform.position, transform.position) <= PlayerInteractions.instance.interactionRange && !isInInteractRange)
        {
            PlayerInteractions.instance.interactions.Add(gameObject);
            isInInteractRange = true;
        }
        else if(Vector3.Distance(PlayerInteractions.instance.transform.position, transform.position) > PlayerInteractions.instance.interactionRange && isInInteractRange)
        {
            PlayerInteractions.instance.interactions.Remove(gameObject);
            isInInteractRange = false;
        }
    }
}
