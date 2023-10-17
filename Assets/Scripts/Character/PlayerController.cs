using UnityEngine;



namespace Character
{
    internal class PlayerController : CharacterProperties
    {
#region ||~~~~~~~~~~~~~|| X ||~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~|| X ||~~~~~~~~~~~~~||

        [Header("Class And Component")]


        [Tooltip("Add the GameManager class here!")]
        [SerializeField] private GameManager gameManager;



        [Tooltip("Add the CharacterController component here!")]
        [SerializeField] private CharacterController characterController;




        [Header("UI Objects")]


        [Tooltip("Add the Interaction button image that will open on the player here!")]
        [SerializeField] private GameObject interactionCanvas;


#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||






#region ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| PRIVATE FIELDS ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||

        private bool _interaction = false;

#endregion ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~||||~~~~~~~~~~~~~||






        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Interaction"))
            {
                this.interactionCanvas.SetActive(true);
                this._interaction = true;
            }
        }




        private void OnTriggerStay(Collider other) 
        {
            Debug.Log(message: $"Interaction: {this._interaction}");
            if (this._interaction && Input.GetKey(KeyCode.E))
            {
                Debug.Log("E basildi!");
                this.interactionCanvas.SetActive(false);
                this._interaction = false;
            }
            
        }



        private void OnTriggerExit(Collider other) {
            this.interactionCanvas.SetActive(false);
            this._interaction = false;
        }



        














        /// <summary>
        /// With this method, the character will move!
        /// </summary>
        internal override void Move()
        {
            if (this.isMove)
            {
                // ~~ Variables ~~
                float _horizontalMove, _verticalMove;

                _horizontalMove = Input.GetAxis("Horizontal") * this.CharacterSpeed * Time.deltaTime;
                _verticalMove = Input.GetAxis("Vertical") * this.CharacterSpeed * Time.deltaTime;

                // this.transform.Translate(new Vector3(_horizontalMove, 0, _verticalMove));
                this.characterController.Move(new Vector3(_horizontalMove, 0, _verticalMove));
            }
        }








        /// <summary>
        /// You can protect the game against errors by running this method when the game starts!
        /// </summary>
        /// <param name="gameManager">GameManager sinifini parametre olark girerek islem yukunu azaltabilirsiniz!</param>
        internal void GameStartSettings(GameManager gameManager)
        {
            this.interactionCanvas.SetActive(false);
            this.isMove = true;

            if (this.gameManager == null)
            {
                this.gameManager = gameManager;
            }
            else if (gameManager == null)
            {
                this.gameManager = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<GameManager>();
            }
        }


    }
}