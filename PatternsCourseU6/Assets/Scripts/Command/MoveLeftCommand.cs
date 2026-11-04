using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private float _amount;
    private Transform _transform;

    public MoveLeftCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = amount;
    }

    public void Execute()
    {
        Debug.Log("Moved Left");
        _transform.Translate(_amount * Vector3.left, Space.World);
    }

    public void Undo()
    {
        Debug.Log("Undid Left");
        _transform.Translate(-_amount * Vector3.left, Space.World);
    }
}
