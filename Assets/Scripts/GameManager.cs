using Character;
using mesut_atakan;
using Restaurant;
using UnityEngine;



internal class GameManager : MonoBehaviour
{
#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

    [Header("Classes")]
    
    [Tooltip("Get Player Controller class")]
    [SerializeField] private PlayerController playerController;


    [Tooltip("Get Camera Controller")]
    [SerializeField] private CameraController cameraController;


    [Tooltip("Add To RestaurantManager class")]
    [SerializeField] private RestaurantManager restaurantManager;

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||








#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| PROPERTIES ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

    internal RestaurantManager _restaurantManager
    {
        get { return this.restaurantManager; }
        set { restaurantManager = value; }
    }

#endregion













    private void Awake() 
    {
        if (this.playerController == null)
            this.playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (this.cameraController == null)
            this.cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        if (this.cameraController._targetTransform == null)
            this.cameraController._targetTransform = playerController.transform;





        this.restaurantManager.PoolLoad();
    }




    private void Start() 
    {
        
    }





    private void FixedUpdate() 
    {
        
    }





    private void Update() 
    {
        this.playerController.Move();
    }








    
    private void LateUpdate() 
    {
        this.cameraController.CameraMovement();


        StartCoroutine(this._restaurantManager.CustomerCreationCycle());
    }













}