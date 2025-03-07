using UnityEngine;

public class CoffreLock : MonoBehaviour
{
    private HingeJoint hinge;
    private bool isUnlocked = false;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();

        if (hinge != null)
        {
            hinge.useLimits = true; // Active les limites pour éviter que le couvercle ne s'ouvre sans clé
            JointLimits limits = hinge.limits;
            limits.min = 0; // Le coffre est verrouillé au départ
            limits.max = 0;
            hinge.limits = limits;
        }
    }

    public void UnlockCoffre()
    {
        if (isUnlocked) return; // Évite de débloquer plusieurs fois

        isUnlocked = true;
        Debug.Log("Coffre déverrouillé !");

        if (hinge != null)
        {
            JointLimits limits = hinge.limits;
            limits.min = -90; // Permet l'ouverture du couvercle
            limits.max = 0;
            hinge.limits = limits;
        }
    }
}
