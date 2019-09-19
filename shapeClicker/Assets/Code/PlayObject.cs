using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayObject : MonoBehaviour
{

    private float rotationSpeed = 25;
    private Vector3 rotateAxis;
    private Vector3 moveDirection;

    private void SetRandomRotationDir()
    {
        rotateAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rotateAxis = rotateAxis.normalized;
    }

    private void SetRandomStartColor()
    {
        Renderer _rend = GetComponent<Renderer>();
        Color _color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        _rend = GetComponent<Renderer>();
        _rend.material.color = _color;
    }

    public virtual void Init(Vector3 _moveDirection)
    {
        SetRandomRotationDir();
        SetRandomStartColor();
        moveDirection = _moveDirection.normalized;
    }

    void Update()
    {
        transform.Rotate(rotateAxis * rotationSpeed * Time.deltaTime);
    }

    public void Die()
    {
        SpawnParticles();
        Destroy(gameObject);
    }

    protected virtual void SpawnParticles()
    {
        Renderer _rend = GetComponent<Renderer>();
        ParticleSystem _ps = Resources.Load("StandartParticle") as ParticleSystem;
        ParticleSystemRenderer _particleSystemRenderer = _ps.GetComponent<ParticleSystemRenderer>();
        _particleSystemRenderer.mesh = GetComponent<MeshFilter>().mesh;
        ParticleSystem.MainModule _psMain = _ps.main;
        _psMain.startColor = _rend.material.color;
        _ps.transform.localScale = transform.localScale;
        Destroy(Instantiate(_ps, transform.position, Quaternion.identity), _psMain.duration);
    }
}
