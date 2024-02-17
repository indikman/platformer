using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace Spatialminds.Platformer
{
    [CreateAssetMenu(menuName ="Platformer/CharacterData", fileName ="CharacterDataScriptableObject")]
    public class CharacterDataScriptableObject : ScriptableObject
    {
        public AnimatorController characterAnimatorController;

        public string[] animParams;

        public Dictionary<string, int> animatorParameters = new Dictionary<string, int>();

        public void UpdateAnimatorParameters()
        {
            foreach (var anim in animParams)
            {
                if(animatorParameters.TryAdd(anim, Animator.StringToHash(anim))) Debug.Log("Added param " + anim);
                else Debug.LogWarning("Param " + anim + " already exists. Not adding it again!");
            }

            Debug.Log("All the Params added");
        }

        void OnEnable()
        {
            UpdateAnimatorParameters();
        }

        public void ClearAnimParams()
        {
            animatorParameters.Clear();
        }
    }
}
