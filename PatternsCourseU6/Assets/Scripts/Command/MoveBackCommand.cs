using UnityEngine;

public class MoveBackCommand : ICommand
{
    private float _amount;
    private Transform _transform;

    public MoveBackCommand(Transform transform, float amount)
    {
        _transform = transform;
        _amount = amount;
    }

    public void Execute()
    {
        Debug.Log("Moved Back");
        _transform.Translate(_amount * Vector3.back, Space.World);
    }

    public void Undo()
    {
        Debug.Log("undid Back");
        _transform.Translate(-_amount * Vector3.back, Space.World);
    }
}
