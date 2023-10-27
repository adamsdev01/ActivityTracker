using System;
using System.Collections.Generic;

namespace BlazorActivityTracker.Data.Models;

public partial class Activity
{
    public int Id { get; set; }

    public DateTime ActivityDate { get; set; }

    public double TotalHours { get; set; }

    public string? Description { get; set; }
}
