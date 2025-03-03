using UnityEngine;

public class BouleCassez : MonoBehaviour
{
    public GameObject brokenVersion;  // Version cassée
    public float breakForce = 5f;     // Force minimale pour casser
    public bool containsKey = false;  // Si cette boule contient la clé
    private bool isBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isBroken && collision.relativeVelocity.magnitude > breakForce)
        {
            BreakBall();
        }
    }

    void BreakBall()
    {
        isBroken = true;
        Instantiate(brokenVersion, transform.position, transform.rotation); // Remplace par la version cassée
        if (containsKey)
        {
            SpawnKey();
        }
        Destroy(gameObject); // Détruit l'ancienne boule
    }

    void SpawnKey()
    {
        GameObject key = Instantiate(Resources.Load<GameObject>("KeyPrefab"), transform.position, Quaternion.identity);
        key.GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse); // Petit effet de rebond
    }
}
