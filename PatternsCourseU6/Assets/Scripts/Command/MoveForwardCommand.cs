using UnityEngine;

public class MoveForwardCommand : ICommand
{
    private float _amount;
    private Transform _transform;

    public MoveForwardCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = amount;
    }

    public void Execute()
    {
        Debug.Log("Moved FRW");
        _transform.Translate(_amount * Vector3.forward, Space.World);
    }

    public void Undo()
    {
        Debug.Log("Undid FRW");
        _transform.Translate(-_amount * Vector3.forward, Space.World);
    }
}
