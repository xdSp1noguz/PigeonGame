using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float momentumDamping = 5f;
    private CharacterController myCC;
    public Animator camAnim;
    private bool isWalking;
    private Vector3 inputVector;
    private Vector3 movementVector;
    private float myGravity = -10f; // Пока отключено, всё верно

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Получаем ссылку на компонент
        myCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();


        camAnim.SetBool("isWalking", isWalking);
    }

    void GetInput()
    {
        if(Input.GetKey(KeyCode.W) || 
           Input.GetKey(KeyCode.A) ||
           Input.GetKey(KeyCode.S) ||
           Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
       else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);
            
            isWalking = false;
        }
        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }
    
    void MovePlayer() 
    {
        // Вычисляем итоговое движение
        movementVector = inputVector * playerSpeed;

        // Передаем команду контроллеру
        myCC.Move(movementVector * Time.deltaTime);
    }
}
