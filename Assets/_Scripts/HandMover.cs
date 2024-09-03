using UnityEngine;
using DG.Tweening; // Make sure you have the DOTween namespace

public class HandMover : MonoBehaviour
{
    public GameObject Hand;
    public float leftPosition = 0; // X position for the left side
    public float rightPosition = 0; // X position for the right side
    public float duration = 1f;      // Duration for each movement

    void Start()
    {
        if (PlayerPrefs.GetInt("HowToPlay") == 0)
        {
            MoveHand();
        }
    }

    void MoveHand()
    {
        // Move the hand to the right
        Hand.transform.DOMoveX(rightPosition, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Once the hand reaches the right, move it to the left
            Hand.transform.DOMoveX(leftPosition, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                // Repeat the movement by calling the same function
                MoveHand();
            });
        });
    }
}
