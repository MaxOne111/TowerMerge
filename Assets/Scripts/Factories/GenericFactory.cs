using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : Object
{
    [SerializeField] protected T _Prefab;

    public virtual T Create(Vector3 _position, Quaternion _rotation) => Instantiate(_Prefab, _position, _rotation);
    public virtual T Create(T _prefab, Vector3 _position, Quaternion _rotation) => Instantiate(_prefab, _position, _rotation);
    public virtual T Create(Vector3 _position, Quaternion _rotation, Transform _parent) => Instantiate(_Prefab, _position, _rotation, _parent);
}