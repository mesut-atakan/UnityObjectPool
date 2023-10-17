using UnityEngine;



namespace Character
{
    internal abstract class CharacterProperties : MonoBehaviour
    {
#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        [Header("Character Main Properties")]

        [Tooltip("Enter the character's speed")]
        [SerializeField] protected float CharacterSpeed;



        [Header("Private Properties")]
        
        [Tooltip("Check whether the character can move or not with this boolean variable!")]
        [SerializeField] protected bool isMove = true;

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||




        /// <summary>
        /// With this method you can make your character move!
        /// </summary>
        internal abstract void Move();
    }
}