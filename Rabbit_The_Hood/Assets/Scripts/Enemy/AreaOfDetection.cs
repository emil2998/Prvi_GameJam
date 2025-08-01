using System.Collections;
using UnityEngine;

public class AreaOfDetection : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHP player))
        {
            enemyMovement.playerSpotted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHP player))
        {

            StartCoroutine(Cooldown());

        }
    }

    private IEnumerator Cooldown()
    {

        yield return new WaitForSeconds(2f);
        enemyMovement.playerSpotted = false;

    }


}
