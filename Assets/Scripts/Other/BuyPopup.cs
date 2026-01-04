using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyPopup : MonoBehaviour
{
    public GameObject panel;

    public void OpenPopup()
    {
        panel.SetActive(true);
        Debug.Log("Popup aperto.");

    }

    public void ClosePopup()
    {
        panel.SetActive(false);
        
    }

    public void ConfirmBuy()
    {
        // Buscar el texto del precio
        GameObject priceTextObj = GameObject.Find("text_price");
        if (priceTextObj != null)
        {
            TMP_Text priceText = priceTextObj.GetComponent<TMP_Text>();
            if (priceText != null)
            {
                // Intentar convertir el texto a int, removiendo el símbolo $
                string priceString = priceText.text.Replace("$", "").Trim();
                if (int.TryParse(priceString, out int price))
                {
                    // Intentar quitar el dinero
                    if (MoneyManager.Instance.quitarDinero(price))
                    {
                        Debug.Log("Compra exitosa! Dinero restante: " + MoneyManager.Instance.GetMoney());
                        // Coger el nombre del ingrediente del popup
                        GameObject ingredientNameObj = GameObject.Find("text_name_ingredients");
                        if (ingredientNameObj != null)
                        {
                            TMP_Text ingredientNameText = ingredientNameObj.GetComponent<TMP_Text>();
                            if (ingredientNameText != null)
                            {
                                string ingredientName = ingredientNameText.text;
                                Debug.Log("Ingrediente comprado: " + ingredientName);

                                // Apuntamos el ingrediente como comprado
                                BoughtIngredientTracker.Instance.AddIngredient(ingredientName);
                            }
                        }

                        ClosePopup();
                    }
                    else
                    {
                        Debug.Log("No tienes suficiente dinero. Precio: " + price + ", Dinero: " + MoneyManager.Instance.GetMoney());
                    }
                }
                else
                {
                    Debug.LogError("El texto del precio no es un número válido: " + priceText.text);
                }
            }
            else
            {
                Debug.LogError("No se encontró componente TMP_Text en text_price");
            }
        }
        else
        {
            Debug.LogError("No se encontró GameObject llamado text_price");
        }
    }
}
