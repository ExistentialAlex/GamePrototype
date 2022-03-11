namespace Prototype.GameGeneration.Rooms
{
    using UnityEngine;

    /// <summary>
    /// Cell config.
    /// </summary>
    public class CellConfig : MonoBehaviour
    {
        /// <summary>
        /// The maximum number of enemies possible in a cell.
        /// </summary>
        [SerializeField]
        private int maxEnemies;

        /// <summary>
        /// The minimum number of enemies possible in a cell.
        /// </summary>
        [SerializeField]
        private int minEnemies;

        /// <summary>
        /// Gets the maximum enemies possible in a cell.
        /// </summary>
        /// <value>The maximum enemies possible.</value>
        public int MaxEnemies
        {
            get => this.maxEnemies;
        }

        /// <summary>
        /// Gets the minimum enemies possible in a cell.
        /// </summary>
        /// <value>The minimum enemies possible.</value>
        public int MinEnemies
        {
            get => this.minEnemies;
        }
    }
}
