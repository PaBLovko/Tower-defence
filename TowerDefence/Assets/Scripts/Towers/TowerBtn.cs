using UnityEngine;

public class TowerBtn : MonoBehaviour {

    [SerializeField]
    GameObject towerObject;
    [SerializeField]
    Sprite dragSprite;


    public GameObject TowerObject{
        get {

            return towerObject;//возвращает башню
        }
    }

    public Sprite DragSprite
    {
        get
        {

            return dragSprite;//возвращает картинку
        }
    }

}
