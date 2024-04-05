using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPhasing : MonoBehaviour
{
    public enum AIMasterPhases
    {
        DormantPhase,
        ExterminationPhase,
        ObservationPhase,
        DefensePhase,
        SiegePhase,
        DesperacyPhase,
    }
    public AIMasterPhases MasterPhase;
    /// <summary>
    /// The player has not been noticed in this phase.
    /// -Scouts will be scattered, but otherwise, there will be no resistance
    /// -On encountering player units, AI units will attempt to flee
    /// -If the AI flees to an outpost, then the player will be discovered
    /// -Phase is exited when the player is discovered
    /// </summary>
    public enum AIDormantPhase
    {
        DormantPhase,
        ExterminationPhase,
        ObservationPhase,
        DefensePhase,
        SiegePhase,
        DesperacyPhase,
    }
    /// <summary>
    /// Player has been noticed.
    /// -AI will actively send scouts out, attempting to find player base.
    /// -AI will create fleets to counter enemies wherever able. Will actively hunt fleets and player freighters
    /// -AI will begin looking to establish hard points
    /// -AI will also look for and secure most valuable resource nodes
    /// -AI will scout with progressively larger forces, pulling back to conserve forces, then proceeding with massive final assault
    /// -Phase ends when massive assault fails
    /// </summary>
    public enum AIExterminationPhase
    {
        DormantPhase,
        ExterminationPhase,
        ObservationPhase,
        DefensePhase,
        SiegePhase,
        DesperacyPhase,
    }
    /// <summary>
    /// AI will pull back and reinforce key positions.
    /// -AI will hunt player forces
    /// -AI will make soft pushes against the players base
    /// -AI will focus on creating fleets to counter player created fleets
    /// -
    /// -Phase ends when key positions begin to fall to the player
    /// </summary>
    public enum AIObservationPhase
    {
        DormantPhase,
        ExterminationPhase,
        ObservationPhase,
        DefensePhase,
        SiegePhase,
        DesperacyPhase,
    }

    public enum AIDefensePhase
    {
        DormantPhase,
        ExterminationPhase,
        ObservationPhase,
        DefensePhase,
        SiegePhase,
        DesperacyPhase,
    }
    /// <summary>
    /// Siege is determined when the AI is losing, but still has production capability
    /// -Heavy units will be sent to key defensive or production posts.
    /// -Lighter units will be sent out as probes
    /// -Resource systems will be abandoned
    /// </summary>
    public enum AISiegePhase
    {
        DormantPhase,
        ExterminationPhase,
        ObservationPhase,
        DefensePhase,
        SiegePhase,
        DesperacyPhase,
    }
    /// <summary>
    /// Desperacy is when the AI has nearly lost.
    /// -Light units will be sent to harrass and slow down player fleets
    /// -Heavier units will be clumped to homeworld for final stand
    /// -Phase ends when AI has been eliminated
    /// </summary>
    public enum AIDesperacyPhase
    {
        DormantPhase,
        ExterminationPhase,
        ObservationPhase,
        DefensePhase,
        SiegePhase,
        DesperacyPhase,
    }
}
