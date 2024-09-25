using UnityEngine;

public class FinishDay : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject _objectBed;
    [SerializeField] private GameObject _dialogSprite;
    [SerializeField] private GameObject _canvasGoodEnding;
    [SerializeField] private GameObject _canvasBadEnding;

    private int ganar = 3;
    private bool _isActive;

    public void Interactuar(PlayerController playerController)
    {
        if (_isActive)
        {
            if (ganar >= 3)
            {
                ActiveTrueFalse.ActiveTrue(_canvasGoodEnding);
            }
            else
            {
                ActiveTrueFalse.Activefalse(_canvasBadEnding);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActiveTrueFalse.ActiveTrue(_dialogSprite);
            _isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ActiveTrueFalse.Activefalse(_dialogSprite);
            _isActive = false;
        }
    }


}
