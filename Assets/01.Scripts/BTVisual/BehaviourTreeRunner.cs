using UnityEngine;

namespace BTVisual
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        private BehaviourTree _tree;

        private void Start()
        {
            _tree = ScriptableObject.CreateInstance<BehaviourTree>();

            var repeatNode = ScriptableObject.CreateInstance<RepeatNode>();

            _tree.rootNode = repeatNode;

        }

        private void Update()
        {
            _tree.Update();
        }
    }
}
