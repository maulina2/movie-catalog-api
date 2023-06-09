﻿namespace Cinema.Models;

public class Review
{
    public Guid Id { get; set; }
    public string? ReviewText { get; set; }
    public int Rating { get; set; }
    public DateTime CreateDateTime { get; set; }
    public bool IsAnonymous { get; set; }
    public User Author { get; set; }
    public Guid MovieId { get; set; }
}