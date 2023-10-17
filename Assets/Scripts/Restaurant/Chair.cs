using Character;
using Unity.VisualScripting;
using UnityEngine;



namespace Restaurant
{
    internal class Chair : MonoBehaviour
    {

#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        [Header("Transform")]
        
        [Tooltip("Add the transform of the chair here.")]
        [SerializeField] internal Transform chairTransform;

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||








#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| PROPERTY FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        internal bool chairIsFull { get; set; }

        internal CustomerAI customer { get; set; }


#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||













#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| Functions ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        /// <summary>
        /// You can add your customer to this chair with this method!
        /// </summary>
        /// <param name="customer">Enter the class of the customer you want to add as a parameter.</param>
        /// <returns>After adding the customer, the chair class will be returned!</returns>
        internal Chair AddCustomer(CustomerAI customer)
        {
            this.chairIsFull = true;
            this.customer = customer;
            return this;
        }




        /// <summary>
        /// With this method, you can indicate that your customer is no longer in the chair.
        /// </summary>
        /// <returns>This chair will be returned!</returns>
        internal Chair CleanCustomer()
        {
            this.chairIsFull = false;
            this.customer = null;
            return this;
        }

#endregion
    }
}