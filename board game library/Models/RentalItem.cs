using System;
using System.Collections.Generic;

namespace board_game_library.Models;

public partial class RentalItem
{
    public int RentalItemId { get; set; }

    public decimal DailyRate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public int RentalId { get; set; }

    public int GameCopyId { get; set; }

    public virtual GameCopy GameCopy { get; set; } = null!;

    public virtual Rental Rental { get; set; } = null!;
}
