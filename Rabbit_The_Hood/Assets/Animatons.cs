using UnityEngine;

public class Animatons : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Update()
    {
        
    }

    public void WalkForward(bool isWalking)
    {
        animator.SetBool("WalkForward", isWalking);
    }
}
