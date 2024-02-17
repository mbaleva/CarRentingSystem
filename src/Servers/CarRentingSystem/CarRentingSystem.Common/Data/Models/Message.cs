using Newtonsoft.Json;
using System;

namespace CarRentingSystem.Common.Data.Models;

public class Message
{
    private string serializedData;

    public int Id { get; set; }

    public Type Type { get; set; }

    public bool Published { get; set; }

    public object Data 
    {
        get => JsonConvert.DeserializeObject(serializedData);
        set => JsonConvert.SerializeObject(value);
    }
}
