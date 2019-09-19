using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPos;
    [SerializeField]
    private Vector3 finalPos;

    public int[] shapes = new int[3] { (int)PrimitiveType.Sphere, (int)PrimitiveType.Cube, (int)PrimitiveType.Cylinder };
    public virtual GameObject GetGameObject()
    {
        GameObject _g;
        int _rand = Random.Range(0, shapes.Length);
        _g = GameObject.CreatePrimitive((PrimitiveType)_rand);
        _g.transform.position = startPos;
        //scale

        Vector3 _direction = finalPos - startPos;
        PlayObject _playObject = _g.AddComponent(typeof(PlayObject)) as PlayObject;
        _playObject.Init(_direction.normalized);
        return _g;
    }
}
