using UnityEngine;
using SBS.ME;

public class BouleCassez : MonoBehaviour
{
    public float breakForce = 5f; // Force minimale pour casser
    public bool containsKey = false; // Si cette boule contient la clé
    public GameObject keyPrefab; // Préfab de la clé (à assigner dans l'Inspector)


    private bool isBroken = false;
    private MeshExploder meshExploder; // Référence au script d'explosion

    private void Start()
    {
        // Récupère automatiquement le script MeshExploder attaché à la boule
        meshExploder = GetComponent<MeshExploder>();

        if (meshExploder == null)
        {
            Debug.LogError("MeshExploder non trouvé sur " + gameObject.name);
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
