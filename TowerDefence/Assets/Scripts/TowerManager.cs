using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Loader<TowerManager>
{

    TowerBtn towerBtnPressed;//нажата кнопка или нет 

    SpriteRenderer spriteRenderer;//отображает картинку окол кусора

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {//если нажимаем левую кнопку мыши
            try
            {
                TowerControlle chousenTower = towerBtnPressed.GetTowerControll;
                Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);//считывает положение нашего курсора относительно экрана
                RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);//луч будет идти от нуля координат и отправлятся к месту нашего клика и будет смотреть куда мы кликаем
                if (chousenTower.GetCost() <= Manager.Instance.GetResources() && hit.collider.tag == "TowerGround" && towerBtnPressed != null)
                {//если мы получим какой-то тег и он равен TowerGround
                    hit.collider.tag = "TowerGroundIsFull";//меняем тэг чтобы нельзя было поставить 2 башни в одну 
                    PlaceTower(hit);//то выставляем товер 
                    DisebleDrag();
                }
                else if (spriteRenderer.enabled)
                {
                    Manager.Instance.GetSound().Play();
                }
            }
            catch (Exception) { }

        }

        if (Input.GetMouseButton(1))
        {
            DisebleDrag();
            towerBtnPressed = null;
        }
        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            TowerControlle chousenTower = towerBtnPressed.GetTowerControll;
            Manager.Instance.SetResources(Manager.Instance.GetResources() - chousenTower.GetCost());
            GameObject newTower = Instantiate(towerBtnPressed.GetTowerObject);

            newTower.transform.position = hit.transform.position;
        }
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnebleDrag(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;

    }

    public void DisebleDrag()
    {
        spriteRenderer.enabled = false;
    }

    public void SelectedTower(TowerBtn towerSellected)
    {
        towerBtnPressed = towerSellected;
        EnebleDrag(towerBtnPressed.GetDragSprite);

    }

}
