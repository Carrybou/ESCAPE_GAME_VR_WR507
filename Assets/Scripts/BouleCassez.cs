using UnityEngine;
using System.Collections.Generic;

public class BouleCassez : MonoBehaviour
{
    public Material interiorMaterial; // Matériau des parties intérieures cassées
    public float breakForce = 5f;     // Force minimale pour casser
    public bool containsKey = false;  // Si cette boule contient la clé

    private bool isBroken = false;
    private MeshDemolisher meshDemolisher;

    void Start()
    {
        meshDemolisher = new MeshDemolisher();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isBroken && collision.relativeVelocity.magnitude > breakForce)
        {
            BreakBall(collision.contacts[0].point);
        }
    }

    void BreakBall(Vector3 impactPoint)
    {
        isBroken = true;

        // Création du point d'impact pour la cassure
        GameObject breakPointObj = new GameObject("BreakPoint");
        breakPointObj.transform.position = impactPoint;
        List<Transform> breakPoints = new List<Transform> { breakPointObj.transform };

        // Vérification si la cassure est possible
        if (meshDemolisher.VerifyDemolishInput(gameObject, breakPoints))
        {
            List<GameObject> fragments = meshDemolisher.Demolish(gameObject, breakPoints, interiorMaterial);

            // Ajouter des Rigidbody aux fragments pour qu'ils tombent naturellement
            foreach (GameObject fragment in fragments)
            {
                if (!fragment.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = fragment.AddComponent<Rigidbody>();
                    rb.mass = 0.1f; // Masse faible pour un effet réaliste
                }
            }

            // Si cette boule contient la clé, la faire apparaître
            if (containsKey)
            {
                SpawnKey();
            }

            // Supprime l'objet original
            Destroy(gameObject);
        }

        // Détruit le point de cassure temporaire
        Destroy(breakPointObj);
    }

    void SpawnKey()
    {
        GameObject key = Instantiate(Resources.Load<GameObject>("KeyPrefab"), transform.position, Quaternion.identity);
        key.GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse);
    }
}
