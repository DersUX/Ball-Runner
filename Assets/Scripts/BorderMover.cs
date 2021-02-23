using UnityEngine;
using DG.Tweening;

public class BorderMover : MonoBehaviour
{
    [SerializeField] private Vector3 _movePosition;
    [SerializeField] private Vector3 _rotatePosition;
    [SerializeField] private float _time = 2f;

    private void Start()
    {
        transform.DOLocalMove(_movePosition, _time).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalRotate(_rotatePosition, _time).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
