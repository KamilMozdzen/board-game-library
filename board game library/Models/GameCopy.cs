using System;
using System.Collections.Generic;

namespace board_game_library.Models;

public partial class GameCopy
{
    public int GameCopyId { get; set; }

    public string InventoryNumber { get; set; } = null!;

    public string? ConditionDescription { get; set; }

    public bool IsActive { get; set; }

    public int GameId { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual RentalItem? RentalItem { get; set; }
}
