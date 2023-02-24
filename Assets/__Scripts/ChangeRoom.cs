using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ChangeRoom : MonoBehaviour, IPointerClickHandler
{
    public static ChangeRoom Instance;
    public static bool IsOutside = false;

    [SerializeField] private Vector3 _cameraRoomPosition;
    [SerializeField] private Vector3 _cameraOutsidePosition;
    [SerializeField] private GameObject _roomCanvas;
    [SerializeField] private GameObject _outsideCanvas;

    [SerializeField] private float _transitionTime;
    [SerializeField] private Animator _transitionAnimator;

    private Camera _camera;

    private void Awake()
    {
        Instance = this;
        _camera = Camera.main;

        IsOutside = false;
    }

    public void ChangeCameraPosition()
    {
        StartCoroutine(ChangeCameraPos());
    }

    private IEnumerator ChangeCameraPos()
    {
        _transitionAnimator.SetTrigger("Transition");

        yield return new WaitForSeconds(_transitionTime);

        if (IsOutside)
        {
            _camera.transform.position = _cameraRoomPosition;
        }
        else
        {
            _camera.transform.position = _cameraOutsidePosition;
        }

        _roomCanvas.SetActive(IsOutside);
        _outsideCanvas.SetActive(!IsOutside);
        IsOutside = !IsOutside;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeCameraPosition();
    }
}
