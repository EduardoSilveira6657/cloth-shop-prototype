using UnityEngine;

namespace _Project.Scripts.Runtime.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float movementSpeedMultiplier = 5f;
        [SerializeField] Rigidbody2D playerRigidBody;
        [SerializeField] Animator playerAnimator;
        [SerializeField] GameObject sideWaysModel;
        [SerializeField] GameObject upWayModel;
        [SerializeField] GameObject downWayModel;
    
        Vector2 _movementInput;
        int _verticalInputParam;
        int _horizontalInputParam;
        int _movementSpeedParam;

        void Awake()
        {
            _verticalInputParam = Animator.StringToHash("Vertical Input");
            _horizontalInputParam = Animator.StringToHash("Horizontal Input");
            _movementSpeedParam = Animator.StringToHash("Movement Speed");
        }

        void Update()
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
        
            if(_movementInput.y > 0.1f) {
                sideWaysModel.SetActive(false);
                upWayModel.SetActive(true);
                downWayModel.SetActive(false);
            } else if(_movementInput.y < -0.1f) {
                sideWaysModel.SetActive(false);
                upWayModel.SetActive(false);
                downWayModel.SetActive(true);
            } else {
                sideWaysModel.SetActive(true);
                upWayModel.SetActive(false);
                downWayModel.SetActive(false);
            }

            playerAnimator.SetFloat(_verticalInputParam, _movementInput.x);
            playerAnimator.SetFloat(_horizontalInputParam, _movementInput.y);
            playerAnimator.SetFloat(_movementSpeedParam, _movementInput.sqrMagnitude);
        }
    
        void FixedUpdate()
        {
            playerRigidBody.MovePosition(playerRigidBody.position + _movementInput * movementSpeedMultiplier * Time.fixedDeltaTime);
        }
    }
}
