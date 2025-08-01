using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerHP player))
        {
            player.Damage(damage);
        }
    }
}
