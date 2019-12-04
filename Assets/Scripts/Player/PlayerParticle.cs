using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    public static PlayerParticle Instance { get; private set; }

    public ParticleSystem castParticle;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        Init(castParticle);
    }

    void Init(ParticleSystem particle)
    {
        particle.Stop();
        var main = particle.main;
        main.useUnscaledTime = true;
    }

    public void PlayCastParticle()
    {
        castParticle.Play();
    }

    public void PauseCastParticle()
    {
        castParticle.Stop();
    }
}
