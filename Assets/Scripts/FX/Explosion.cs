using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioClip audioClip;

    private void Awake()
    {
        FindAnyObjectByType<SFXManager>().PlaySFX(SFX_Type.Explosion, audioClip, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!particleSystem.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
