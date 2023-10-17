using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.Pool;


namespace Restaurant
{
    internal class RestaurantManager : MonoBehaviour
    {
#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        [Header("Classes")]

        [Tooltip("Load the GameManager class, which is the game manager, into this variable!")]
        [SerializeField] private GameManager gameManager;




        [Space(10f), Header("Restaurant Details")]


        [Tooltip("Restoranttaki tum sandalyeleri bu diziye ekleyin!")]
        [SerializeField] private Chair[] chairs;


        


        [Header("Object Pool")]

        
        // This system will greatly optimize your gameplay!
        [Tooltip("Enter how many customers you want to have on stage at the same time!")]
        [SerializeField] private int maxCharacter;
        
  







        [Space(15f), Header("Customer Details")]

        [Tooltip("Add all the customer models in the game to the series!")]
        [SerializeField] private GameObject[] CustomerPrefabs;

        [Tooltip("Add the Transform component from which customers will be created!")]
        [SerializeField] private Spawner[] customerSpawners = new Spawner[2];


        [Tooltip("Choose how often to create customers!")]
        [SerializeField] [Range(0, 15)] private float customerSpawnerDuration = 2;


        [Tooltip("Do you allow the creation of customers?")]
        [SerializeField] private bool spawnerIsActive = true;








    #region Serialize Fields Cool Down Timers
        [Space(15f), Header("Cool Down Timers")]

        [Tooltip("The time the customer waits for the waiter after sitting at the table!")]
        [SerializeField] [Range(2, 120)] private ushort customerWaitWaiter = 45;


        [Tooltip("Waiting time for a waiter while taking an order from a customer")]
        [SerializeField] [Range(0, 15)] private ushort whenOrdering = 5;


        [Tooltip("Waiting time for customer's order!")]
        [SerializeField] [Range(5, 180)] private ushort waitsOrder = 40;


        [Tooltip("Time for the customer to eat the food!")]
        [SerializeField] [Range(0, 60)] private ushort waitEatingCustomer = 10;
    #endregion

        

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||







#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| PRIVATE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        private bool _isSpawn = true;



        // All objects in this Queue List will be Instantiated once at the beginning of the game, so the instantiate method will not be used in the middle of the game!"
        private Queue<GameObject> pooledCharacters;

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||







#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| PROPERTIES ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        internal Spawner[] _customerSpawners
        {
            get { return this.customerSpawners; }
        }

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||















        /// <summary>
        /// With this method, it will be checked whether there is an empty chair on the stage.
        /// </summary>
        /// <returns>If there are empty chairs, a chair class will be returned. If there are no empty chairs, a null value will be returned!</returns>
        internal Chair SearchNullChair()
        {
            foreach (Chair chair in this.chairs)
            {
                if (chair.chairIsFull == false)
                {
                    return chair;
                }
                else
                {
                    continue;
                }
            }

            return null;
        }















        /// <summary>
        /// Counter function that will continuously create customers at certain time intervals and until the admin allows it.
        /// </summary>
        /// <returns></returns>
        internal IEnumerator CustomerCreationCycle()
        {
            while(this.spawnerIsActive && this._isSpawn)
            {
                this._isSpawn = false;
                SpawnCustomer();
                yield return new WaitForSeconds(this.customerSpawnerDuration);
                this._isSpawn = true;
                
            }

        }



        /// <summary>
        /// You can create a random customer with this method!
        /// </summary>
        /// <returns>It will return the class of type GameObject created!</returns>
        internal void SpawnCustomer()
        {
            // ~~ Variables ~~
            GameObject _createObject;
            CustomerAI _customerComponent;
            int _randomIndex;


            _randomIndex = Random.Range(0, this.CustomerPrefabs.Length);

            
            _createObject = GetPooledCharacter();
            _customerComponent = _createObject.GetComponent<CustomerAI>();
            _customerComponent._gameManager = this.gameManager;

            _customerComponent.AIControl();

        }







    #region Object Pool Functions


        /// <summary>
        /// With this method, character models are transferred to the pool created and 
        /// the next element is drawn from the list instead of calling an instantiate method in-game!
        /// </summary>
        internal void PoolLoad()
        {
            // ~~ Variables
            GameObject _createObject;
            GameObject _selectCustomerPrefab;
            Spawner _selectCustomerSpawner;
            CustomerAI _customerComponent;


            pooledCharacters = new Queue<GameObject>();

            for (int i = 0; i < this.maxCharacter; i++)
            {
                _selectCustomerPrefab = this.CustomerPrefabs[Random.Range(0, this.CustomerPrefabs.Length)];
                _selectCustomerSpawner = this.customerSpawners[Random.Range(0, this.customerSpawners.Length)];

                _createObject = Instantiate(_selectCustomerPrefab, _selectCustomerSpawner.transform.position, Quaternion.identity);
                _customerComponent = _createObject.GetComponent<CustomerAI>();
                _customerComponent.SetCustomerCrateSpawner(_selectCustomerSpawner);
  
                
                this.pooledCharacters.Enqueue(_createObject);
            }
        }







        /// <summary>
        /// With this method, the next object is drawn from the character pool and removed from the pool!
        /// </summary>
        /// <returns>The object removed from the pool is returned!</returns>
        internal GameObject GetPooledCharacter()
        {
            // ~~ Variables ~~
            GameObject _obj;

            _obj = this.pooledCharacters.Dequeue();     // The first element in the pool will be drawn and removed from the pool!
            _obj.SetActive(true);               // Your object will be activated!

            return _obj;                            // The object taken out of the pool will be returned!
        }



        /// <summary>
        /// With this method, the element removed from the pool is added back to the pool,
        /// but this time it moves to the back row!
        /// </summary>
        /// <param name="obj">Enter the element you want to add to the pool as a parameter!</param>
        internal void AddPooledCharacter(GameObject obj)
        {
            obj.SetActive(false);               // The object will be disabled!
            this.pooledCharacters.Enqueue(obj); // The object entered as a parameter will be added to the back row of the pool!
        }

    #endregion
    }
}