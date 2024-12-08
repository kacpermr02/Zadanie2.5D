using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    public int totalItemsToCollect; 
    private int itemsInDropZone; 
    public Collider2D dropZone;

    void Update()
    {
        if (itemsInDropZone >= totalItemsToCollect)
        {
            LoadLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            itemsInDropZone++;
            Debug.Log($"Przedmiot umieszczony w strefie. Liczba: {itemsInDropZone}/{totalItemsToCollect}");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            itemsInDropZone--;
            Debug.Log($"Przedmiot usunięty ze strefy. Liczba: {itemsInDropZone}/{totalItemsToCollect}");
        }
    }

    void LoadLevel()
    {
        Debug.Log("Wszystkie przedmioty zebrane! Ładowanie następnego poziomu...");
    }
}
