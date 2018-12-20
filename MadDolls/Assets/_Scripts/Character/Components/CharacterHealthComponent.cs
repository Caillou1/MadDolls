﻿public class CharacterHealthComponent : CharacterComponent
{
    public int MaxHealth;
    public int CurrentHealth { get; private set; }

    public void ApplyDamage(int amount)
    {
        CurrentHealth -= amount;
        
        if(CurrentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        //Enable ragdoll
    }
}
