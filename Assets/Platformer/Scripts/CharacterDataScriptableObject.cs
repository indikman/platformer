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

        //public string[] animParams;

        public Dictionary<string, int> animatorParameters = new Dictionary<string, int>();

        public void UpdateAnimatorParameters()
        {

            if(characterAnimatorController==null)
            {
                Debug.LogError("Animator controller not assigned");
                return;
            }

            foreach (var anim in characterAnimatorController.parameters)
            {
                if(animatorParameters.TryAdd(anim.name, anim.nameHash)) Debug.Log("Added param " + anim.name);
                else Debug.LogWarning("Param " + anim.name + " already exists. Not adding it again!");
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
