using UnityEngine;

public class MoveRightCommand : ICommand
{
    private float _amount;
    private Transform _transform;

    public MoveRightCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = amount;
    }

    public void Execute()
    {
        Debug.Log("Moved Right");
        _transform.Translate(_amount * Vector3.right, Space.World);
    }

    public void Undo()
    {
        Debug.Log("Undid Right");
        _transform.Translate(-_amount * Vector3.right, Space.World);
    }
}
