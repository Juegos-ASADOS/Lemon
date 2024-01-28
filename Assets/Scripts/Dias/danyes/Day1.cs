using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1 : DayBase
{
    protected override void PrepareDay()
    {
        // Ejemplo
        dayChores.Enqueue(new TimeWaitEventChore(5));
        // Ejemplo
    }
}


