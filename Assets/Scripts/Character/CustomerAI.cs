using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Restaurant;


namespace Character
{
    internal class CustomerAI : MonoBehaviour
    {
#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        [Header("Classes")]

        [Tooltip("Oyun yoneticisi olan gameManager sinifini buraya atayiniz!")]
        [SerializeField] private GameManager gameManager;



        [Header("Components")]

        [Tooltip("Add To NavMeshAgent Component")]
        [SerializeField] private NavMeshAgent ai;



        [Header("UI Objects")]

        [Tooltip("Add the image component that shows the customer's waiting time!")]
        [SerializeField] private Image coolDownTimeImage;




        [Header("Event")]

        [Tooltip("Save your project from getting tired with this Event system!")]
        [SerializeField] private UnityEvent customerEvent;

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||












#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| PRIVATE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        private bool _isCustomer = false;


        private GameObject thisPoolGameObject;


        private const float _destroyTargetPositonDistance = 3; 



        

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||












#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| PROPERTIES ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        internal GameManager _gameManager
        {
            get {return this.gameManager; }
            set { this.gameManager = value; }
        }



        internal GameObject _thisPoolGameObject
        {
            get { return this.thisPoolGameObject; }
            set { this.thisPoolGameObject = value; }
        }


        internal UnityEvent _customerEvent
        {
            get { return this.customerEvent; }
            set { this.customerEvent = value; }
        }


        internal Spawner startSpawner { get; private set; }

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||




        internal void AIControl() {
            // ~~ Variables ~~
            Transform _targetTransform;

            IsCustomer();
            if (this._isCustomer)
            {
                ai.destination = this.gameManager._restaurantManager.SearchNullChair().AddCustomer(this).chairTransform.position;
                _targetTransform = null;
                return;
            }
            else
            {
                if (Vector3.Distance(this.transform.position, this.gameManager._restaurantManager._customerSpawners[0].transform.position) > Vector3.Distance(this.transform.position, this.gameManager._restaurantManager._customerSpawners[1].transform.position))
                {
                    _targetTransform = this.gameManager._restaurantManager._customerSpawners[0].transform;
                }
                else
                {
                    _targetTransform = this.gameManager._restaurantManager._customerSpawners[1].transform;
                }


                ai.destination = _targetTransform.position;
                return;
            }
        }






        /// <summary>
        /// With this method, we will find out which spawner this customer was born from!
        /// </summary>
        /// <param name="spawnerTransform">Enter as a parameter which spawner the customer was created in!</param>
        internal void SetCustomerCrateSpawner(Spawner spawner)
        {
            this.startSpawner = spawner;
        }






        /// <summary>
        /// With this method, you will add this object to the pool again!
        /// </summary>
        /// <param name="targetPosition">Enter the position the character will follow!</param>
        private void DestroyThisCharacter(Vector3 targetPosition)
        {
            if (Vector3.Distance(this.transform.position, targetPosition) <= _destroyTargetPositonDistance)
                this.gameManager._restaurantManager.AddPooledCharacter(this.gameObject);
        }









        /// <summary>
        /// With this method, we will check whether this person is a restaurant customer or not!
        /// </summary>
        private void IsCustomer()
        {
            // ~~ variables ~~
            this._isCustomer = (Random.Range(0,2) == 0) && this.gameManager._restaurantManager.SearchNullChair();
        }
        
    }
}