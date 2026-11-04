using UnityEngine;

public class JumpCommand : ICommand
{
    private Rigidbody _rb;
    private float _jumpForce;

    public JumpCommand(Rigidbody rb, float jumpForce)
    {
        _rb = rb;
        _jumpForce = jumpForce;
    }

    public void Execute()
    {
        Debug.Log("Jumped");
        _rb.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
    }

    public void Undo()
    {
        Debug.Log("Undid the jump");
        _rb.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
    }
}
