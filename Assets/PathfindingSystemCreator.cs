using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingSystemCreator : MonoBehaviour
{
    public bool m_usingAStarSystem;

    private PathfindingSystemBase m_pathfindingSystem;

    [SerializeField] private bool m_updatePathfindingSystem;

    // Start is called before the first frame update
    private void Start()
    {
        if (m_usingAStarSystem)
        {
            m_pathfindingSystem = gameObject.AddComponent<StepByStepAStarPathfindingController>();
        }
        else
        {
            m_pathfindingSystem = gameObject.AddComponent<StepByStepBruteforcePathfinding>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_updatePathfindingSystem)
        {
            m_updatePathfindingSystem = false;
            if(m_pathfindingSystem!=null) Destroy(m_pathfindingSystem);
            
            if (m_usingAStarSystem)
            {
                m_pathfindingSystem = gameObject.AddComponent<StepByStepAStarPathfindingController>();
            }
            else
            {
                m_pathfindingSystem = gameObject.AddComponent<StepByStepBruteforcePathfinding>();
            }
            
        }
    }
}
