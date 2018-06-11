using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.StoreDomain.Entities
{
    public enum GameType
    {
        Action = 0,
        Adventure = 1,
        Role_playing = 2,
        Simulation = 3,
        Strategy = 4,
        Sports = 5
    }

    [Flags]
    public enum GamePlatforms
    {
        Windows = 1,
        Linux = 2,
        MacOS = 4
    }

    public class Game
    {
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual Company Publisher { get; set; }
        [Required]
        public virtual Company Developer { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [RegularExpression(@"\d+[.]\d+[.]\d+", ErrorMessage = "Wrong version number")]
        public string Version { get; set; }
        [Required]
        public GameType GameType { get; set; }
        [Required]
        public GamePlatforms GamePlatforms { get; set; }
        [Required]
        public virtual GameAddition Addition { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
