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
        [SerializeField] private float highJumpMultiplier = 2f;
        [SerializeField] private bool clampFallSpeed = false;
        [SerializeField] private float fallSpeedClamp = 50f;


        public float horizontal {get; private set;}
        bool isJumpPressed;
        public Rigidbody2D characterRB {get; private set;}
        Vector2 direction;
        bool facingRight = true;
        public CharacterStateManager characterStateManager {get; private set;}
        [SerializeField] private CharacterAnimatorManager characterAnim;
        public CharacterAnimatorManager CharacterAnim() => characterAnim;
        

        public bool isGround { get; private set; }

        void Awake()
        {
            characterRB = GetComponent<Rigidbody2D>();
            characterStateManager = new CharacterStateManager(this);
        }

        public virtual void Start()
        {
            characterStateManager.Initialize(characterStateManager.idleState);
        }

        public virtual void Update()
        {
            CheckIsGround();
            UpdateCharacterDirection();
            UpdateFall();

            characterStateManager.Update();
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        internal void SetHorizontal(float x) => horizontal = x;

        internal void SetJumpPressed(bool value) => isJumpPressed = value;

        public void MovePlayer()
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
                //characterRB.velocity = new Vector3(characterRB.velocity.x, Mathf.Clamp(characterRB.velocity.y, ))
            }
            else if (characterRB.velocity.y > 0 && !isJumpPressed) // When we are jumping up but has released the jump button.
            {
                characterRB.velocity += (lowJumpMultiplier - 1) * Physics2D.gravity.y * Vector2.up * Time.deltaTime;
            }else if(characterRB.velocity.y > 0 && isJumpPressed)
            {
                characterRB.velocity += (highJumpMultiplier - 1) * Physics2D.gravity.y * Vector2.up * Time.deltaTime;
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
