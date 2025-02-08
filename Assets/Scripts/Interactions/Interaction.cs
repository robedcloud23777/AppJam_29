using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public abstract LangText interactionName { get; }
    public abstract void OnInteract();
    Player_Interactions player;

    private void OnEnable()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Interactions>();
    }
    bool added = false;
    private void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= player.interactionRange)
        {
            if (added) return;
            added = true;
            AddInteraction();
        }
        else
        {
            if (!added) return;
            added = false;
            RemoveInteraction();
        }
    }
    bool isAdded = false;
    protected void AddInteraction()
    {
        if (isAdded) return;
        isAdded = true;
        player.AddInteraction(this);
    }
    protected void RemoveInteraction()
    {
        if (!isAdded) return;
        isAdded = false;
        player.RemoveInteraction(this);
    }
}
