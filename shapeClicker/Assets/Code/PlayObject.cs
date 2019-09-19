using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayObject : MonoBehaviour,IPointerClickHandler
{
    Rigidbody rb;
    private float rotationSpeed = 25;
    private Vector3 rotateAxis;
    private Vector3 moveDirection;

    public float Speed {
        set
        {
            rb.velocity = value * moveDirection;
        }
    }

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

    public virtual void Init(Vector3 _moveDirection,float _speed)
    {
        SetRandomRotationDir();
        SetRandomStartColor();
        moveDirection = _moveDirection.normalized;
        rb = gameObject.AddComponent<Rigidbody>();
        rb.velocity = _speed * moveDirection;
        rb.useGravity = false;
    }

    void Update()
    {
        transform.Rotate(rotateAxis * rotationSpeed * Time.deltaTime);
    }

    public virtual void Die()
    {
        SpawnParticles();
        GameController.Instance.Score++;
        DestroyThisObject();
    }

    protected virtual void SpawnParticles()
    {
        Renderer _rend = GetComponent<Renderer>();
        GameObject _g = Resources.Load("StandartParticle") as GameObject;
        ParticleSystem _ps =_g.GetComponent<ParticleSystem>();
        ParticleSystemRenderer _particleSystemRenderer = _ps.GetComponent<ParticleSystemRenderer>();
        _particleSystemRenderer.mesh = GetComponent<MeshFilter>().mesh;
        _particleSystemRenderer.material = GetComponent<Renderer>().sharedMaterial;
        ParticleSystem.MainModule _psMain = _ps.main;
        //_psMain.startSize = transform.localScale.x * 0.1f;
        _psMain.startColor = _rend.material.color;
        _ps.transform.localScale = transform.localScale;
        Destroy(Instantiate(_ps, transform.position, Quaternion.identity),2f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "DestroyPoint")
        {
            DestroyThisObject();
        }
    }

    private void DestroyThisObject()
    {
        GameController.Instance.DestroyObject(gameObject);
        Destroy(gameObject);
    }
}
