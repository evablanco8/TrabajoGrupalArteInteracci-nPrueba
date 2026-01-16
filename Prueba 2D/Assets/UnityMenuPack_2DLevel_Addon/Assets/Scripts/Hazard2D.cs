using UnityEngine;

public class Hazard2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var ctrl = other.GetComponent<Player2DController>();
            if (ctrl != null) ctrl.DisableControl();
            GameManager2D.Instance?.PlayerDied();
        }
    }
}
