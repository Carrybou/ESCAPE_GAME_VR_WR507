using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyLock : MonoBehaviour
{
    public Transform keySlot; // Position où la clé doit être placée
    public GameObject keyObject; // La clé à insérer
    public CoffreLock coffreLock; // Référence au script CoffreLock du coffre

    private bool keyInserted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == keyObject && !keyInserted)
        {
            InsertKey(other.gameObject);
        }
    }

    void InsertKey(GameObject key)
    {
        keyInserted = true;
        Debug.Log("Clé insérée ! Le coffre va s'ouvrir.");

        // Désactiver les interactions XR pour empêcher la prise de la clé
        XRGrabInteractable grabInteractable = key.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }

        // Désactiver la gravité et bloquer la clé en place
        Rigidbody rb = key.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero; // Stopper tout mouvement
            rb.angularVelocity = Vector3.zero;
        }

        // Placer la clé exactement dans la serrure
        key.transform.position = keySlot.position;
        key.transform.rotation = keySlot.rotation;

        // Déverrouiller le coffre
        coffreLock.UnlockCoffre();
    }
}
