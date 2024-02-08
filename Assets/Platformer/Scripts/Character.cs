using UnityEngine;

namespace Spatialminds.Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed;

        [Header("Jump")]
        [SerializeField] private float jumpVelocity;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private Vector2 groundCheckOrigin;
        [SerializeField] private LayerMask groundLayer;

        [Header("Falling")]
        [SerializeField] private float fallMultiplier = 2.5f;
        [SerializeField] private float lowJumpMultiplier = 2f;

        float horizontal;
        bool isJumpPressed;
        Rigidbody2D characterRB;
        Vector2 direction;
        bool facingRight = true;
        

        public bool isGround { get; private set; }

        public virtual void Start()
        {
            characterRB = GetComponent<Rigidbody2D>();
        }

        public virtual void Update()
        {
            CheckIsGround();
            UpdateCharacterDirection();
            UpdateFall();
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        internal void SetHorizontal(float x) => horizontal = x;

        internal void SetJumpPressed(bool value) => isJumpPressed = value;

        internal void MovePlayer()
        {
            direction = transform.right * horizontal;
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, moveSpeed * Time.deltaTime);
        }

        internal void Jump()
        {
            if (isGround)
            {
                characterRB.velocity += Vector2.up * jumpVelocity;
            }
        }

        void CheckIsGround()
        {
            isGround = Physics2D.Raycast((Vector2)transform.position + groundCheckOrigin, Vector2.down, groundCheckDistance, groundLayer);
        }

        void UpdateFall()
        {
            // This means the player is falling
            if (characterRB.velocity.y < 0)
            {
                characterRB.velocity += (fallMultiplier - 1) * Physics2D.gravity.y * Vector2.up * Time.deltaTime;
            }
            else if (characterRB.velocity.y > 0 && !isJumpPressed) // When we are jumping up but has released the jump button.
            {
                characterRB.velocity += (lowJumpMultiplier - 1) * Physics2D.gravity.y * Vector2.up * Time.deltaTime;
                Debug.Log("Jump is not pressed");
            }
        }

        void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        void UpdateCharacterDirection()
        {
            if (facingRight == false && horizontal > 0)
            {
                Flip();
            }
            else if (facingRight == true && horizontal < 0)
            {
                Flip();
            }
        }
    }
}
