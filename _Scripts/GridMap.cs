using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridMap : MonoBehaviour
{
    ParticleCell[,] grid = new ParticleCell[60,60];
    [Header("Base")]
    public GameObject CellObject;
    public Transform MapParent;
    public float cellDist;

    public ValueScriptable dirtyPercent;
    public ValueBinder dirtyBinder;
    [Header("Laundry")]
    public ValueScriptable laundryCount;
    public ValueBinder laundryBinder;
    public GameEvent laundryBought;
    [Header("Cat")]
    public ValueScriptable catCount;
    public ValueBinder catBinder;
    public GameEvent catBought;
    [Header("Takeout")]
    public ValueScriptable takeoutCount;
    public ValueBinder takeoutBinder;
    public GameEvent takoutBought;
    [Header("Mold")]
    public ValueScriptable moldCount;
    public ValueBinder moldBinder;
    public GameEvent moldBought;
    [Header("Roomies")]
    public ValueScriptable roomieCount;
    public ValueBinder roomieBinder;
    public GameEvent roomieBought;
    [Header("Dirty Dollars")]
    public DirtyDollarsManager ddm;
    //Abused to give money
    public GameEvent clickedEvent;

    [Header("Room Full")]
    public ValueScriptable roomCount;
    public ValueBinder roomCountBinder;
    public GameObject particleExplostion;
    public GameEvent momSpawn;
    
  
    public void Start()
    {
        //Fill Empty Grid
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                GameObject temp = Instantiate(CellObject, MapParent);
                temp.GetComponent<RectTransform>().localPosition = new Vector3(x * cellDist, y * cellDist, 0);
                grid[x,y] = temp.GetComponent<ParticleCell>();
            }
        }
        catCount.value = 0;
        roomieCount.value = 0;
        takeoutCount.value = 0;
        moldCount.value = 0;
        laundryCount.value = 0;

        ddm.currentCurrency.value = 0;

        roomCount.value = 0;

        catBinder.UpdateText();
        takeoutBinder.UpdateText();
        roomieBinder.UpdateText();
        laundryBinder.UpdateText();
        moldBinder.UpdateText();
        dirtyBinder.UpdateText();
        roomCountBinder.UpdateText();

        InvokeRepeating("Automata", 0f, 0.1f);
    }
    #region laundry
    public void CreateLaundryInvoke()
    {
        if (!ddm.TryRemoveCurrency(2))
            return;
        InvokeRepeating("LaundryCall", 0f, 10f);
        laundryCount.value += 1;
        laundryBinder.UpdateText();
        laundryBought.Raise();
    }

    private void LaundryCall()
    {
        int rand = UnityEngine.Random.Range(0, 59);
        grid[rand, 59].state = particleType.laundry;
        clickedEvent.Raise();
    }
    #endregion
    #region cats
    public void CreateCatInvoke()
    {
        if (!ddm.TryRemoveCurrency(10))
            return;
        InvokeRepeating("CatCall", 0f, 5f);
        catCount.value += 1;
        catBinder.UpdateText();
        catBought.Raise();
    }

    private void CatCall()
    {
        int rand = UnityEngine.Random.Range(0,59);
        grid[rand, 59].state = particleType.cat;
        clickedEvent.Raise();
    }
    #endregion
    #region takout
    public void TakeoutInvoke()
    {
        if (!ddm.TryRemoveCurrency(25))
            return;
        InvokeRepeating("TakeoutSpawn", 0f, 10f);
        takeoutCount.value += 1;
        takeoutBinder.UpdateText();
        takoutBought.Raise();
    }

    private void TakeoutSpawn()
    {
        int rand = UnityEngine.Random.Range(0, 57);
        grid[rand+1, 59].state = particleType.takeout;
        grid[rand, 58].state = particleType.takeout;
        grid[rand+1, 58].state = particleType.takeout;
        grid[rand+2, 58].state = particleType.takeout;
        grid[rand+1, 57].state = particleType.takeout;
        clickedEvent.Raise();
    }
    #endregion
    #region mold
    public void CreateMoldInvoke()
    {
        if (!ddm.TryRemoveCurrency(50))
            return;
        InvokeRepeating("MoldSpawn", 0f, 15f);
        moldCount.value += 1;
        moldBinder.UpdateText();
        moldBought.Raise();
    }

    private void MoldSpawn()
    {
        int rand = UnityEngine.Random.Range(0, 57);
        grid[rand, 59].state = particleType.mold;
        grid[rand + 1, 59].state = particleType.mold;
        grid[rand + 2, 59].state = particleType.mold;
        grid[rand, 58].state = particleType.mold;
        grid[rand + 1, 58].state = particleType.mold;
        grid[rand + 2, 58].state = particleType.mold;
        grid[rand, 57].state = particleType.mold;
        grid[rand + 1, 57].state = particleType.mold;
        grid[rand + 2, 57].state = particleType.mold;
        clickedEvent.Raise();
        clickedEvent.Raise();
        clickedEvent.Raise();
    }
    #endregion
    #region roomie
    public void CreateRoomieInvoke()
    {
        if (!ddm.TryRemoveCurrency(100))
            return;
        InvokeRepeating("RoomieSpawn", 0f, 15f);
        roomieCount.value += 1;
        roomieBinder.UpdateText();
        roomieBought.Raise();
    }

    private void RoomieSpawn()
    {
        for(int i = 1; i < 59; i++)
            grid[i, 59].state = particleType.roomie;
        //Yikes
        clickedEvent.Raise();
        clickedEvent.Raise();
        clickedEvent.Raise();
    }
    #endregion

    public void Automata()
    {
        UpdateParticles();
        DrawGrid();
        CheckDirty();
    }

    public void CleanRoom()
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                grid[x, y].state = particleType.empty;
            }
        }

        CancelInvoke();
        InvokeRepeating("Automata", 0f, 0.1f);

        catCount.value = 0;
        roomieCount.value = 0;
        takeoutCount.value = 0;
        moldCount.value = 0;
        laundryCount.value = 0;

        catBinder.UpdateText();
        takeoutBinder.UpdateText();
        roomieBinder.UpdateText();
        laundryBinder.UpdateText();
        moldBinder.UpdateText();

        ddm.currentCurrency.value = 0;
        ddm.vb.UpdateText();
    }

    private void CheckDirty()
    {
        int total = grid.GetLength(1) * grid.GetLength(0);
        int count = 0;
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (grid[x, y].state != particleType.empty)
                    count++;
            }
        }
        float percent = (float)count / (float)total;
        percent = (float)Math.Round(percent * 100, 2);
        dirtyPercent.value = percent;
        dirtyBinder.UpdateText();

        if(percent == 100)
        {
            roomCount.value++;
            roomCountBinder.UpdateText();
            ExplodeRoom();
        }
    }

    private void ExplodeRoom()
    {
        //Blowup and reset the room for some extra flare
        momSpawn.Raise();
        CleanRoom();
    }

    private void UpdateParticles()
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if(grid[x,y].state != particleType.empty)
                    MoveParticle(x, y);
            }
        }
    }

    private void MoveParticle(int x, int y)
    {

        if (y < 1)
            return;
        //This gross monstrosity of repeating itself to deal with edges
        if (x - 1 < 0)
        {
            if (grid[x + 1, y - 1].state == particleType.empty)
            {
                grid[x + 1, y - 1].state = grid[x, y].state;
                grid[x, y].state = particleType.empty;
            }
            else if (grid[x, y - 1].state == particleType.empty)
            {
                grid[x, y - 1].state = grid[x, y].state;
                grid[x, y].state = particleType.empty;
            }
        }
        else if (x + 1 >= 60)
        {
            if(grid[x - 1, y - 1].state == particleType.empty)
            {
                grid[x - 1, y - 1].state = grid[x, y].state;
                grid[x, y].state = particleType.empty;
                
            }
            else if(grid[x, y - 1].state == particleType.empty)
            {
                grid[x, y - 1].state = grid[x, y].state;
                grid[x, y].state = particleType.empty;
            }
        }
        //Code dealing with not edge cases starts here
        else if(grid[x,y - 1].state == particleType.empty)
        {
            grid[x,y - 1].state = grid[x, y].state;
            grid[x, y].state = particleType.empty;
        }
        else if (grid[x - 1,y - 1].state == particleType.empty)
        {
            grid[x - 1, y - 1].state = grid[x, y].state;
            grid[x,y].state = particleType.empty;
        }
        else if (grid[x + 1,y - 1].state == particleType.empty)
        {
            grid[x + 1, y - 1].state = grid[x, y].state;
            grid[x,y].state = particleType.empty;
        }
    }

    private void DrawGrid()
    {
        for (int y = grid.GetLength(1) - 1; y >= 0; --y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                grid[x,y].UpdateColor();
            }
        }
    }
}
