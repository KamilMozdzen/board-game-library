using System;
using System.Collections.Generic;

namespace board_game_library.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public int RentalId { get; set; }

    public virtual Rental Rental { get; set; } = null!;
}
