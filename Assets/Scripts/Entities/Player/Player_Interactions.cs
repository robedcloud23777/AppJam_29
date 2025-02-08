using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Interactions : MonoBehaviour
{
    [SerializeField] float m_interactionRange;
    public float interactionRange => m_interactionRange;

    [SerializeField] TMP_Text interactionText;
    List<Interaction> interactions = new();

    public void OnUpdate()
    {
        interactions.Sort((a, b) => { return Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)); });

        if (interactions.Count > 0)
        {
            interactionText.transform.position = Camera.main.WorldToScreenPoint(interactions[0].transform.position);
            interactionText.text = $"[E] {interactions[0].interactionName.text}";
            interactionText.gameObject.SetActive(true);
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            interactions[0].OnInteract();
        }
    }
    public void AddInteraction(Interaction interaction)
    {
        interactions.Add(interaction);
    }
    public void RemoveInteraction(Interaction interaction)
    {
        interactions.Remove(interaction);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}