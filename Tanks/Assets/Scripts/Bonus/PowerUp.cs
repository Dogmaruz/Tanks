using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FP_CharacterController character = collision.transform.root.GetComponent<FP_CharacterController>();

        if (character != null)
        {
            OnPickedUp(character);

            Destroy(gameObject);
        }
    }
    protected abstract void OnPickedUp(FP_CharacterController character);
}
