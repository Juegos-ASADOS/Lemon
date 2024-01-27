using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayBase : MonoBehaviour
{
    protected bool isDayRunning = true;
    protected Queue<DayChore> dayChores = new Queue<DayChore>();
    protected DayChore actualChore = null;

    [SerializeField] protected Cliente[] clientsOfDay;

    public virtual void InitDay()
    {
        isDayRunning = true;
        StartCoroutine(ProcessDay());
    }

    protected virtual void PrepareDay()
    {
        // Enqueue chores for the day
    }

    public bool HasEnded()
    {
        return !isDayRunning;
    }

    public IEnumerator ProcessDay()
    {
        PrepareDay();

        Debug.Assert(dayChores.Count != 0, "No hay tareas en todo el día: " + this.name);
        
        while (dayChores.Count > 0){
            actualChore = dayChores.Dequeue();
            actualChore.StartChore();

            while (!actualChore.HasFinished())
            {
                actualChore.UpdateChore();
                yield return null;
            }
        }

        isDayRunning = false;
    }
}


public abstract class DayChore
{
    public abstract void StartChore();
    public abstract void UpdateChore();
    public abstract bool HasFinished();
}

public class ClientChore : DayChore
{
    Cliente client;
    public ClientChore(Cliente client)
    {
        this.client = client;
    }

    public override bool HasFinished()
    {
        return false; // Comprobar si el cliente se ha marchado
    }

    public override void StartChore()
    {
        // Preparar al cliente para entrar
    }

    public override void UpdateChore()
    {
        // Manejar todo el ciclo del cliente, si no lo maneja él ya por su cuenta
    }

}

public class RandomEventChore : DayChore
{
    public RandomEventChore()
    {
    
    }
    // No sé cómo serán pero hay que adaptarlo aquí
    public override bool HasFinished()
    {
        return false;
    }

    public override void StartChore()
    {
    }

    public override void UpdateChore()
    {

    }
}

// Clase simple que espera unos segundos, para que no sucedan todas las tareas de golpe
public class TimeWaitEventChore : DayChore
{
    float waitTime = 0;
    public TimeWaitEventChore(float timeInSeconds)
    {
        waitTime = timeInSeconds;
    }

    public override bool HasFinished()
    {
        return waitTime <= 0; // Acaba el timer
    }

    public override void StartChore()
    {
        Debug.Log("Started Timer");
        // nada
    }

    public override void UpdateChore()
    {
        waitTime -= Time.deltaTime;
    }
}