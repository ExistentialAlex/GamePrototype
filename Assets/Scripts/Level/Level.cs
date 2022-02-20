namespace Prototype.GameGeneration
{
    using System.Collections.Generic;

    /// <summary>
    /// A level in the game, containing the list of floors defined in the game.
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        /// <param name="levelNo">The level number.</param>
        public Level(int levelNo)
        {
            this.NoFloors = levelNo;
            this.Floors = new List<Floor>();
        }

        /// <summary>
        /// Gets or sets the list of floors.
        /// </summary>
        /// <value>The list of floors.</value>
        public List<Floor> Floors { get; set; }

        /// <summary>
        /// Gets or sets the number of floors.
        /// </summary>
        /// <value>The number of floors.</value>
        public int NoFloors { get; set; }
    }
}
