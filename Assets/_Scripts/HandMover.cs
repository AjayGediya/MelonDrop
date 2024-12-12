using UnityEngine;
using DG.Tweening; // Make sure you have the DOTween namespace

public class HandMover : MonoBehaviour
{
    public GameObject hand;
    public float leftSidePosition = 0; // X position for the left side
    public float rightSidePosition = 0; // X position for the right side
    public float durationTime = 1f;      // Duration for each movement

    void Start()
    {
        if (PlayerPrefs.GetInt("HowToPlay") == 0)
        {
            MoveHandObject();
        }
    }

    void MoveHandObject()
    {
        hand.transform.DOMoveX(rightSidePosition, durationTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            hand.transform.DOMoveX(leftSidePosition, durationTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                MoveHandObject();
            });
        });
    }
}
