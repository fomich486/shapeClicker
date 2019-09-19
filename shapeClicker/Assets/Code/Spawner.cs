using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform start;
    private Transform final;

    public Spawner(Transform _start, Transform _final)
    {
        start = _start;
        final = _final;
    }

    public int[] shapes = new int[3] { (int)PrimitiveType.Sphere, (int)PrimitiveType.Cube, (int)PrimitiveType.Cylinder };
    public virtual GameObject GetGameObject(float speed)
    {
        GameObject _g;
        int _rand = Random.Range(0, shapes.Length);
        _g = GameObject.CreatePrimitive((PrimitiveType)_rand);
        _g.transform.position = start.position;
        _g.transform.localScale = start.localScale;
        //scale

        Vector3 _direction = final.position - start.position;
        PlayObject _playObject = _g.AddComponent(typeof(PlayObject)) as PlayObject;
        _playObject.Init(_direction.normalized,speed);
        return _g;
    }
}
