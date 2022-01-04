using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private Transform turretPosition;


    private void MovePlayer(Vector2 inputMovement)
    {
        var playerTransform = transform;
        var position = playerTransform.position;
        if (!((position.x + inputMovement.x) > (maxDistance * -1)) ||
            !((position.x + inputMovement.x) < maxDistance)) return;
        position = new Vector3(position.x + inputMovement.x, position.y, 0);
        playerTransform.position = position;
    }

    private void PlayerShoot()
    {
        var bullet = ObjectPooling.SharedInstance.GetPooledObject();
        if (bullet == null) return;
        bullet.transform.position = turretPosition.position;
        bullet.SetActive(true);
    }
    
    public void Shoot(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        PlayerShoot();
    }
    
    
    public void Move(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var inputMovement = context.ReadValue<Vector2>();
        MovePlayer(inputMovement);
    }
    

}
