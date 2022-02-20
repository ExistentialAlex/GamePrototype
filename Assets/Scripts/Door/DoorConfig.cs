using UnityEngine;

/// <summary>
/// Defines a configuration for the Door class.
/// Can be modified in unity inspector.
/// </summary>
public class DoorConfig : MonoBehaviour
{
    /// <summary>
    /// Default Size for the door.
    /// Can be overridden in unity.
    /// </summary>
    [SerializeField]
    private int defaultDoorSize;

    /// <summary>
    /// Gets or sets the default door size.
    /// </summary>
    /// <value>The default size of a door.</value>
    public int DefaultDoorSize
    {
        get => this.defaultDoorSize;
        set => this.defaultDoorSize = value;
    }
}
