using System;
using System.Collections.Generic;

namespace board_game_library.Models;

public partial class Rental
{
    public int RentalId { get; set; }

    public DateOnly RentalDate { get; set; }

    public DateOnly DueDate { get; set; }

    public int CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<RentalItem> RentalItems { get; set; } = new List<RentalItem>();
}
