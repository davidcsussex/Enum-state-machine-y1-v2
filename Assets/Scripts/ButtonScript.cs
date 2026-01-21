using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    //public variable to reference the button text - set this up in the Unity editor
    public TMP_Text buttonText;

    public void ButtonMethod()
    {
        buttonText.text = "Clicked!";
    }

}
