using UnityEngine;
using SBS.ME;
using UnityEngine.XR.Interaction.Toolkit;

public class BouleCassez : MonoBehaviour
{
    public float breakForce = 5f; // Force minimale pour casser
    public bool containsKey = false; // Si cette boule contient la clé
    public GameObject keyPrefab; // Préfab de la clé (à assigner dans l'Inspector)


     private Rigidbody rb;
    private MeshExploder meshExploder;
    private XRGrabInteractable grabInteractable;
    private bool isBroken = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshExploder = GetComponent<MeshExploder>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (meshExploder == null)
        {
            Debug.LogError("MeshExploder non trouvé sur " + gameObject.name);
        }
        // Désactive la gravité pour suspendre la boule
        rb.useGravity = false;
        rb.isKinematic = true; // Empêche les mouvements indésirables

        // Ajoute un listener pour détecter le grab
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'impact est suffisant et que la boule n'est pas déjà cassée
        if (!isBroken && collision.relativeVelocity.magnitude > breakForce)
        {
            BreakBall();
        }
    }

    void BreakBall()
    {
        isBroken = true;

        // Active l'explosion via le script MeshExploder
        if (meshExploder != null)
        {
            meshExploder.explodeNOW = true; // Suppose que la méthode s'appelle Explode()
        }

        // Si cette boule contient la clé, déclencher son apparition
        if (containsKey)
        {
            SpawnKey();
        }

    }
     void OnGrab(SelectEnterEventArgs args)
    {
        // Active la gravité quand on attrape la boule
        rb.useGravity = true;
        rb.isKinematic = false;
    }
    void SpawnKey()
    {
        if (keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Le prefab de la clé n'est pas assigné à " + gameObject.name);
        }
    }

}
