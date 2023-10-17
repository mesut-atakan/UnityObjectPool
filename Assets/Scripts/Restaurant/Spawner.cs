using Character;
using UnityEngine;



namespace Restaurant
{
    internal class Spawner : MonoBehaviour
    {

#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        [Tooltip("Add the game manager `GameManager` class here!")]
        [SerializeField] private GameManager gameManager;


        

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||









        private void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag("Customer"))
            {
                CustomerAI _customerComponent;
                _customerComponent = other.GetComponent<CustomerAI>();

                if (_customerComponent.startSpawner != this)
                {
                    _customerComponent.gameObject.SetActive(false);
                    _customerComponent.transform.position = _customerComponent.startSpawner.transform.position;
                    this.gameManager._restaurantManager.AddPooledCharacter(other.gameObject);
                }
            }
        }
    }
}