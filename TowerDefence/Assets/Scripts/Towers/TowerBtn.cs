using UnityEngine;

public class TowerBtn : MonoBehaviour {

    [SerializeField]
    GameObject towerObject;
    [SerializeField]
    Sprite dragSprite;


    public GameObject GetTowerObject{
        get {
            return towerObject;//возвращает башню
        }
    }

    public Sprite GetDragSprite
    {
        get
        {
            return dragSprite;//возвращает картинку
        }
    }

}
