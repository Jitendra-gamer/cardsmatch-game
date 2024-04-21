using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CardMatch.UI
{
    public class PopUpController : MonoBehaviour
    {
        public GameObject popupGameObject;
        [SerializeField] private TMP_Text messageText;

        public void ShowPopUp(string message)
        {
            messageText.text = message;
            popupGameObject.SetActive(true);
        }

        public void HidePopUp()
        {
            popupGameObject.SetActive(false);
        }
    }
}
