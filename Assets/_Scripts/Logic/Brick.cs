using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Coroutine destroyRoutine = null;

    public GameObject vfx_BrickExplosion;

    private void OnCollisionEnter(Collision other)
    {
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("Ball")) return;
        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        GameManager.Instance.OnBrickDestroyed(transform.position);
        Destroy(gameObject);
        // instantiate the particle effect at the brick's position
        GameObject particleInstance = Instantiate(vfx_BrickExplosion, transform.position, Quaternion.identity);
        // then destroy the particle instance after 1 second
        Destroy(particleInstance, 1f);
    }
}
