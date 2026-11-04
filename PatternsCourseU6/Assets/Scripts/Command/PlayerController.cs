using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody _rb;

    [Range(0,5)]
    public float moveDistance = 1.0f;

    [Range(5, 10)]
    public float jumpForce = 7.0f;



    private ICommand _commandForward; //Command for Wkey
    private ICommand _commandLeft; //Command for Akey
    private ICommand _commandBack; //Command for Skey
    private ICommand _commandRight; //Command for Dkey
    private ICommand _commandSpace; // Command for SpaceKey
    private ICommand _commandNothing;
    private ICommand _commandUndo;
    private ICommand _commandRedo;

    public ICommand previousStep;
    public ICommand nextStep;

    private void HandleInput()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            _commandForward.Execute();
            previousStep = _commandForward;
            nextStep = null;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            _commandLeft.Execute();
            previousStep = _commandLeft;
            nextStep = null;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            _commandBack.Execute();
            previousStep = _commandBack;
            nextStep = null;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            _commandRight.Execute();
            previousStep = _commandRight;
            nextStep = null;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _commandSpace.Execute();
            previousStep = _commandSpace;
            nextStep = null;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            InvertMovement();
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if(previousStep != null)
            {
                previousStep.Undo();
                nextStep = previousStep;
                previousStep = null;
            }
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            if(nextStep != null)
            {
                nextStep.Execute();
                previousStep = nextStep;
                nextStep = null;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _commandForward =new MoveForwardCommand(transform, moveDistance);

        _commandLeft = new MoveLeftCommand(transform, moveDistance);

        _commandBack = new MoveBackCommand(transform, moveDistance);

        _commandRight = new MoveRightCommand(transform, moveDistance);

        _commandSpace = new JumpCommand(_rb, jumpForce);

        _commandNothing = new NothingCommand();

        _commandUndo = null;
        _commandRedo = null;
    }

    void InvertMovement()
    {
        Debug.Log("INVERTED");
        // Switch the movement commands
        (_commandForward, _commandBack) = (_commandBack, _commandForward);
        (_commandLeft, _commandRight) = (_commandRight, _commandLeft);
    }

    // Update is called once per frame
    void Update()
    {
        //Check and handle input => Move Player
        HandleInput();
    }
}
