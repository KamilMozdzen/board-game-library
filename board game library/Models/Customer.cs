using System;
using System.Collections.Generic;

namespace board_game_library.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
