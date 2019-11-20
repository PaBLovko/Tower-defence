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

        if (Input.GetMouseButton(1))
        {
            DisebleDrag();
            towerBtnPressed = null;
        }
        if (spriteRenderer.enabled)
        {//если у нас активен спрайт рендерер то следуем мыши
            FollowMouse();
        }
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)//мы не сможем поставить башню если нажали на кноку выбора башни
        {
            TowerControlle chousenTower = towerBtnPressed.GetTowerControll;
            Manager.Instance.SetResources(Manager.Instance.GetResources() - chousenTower.GetCost());
            GameObject newTower = Instantiate(towerBtnPressed.GetTowerObject);//создаем там башню именно того типа на который мы кликнули 

            newTower.transform.position = hit.transform.position;//положение нового тавера будет в том месте куда мы кликаем
        }
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//изображение просчитывает положение относительно камеры и привяз к курсору 
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnebleDrag(Sprite sprite)
    {
        spriteRenderer.enabled = true;//включаем отображение картинки
        spriteRenderer.sprite = sprite;//отображаем ту картинку кот выбрана

    }

    public void DisebleDrag()
    {
        spriteRenderer.enabled = false;//выключаем отображение картинки
    }

    public void SelectedTower(TowerBtn towerSellected)
    {//какая башня была выбрана 
        towerBtnPressed = towerSellected;//относит только кнопки мы нажали должна быть выбрана башня
        EnebleDrag(towerBtnPressed.GetDragSprite);//включаем отолражение картинки тавера 

    }

}
