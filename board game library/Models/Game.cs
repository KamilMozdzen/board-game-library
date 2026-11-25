using System;
using System.Collections.Generic;

namespace board_game_library.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string Title { get; set; } = null!;

    public int MinPlayers { get; set; }

    public int MaxPlayers { get; set; }

    public int PlayingTimeMinutes { get; set; }

    public decimal DailyRate { get; set; }

    public int CategoryId { get; set; }

    public int PublisherId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<GameCopy> GameCopies { get; set; } = new List<GameCopy>();

    public virtual Publisher Publisher { get; set; } = null!;
}
