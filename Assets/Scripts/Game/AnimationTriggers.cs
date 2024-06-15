using Game.States;
using UnityEngine;

namespace Game
{
    public class AnimationTriggers : MonoBehaviour
    {
        //todo change to abstract class
        private PlayerCharacter _playerCharacter;

        private void Awake()
        {
            _playerCharacter = GetComponent<PlayerCharacter>();
        }

        private void AnimationFinishTrigger() => _playerCharacter.AnimationFinishTrigger();
    }
}