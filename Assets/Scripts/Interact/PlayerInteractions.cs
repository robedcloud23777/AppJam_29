using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public static PlayerInteractions instance;

    public List<GameObject> interactions = new List<GameObject>();
    public float interactionRange;
    public LayerMask interactionLayer;

    public GameObject marker;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        interactions.Sort((a, b) => { return (int)Vector3.Distance(transform.position, a.transform.position) * 100 - (int)Vector3.Distance(transform.position, b.transform.position) * 100; });

        if (interactions.Count > 0)
        {
            marker.SetActive(true);
            marker.transform.position = interactions[0].transform.position + Vector3.up;
        }
        else
            marker.SetActive(false);


        if (Input.GetKeyDown(KeyCode.A))
        {
            interactions[0].GetComponent<Interaction>().OnInteract();

        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}