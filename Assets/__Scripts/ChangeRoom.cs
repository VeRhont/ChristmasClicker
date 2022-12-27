using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ChangeRoom : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Vector3 _cameraRoomPosition;
    [SerializeField] private Vector3 _cameraOutsidePosition;
    [SerializeField] private GameObject _roomCanvas;
    [SerializeField] private GameObject _outsideCanvas;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime;

    private Camera _camera;
    private bool _isOutside = false;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void ChangeCameraPosition()
    {
        StartCoroutine(ChangePosition());
    }

    private IEnumerator ChangePosition()
    {
        // _animator.SetTrigger("Transition");

        yield return new WaitForSeconds(_transitionTime);

        if (_isOutside)
        {
            _camera.transform.position = _cameraRoomPosition;
        }
        else
        {
            _camera.transform.position = _cameraOutsidePosition;
        }

        _roomCanvas.SetActive(_isOutside);
        _outsideCanvas.SetActive(!_isOutside);
        _isOutside = !_isOutside;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Ok");
        ChangeCameraPosition();
    }
}
