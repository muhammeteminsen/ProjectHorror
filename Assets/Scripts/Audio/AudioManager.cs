using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> floorFootsteps;
    [SerializeField] private List<AudioClip> grassFootsteps;
    [SerializeField] private List<AudioClip> metalFootsteps;
    private AudioSource _audioSource;
    private PlayerMovement _playerMovement;
    private int _previousSound;
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        RaycastController();
    }

    private void PlayAudio(List<AudioClip> sound,float volume)
    {
        if (_playerMovement.InputVector.x != 0 || _playerMovement.InputVector.y != 0)
        {
            if (_audioSource.isPlaying && _audioSource!=null)return;
            int currentSound = Random.Range(0, sound.Count);
            if (_previousSound == currentSound) currentSound = (currentSound + 1) % sound.Count;
            _previousSound = currentSound;
            _audioSource.clip = sound[currentSound];
            _audioSource.PlayOneShot(_audioSource.clip,volume); 
        }
    }

    private void RaycastController()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.down,out hit,10f))
        {
            Collider collision = hit.collider;
            switch (collision.sharedMaterial?.name)
            {
                case "Floor Material":
                    PlayAudio(floorFootsteps,1);
                    break;
                case "Grass Material":
                    PlayAudio(grassFootsteps,1);
                    break;
                case "Metal Material" :
                    PlayAudio(metalFootsteps,1);
                    break;
            }
            Debug.DrawRay(transform.position,Vector3.down*hit.distance,Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position,Vector3.down*1000,Color.gray);
        }
    }
}