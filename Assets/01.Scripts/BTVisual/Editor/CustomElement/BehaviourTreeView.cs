using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BTVisual
{
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits> { }
        public new class UxmlTraits : GraphView.UxmlTraits { }

        private BehaviourTree _tree;

        public Action<NodeView> OnNodeSelected;

        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());

            //�Ŵ�ǽ������ (�巡���̺�Ʈ -> ���콺�ٿ� + ���콺 ���� + ���콺 �� �̺�Ʈ)

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
        }

        public void PopulateView(BehaviourTree tree)
        {
            
            _tree = tree;
            graphViewChanged -= OnGraphViewChanged; //���� �̺�Ʈ ������

            DeleteElements(graphElements); //������ �׷����� �ֵ��� ���� ����

            graphViewChanged += OnGraphViewChanged; //�ٽ� �̺�Ʈ ����

            if(_tree.rootNode == null)
            {
                //���� �����ð��� ���� tree ���Ծ���!!!!
                _tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(_tree);
                AssetDatabase.SaveAssets(); 
            }

            tree.nodes.ForEach(n => CreateNodeView(n));

            //���� �׷��ִ� ��
            tree.nodes.ForEach(n =>
            {
                var children = tree.GetChildren(n); //n�� �ڽ��� �����´�.
                NodeView parent = FindNodeView(n);
                children.ForEach(c =>
                {
                    NodeView child = FindNodeView(c); //�ش� �ڽ� �����ͼ�
                    //�������
                    Edge edge = parent.output.ConnectTo(child.input);
                    AddElement(edge);
                });
            });
        }

        //guid�� ��� ������Ʈ �������ִ� �Լ�
        private NodeView FindNodeView(Node node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if(graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    var nv = elem as NodeView;
                    if(nv != null)
                    {
                        _tree.DeleteNode(nv.node); //���� SO������ �����ض�
                    }

                    var edge = elem as Edge;
                    if(edge != null)  //���ἱ�� �����ȰŴ�
                    {
                        //�̰� �����ϸ�ɱ�?
                        NodeView parent = edge.output.node as NodeView;
                        NodeView child = edge.input.node as NodeView;

                        _tree.RemoveChild(parent.node, child.node);
                    }
                });
            }

            if(graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parent = edge.output.node as NodeView;
                    NodeView child = edge.input.node as NodeView;

                    _tree.AddChild(parent.node, child.node);
                });
            }

            return graphViewChange;
        }

        private void CreateNodeView(Node n)
        {
            NodeView nv = new NodeView(n);
            nv.OnNodeSelected = OnNodeSelected;
            AddElement(nv);
        }

        private void CreateNode(Type t)
        {
            Node node = _tree.CreateNode(t);
            CreateNodeView(node);
            
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if(_tree == null)
            {
                evt.StopPropagation(); //�̺�Ʈ ���� ������ �ɰ�
                return;
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach(var t in types)
                {
                    evt.menu.AppendAction($"[{t.BaseType.Name}] / {t.Name}", 
                                            (a) => { CreateNode(t); });
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[{t.BaseType.Name}] / {t.Name}",
                                            (a) => { CreateNode(t); });
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[{t.BaseType.Name}] / {t.Name}",
                                            (a) => { CreateNode(t); });
                }
            }
        }


        //�巡���� ���۵ɶ� ���ᰡ���� ��Ʈ�� �������� �Լ�
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(x => x.direction != startPort.direction && x.node != startPort.node)
                .ToList();
        }
    }
}
