using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
    private static Transform FreeObject(Transform _pool)
    {
        if (_pool.childCount == 0)
        {
            return null;
        }
        
        for (int i = 0; i < _pool.childCount; i++)
        {
            if (_pool.GetChild(i))
            {
                return _pool.GetChild(i);
            }
        }

        return null;
    }
    
    private static T FreeObject<T>(List<T> _pool) where T : MonoBehaviour
    {
        if (_pool.Count == 0)
        {
            return null;
        }
        
        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i])
            {
                return _pool[i];
            }
        }

        return null;
    }

    public static void ReturnToPool(Transform _object, Transform _pool)
    {
        _object.SetParent(_pool);
        
        _object.gameObject.SetActive(false);
    }
    
    public static void ReturnToPool<T>(T _object, List<T> _pool) where T: MonoBehaviour
    {
        _pool.Add(_object);
        
        _object.gameObject.SetActive(false);
    }

    
    public static T PoolInstantiate<T>( T _prefab, Vector3 _position, Quaternion _rotation, List<T> _pool) where T : MonoBehaviour
    {
        T _free_Object = FreeObject(_pool);
        
        if (_free_Object)
        {
            _free_Object.transform.position = _position;

            _free_Object.transform.rotation = _rotation;
            
            _free_Object.gameObject.SetActive(true);
            
            _free_Object.transform.SetParent(null);

            _pool.Remove(_free_Object);

            return _free_Object;
        }

        T _new_Oblect = Object.Instantiate(_prefab, _position, _rotation);
        return _new_Oblect;
    }
    
    public static T PoolInstantiate<T>( T _prefab, Vector3 _position, Quaternion _rotation, Transform _pool) where T : MonoBehaviour
    {
        if (FreeObject(_pool))
        {
            FreeObject(_pool).gameObject.SetActive(true);
            
            FreeObject(_pool).position = _position;
            
            FreeObject(_pool).SetParent(null);

            return null;
        }

        T _new_Oblect = Object.Instantiate(_prefab, _position, _rotation);
        return _new_Oblect;
    }
}