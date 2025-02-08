using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public static PlayerInteractions instance;

    public List<GameObject> interactions = new List<GameObject>();
    public float interactionRange;
    public LayerMask interactionLayer;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        interactions.Sort((a, b) => { return (int)Vector3.Distance(transform.position, a.transform.position) * 100 - (int)Vector3.Distance(transform.position, b.transform.position) * 100; });

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("??");
            interactions[0].GetComponent<Interaction>().OnInteract();
            
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}