using UnityEngine;

public class ObjectInteract : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _dialogSprite;

    private bool _isActive;

    public void Interactuar(PlayerController player)
    {
        if (_isActive)
        {
            transform.SetParent(player._playerClamp);
            transform.position = player._playerClamp.position;
            GetComponent<Rigidbody2D>().isKinematic = true;
            player._playerAnimator.SetBool("TakeObject", true);
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